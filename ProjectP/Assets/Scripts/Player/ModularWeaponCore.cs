using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularWeaponCore : MonoBehaviour
{
    private PlayerControls playerControls;
    public Transform WeaponPos;
    [SerializeField]
    GameObject startingWeapon;
    [SerializeField]
    float swapCooldown = 2f;
    [SerializeField]
    private bool canSwap;

    public bool swapOnCooldown;
    GameObject currentWeapon;

    //weapon array
    [SerializeField]
    int weaponSlotCount = 3;
    [SerializeField]
    GameObject[] weapons = new GameObject[3];

    public int currentWeaponIndex;
    public int currentWeaponCount;

    public bool standingOnWeapon;
    public GameObject weaponOnGround;

    void Start()
    {
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
    }


    void Update()
    {
        WeaponRotateToLookAtMouse();
        SwapWeapon();
        pickUpWeapon();
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

        if (currentWeaponCount >= weaponSlotCount)
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
