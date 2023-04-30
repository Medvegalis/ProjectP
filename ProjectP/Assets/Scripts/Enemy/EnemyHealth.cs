using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float hurtSpriteColorChangeTime = .2f;
    private bool gotHurt;
    private float timeSinceHurt;
    public int health;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        //spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
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

    public void DamageEnemy(int damageAmount)
    {
        if (isInvincible)
            return;

        if (health > 0)
        {
            health -= damageAmount;
            spriteRenderer.color = Color.red;
            gotHurt = true;
        }
            

        if (health <= 0)
        {
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            Destroy(gameObject);
            return;
        }
    }
}
