using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    private int attackDamage;
    private float attackKnockback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag + " hit " + collision.transform.gameObject.name);
        if (collision.transform.tag == "Enemy")
        {
            DamageEnemy(collision);
        }
    }


    private void DamageEnemy(Collider2D enemy)
    {
        //get the enemy health script to be able to damage it
        var healthScript = enemy.GetComponent<EnemyHealth>();
        //check if the collided entity tagged as enemy has enemy health script
        if (healthScript != null)
        {
            healthScript.DamageEnemy(attackDamage);
        }
    }

    public void setAttackDamage(int damage) 
    {
        attackDamage = damage;
    }

    public void setKnockback(float knockback)
    {
        attackKnockback = knockback;
    }
}
