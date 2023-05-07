using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModularWeaponCore : MonoBehaviour
{
    [Header("Weapon slots")]
    public Image[] slots;
    public Image[] slotBorders;
    public Sprite selectedSlotBorderSprite;
    public Sprite slotBorderSprite;


    private PlayerControls playerControls;

    [Header("Variables for weapons")]
    public Transform WeaponPos;
    [SerializeField]
    GameObject startingWeapon;


    [Header("Swapping")]
    [SerializeField]
    float swapCooldown = 2f;
    [SerializeField]
    private bool canSwap;

    public bool swapOnCooldown;
    GameObject currentWeapon;

    //weapon array
    [SerializeField]
    GameObject[] weapons;

    public int maxWeaponCount;

    public int currentWeaponIndex;
    public int currentWeaponCount;

    [Header("Pickup variables")]
    public bool standingOnWeapon;
    public GameObject weaponOnGround;

    void Start()
    {
        maxWeaponCount = weapons.Length;
        //weapon swap logic
        canSwap = true;
        swapOnCooldown = false;
        weapons[0] = startingWeapon;
        playerControls = gameObject.GetComponent<PlayerControler>().playerControls;
        currentWeaponIndex = 0;
        InstantiateWeapon(startingWeapon);
        //pick up logic
        standingOnWeapon = false;
        currentWeaponCount = 1;
        //slotBorderSprite instantiation
        updateslotBorderSpriteVisuals();
    }


    void Update()
    {
        WeaponRotateToLookAtMouse();

        SwapWeapon();

        pickUpWeapon();

        updateslotBorderSpriteVisuals();
    }

    //waepon slot sprite update logic
    private void updateslotBorderSpriteVisuals() {
        for (int i = 0; i < slots.Length; i++)
        {
            var currentSlot = slots[i];
            //adding the weapon sprite to the slotBorderSprite
            if (weapons[i] != null)
            {
                GameObject weapon = weapons[i];
                Sprite weaponSprite = weapon.GetComponent<SpriteRenderer>().sprite;


                currentSlot.sprite = weaponSprite;
                currentSlot.enabled = true;
            }
            else
            {
                currentSlot.enabled = false;
            }

            //updating selected slot highlight
            if (i == currentWeaponIndex)
            {
                slotBorders[i].sprite = selectedSlotBorderSprite;
            }
            else
            {
                slotBorders[i].sprite = slotBorderSprite;
            }
        }
    }

    // Aim at mouse logic
    private void WeaponRotateToLookAtMouse()
    {
        //var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //var angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        //weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (!playerControls.Player.enabled)
        {
            return;
        }
        if (Time.timeScale == 0)
        {
            return;
        }
		if (currentWeapon.GetComponent<IIsRotatable>() == null)
		{
            return;
		}
        float angle = Utility.AngleTowardsMouse(currentWeapon.transform.position);
        currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    //swap weapon logic
    private void SwapWeapon()
    {
        //guard clause to check if player can swap
        if (!canSwap)
        {
            return;
        }
        //guard clause to check if swap cooldown passed
        if (swapOnCooldown)
        {
            return;
        }
        //guard clause to check if Swap button is not pressed or controls are disabled
        if (!SwapButtonPressed())
        {
            return;
        }


        //check if next weapon is an index away or should we wrap around
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        //check if you can swap over to the other weapon
        if (currentWeaponIndex >= currentWeaponCount)
        {
            currentWeaponIndex = 0;
            return;
        }


        Destroy(currentWeapon);
        InstantiateWeapon(weapons[currentWeaponIndex]);
        StartCoroutine(PerformCooldownPeriod());
    }

    private IEnumerator PerformCooldownPeriod() {
        swapOnCooldown = true;
        yield return new WaitForSeconds(swapCooldown);

        yield return new WaitForSeconds(0.1f);
        swapOnCooldown = false;
    }

    private bool SwapButtonPressed()
    {
        //guard clause to check if player controls are enabled
        if (!playerControls.Player.enabled)
        {
            return false;
        }
        //check if weapon swap button is pressed
        if (playerControls.Player.SwapWeapon.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    //Pickup logic
    private void pickUpWeapon() 
    {
        if (!standingOnWeapon)
        {
            return;
        }
        if (!PickUpButtonPressed())
        {
            return;
        }

        if (currentWeaponCount >= maxWeaponCount)
        {
            replaceExistingWeapon();
            return;
        }

        Vector3 zeroPos = new Vector3(0, 0, 0);
        weapons[currentWeaponCount++] = weaponOnGround;
        weaponOnGround.transform.position = zeroPos;
    }

    private void replaceExistingWeapon() {
        Vector3 zeroPos = new Vector3(0, 0, 0);

        weapons[currentWeaponIndex] = weaponOnGround;
        weaponOnGround.transform.position = zeroPos;
        Destroy(currentWeapon);
        InstantiateWeapon(weapons[currentWeaponIndex]);
    }

    private void dropActiveWeapon() 
    { 
    }

    //logic to check if player is standing on weapon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Weapon")
        {
            standingOnWeapon = true;
            weaponOnGround = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            standingOnWeapon = false;
            weaponOnGround = null;
        }
    }

    private bool PickUpButtonPressed()
    {
        //guard clause to check if player controls are enabled
        if (!playerControls.Player.enabled)
        {
            return false;
        }
        //check if weapon swap button is pressed
        if (playerControls.Player.PickUpWeapon.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    //main instantiate method to make the weapon object active
    private void InstantiateWeapon(GameObject weaponObj)
    {
        currentWeapon = Instantiate(weaponObj, WeaponPos.position, WeaponPos.rotation);
        currentWeapon.transform.SetParent(gameObject.transform);
        var attackScript = currentWeapon.GetComponent<IHasAttack>();
        attackScript.EnableAttack();
    }
}
