using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class PlayerScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private UnityEvent Dead;
    [SerializeField] private UnityEvent LevelUp;
    [SerializeField] private float InvincibilityTime = 1f;
    [SerializeField] private bool IsBeingHit = false;
    [SerializeField] private bool isInvincible = false;
    bool temp = true;

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

        StartCoroutine(nameof(StartDelayed));


    }

    IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(0.6f);
        playerLevelUIText.text = currentLvl.ToString();
        if (currentLvl != 0)
            maxXP = maxXP * currentLvl * 2;
        xpSlider.maxValue = maxXP;
        xpSlider.value = currentXP;
    }
    void Update() 
    {
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
        if(coll.transform.tag == "Enemy" || coll.transform.tag == "EnemyProjectile")
        {
            IsBeingHit = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.transform.tag == "Enemy" || coll.transform.tag == "EnemyProjectile")
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
                playerLevelUIText.text = currentLvl.ToString();
                xpSlider.maxValue = maxXP;
                xpSlider.value = currentXP;
                LevelUp.Invoke();
            }
            else
            {
                if (currentXP + value >= maxXP)
                {

                    currentXP += value;
                    xpSlider.value = maxXP;
                }
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

        IsBeingHit = false;
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

    public void BecomeInvicible(float duration)
    {
        StartCoroutine(SetInvicibleForDuration(duration));
    }

    public IEnumerator SetInvicibleForDuration(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public void LoadData(GameData data)
    {
        this.currentLvl = data.playerLevel;
        this.currentXP = data.playerCurrentXP;
    }

    public void SaveData(GameData data)
    {
        data.playerLevel = this.currentLvl;
        data.playerCurrentXP = this.currentXP;
    }
}
