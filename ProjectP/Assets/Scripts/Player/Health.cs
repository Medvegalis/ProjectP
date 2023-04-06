using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent Dead;
    [SerializeField] private float InvincibilityTime = 1f;
    [SerializeField] private bool IsBeingHit = false;
    [SerializeField] private bool isInvincible = false;


    private SpriteRenderer spriteRenderer;

    [Header("Player Health")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int health;
    public int numOfHearts;

    [Header("Player XP")]
    [SerializeField] private int maxXP = 100;
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int currentLvl = 0;

    public Slider xpSlider;
    public TextMeshProUGUI playerLevelUIText;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Collectable.OnCollected += IncreaseLevel;
    }
    void Update() {

        if (health > numOfHearts)
            health = numOfHearts;

        for (int i = 0; i < hearts.Length; i++) {

            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }

        if(IsBeingHit)
        {
            DamagePlayer();
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.transform.tag == "Enemy")
        {
            IsBeingHit = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.transform.tag == "Enemy")
        {
            IsBeingHit = false;
        }
    }

    private void IncreaseLevel(int value)
    {
        if(value == 0)
        {
            if (currentXP >= maxXP)
            {
                currentLvl++;
                currentXP = 0;
                maxXP = maxXP * 2;

                xpSlider.maxValue = maxXP;
                xpSlider.value = currentXP;
                playerLevelUIText.text = currentLvl.ToString();
            }
            else
            {
                currentXP += 25;
                xpSlider.value = currentXP;
            }
        }
    }


    public void DamagePlayer()
    {
        if(isInvincible)
            return;

        if(health > 0)
            health -= 1;

        if(health <= 0)
        {
            Dead.Invoke();
            return;
        }

        StartCoroutine(BecomeInvincible());
    }

    //Makes the player invincible for InvincibilityTime duration
    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(InvincibilityTime);

        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }
}
