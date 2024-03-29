using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float hurtSpriteColorChangeTime = .2f;

    [SerializeField]
    private AudioSource takingDamageAudioSourceMain;
    [SerializeField]
    private AudioSource takingDamageAudioSourceAlternate;

    private bool alternateDamageSound;
    private bool gotHurt;
    private float timeSinceHurt;
    public int health;
    public bool isFullHP = true;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        //spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        alternateDamageSound = false;
        gotHurt = false;
    }

    void Update()
    {
        if (gotHurt)
        {
            timeSinceHurt = Time.time;
            gotHurt = false;
        }
        if (timeSinceHurt + hurtSpriteColorChangeTime <= Time.time)
        {
           spriteRenderer.color = Color.white;
        }
    }

    public void setInvincible(bool state)
    {
        isInvincible = state;
    }

    public void DamageEnemy(int damageAmount)
    {
        if (isInvincible)
            return;

        if (health > 0)
        {
            isFullHP = false;
            health -= damageAmount;
            spriteRenderer.color = Color.red;
            gotHurt = true;
            
			if (alternateDamageSound)
			{
                takingDamageAudioSourceAlternate.Play();
                alternateDamageSound = false;
            }
			else
			{
                takingDamageAudioSourceMain.Play();
                alternateDamageSound = true;
			}

        }
            

        if (health <= 0)
        {
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            Destroy(gameObject);
            return;
        }
    }
}
