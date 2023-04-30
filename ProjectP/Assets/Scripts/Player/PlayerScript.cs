using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private UnityEvent Dead;
    [SerializeField] private UnityEvent LevelUp;
    [SerializeField] private float InvincibilityTime = 1f;
    [SerializeField] private bool IsBeingHit = false;
    [SerializeField] private bool isInvincible = false;

    private int damageToTake; // damage that will be delt to player from onDamagePlayer event

    public Stat maxHealth;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat speed;


    private SpriteRenderer spriteRenderer;

    [Header("Player Health")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Player XP")]
    [SerializeField] private int maxXP = 100;
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int currentLvl = 0;

    public Slider xpSlider;
    public TextMeshProUGUI playerLevelUIText;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth.currentValue;
    }

    void Start()
    {
        Collectable.OnCollected += IncreaseLevel;
        Collectable.OnCollected += Heal;
        EnemyProjectileScript.onHit += DamagePlayer;

    }
    void Update() {

        if (currentHealth > maxHealth.currentValue)
            currentHealth = maxHealth.currentValue;

        for (int i = 0; i < hearts.Length; i++) {

            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < maxHealth.currentValue)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }

        if(IsBeingHit)
        {
            DamagePlayer(1);
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

    private void IncreaseLevel(typeOfLoot type, int value)
    {
        if(type == typeOfLoot.xp)
        {
            if (currentXP >= maxXP)
            {
                currentLvl++;
                currentXP = 0;
                maxXP = maxXP * 2;

                xpSlider.maxValue = maxXP;
                xpSlider.value = currentXP;
                playerLevelUIText.text = currentLvl.ToString();
                LevelUp.Invoke();
            }
            else
            {
                currentXP += value;
                xpSlider.value = currentXP;
            }
        }
    }

    private void Heal(typeOfLoot type, int value)
    {
        if (type == typeOfLoot.health)
        {
            int temp = currentHealth + value;

            if (temp <= maxHealth.maxLevel)
                currentHealth += value;
        }
    }


    public void DamagePlayer(int amount)
    {
        if (amount == 0)
            return;

        if(isInvincible)
            return;

        if(currentHealth > 0)
            currentHealth -= amount;

        if(currentHealth <= 0)
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
