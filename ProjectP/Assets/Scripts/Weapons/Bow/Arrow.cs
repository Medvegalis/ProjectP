using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float arrowSpeed = 5f;
    [SerializeField]
    private float destroyTimer = 2f;
    [SerializeField]
    private int damage = 1;

    private Rigidbody2D arrowRigidBody;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        arrowRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //arrow movement
        if (arrowRigidBody.bodyType == RigidbodyType2D.Static)
        {
            return;
        }
        arrowRigidBody.velocity = (Vector2)transform.up * arrowSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
/*        if (collision.isTrigger)
        {
            return;
        }*/

        switch (collision.transform.tag)
        {
            //if the other rigid body has enemy tag try to deal damage
            case "Enemy": DamageEnemy(collision.collider); break;
            // if it hit terrain make it linger for a bit and destroy it
            case "Terrain": TerrainHitResolve(); break;
            default: break;
        }
    }

    private void DamageEnemy(Collider2D enemy)
    {
        //immediately destroy the arrow
        Destroy(gameObject);

        //get the enemy health script to be able to damage it
        var healthScript = enemy.GetComponent<EnemyHealth>();
        //check if the collided entity tagged as enemy has enemy health script
        if (healthScript != null)
        {
            healthScript.DamageEnemy(damage);
        }
    }

    /// <summary>
    /// resolve the terrain hit logic
    /// </summary>
    private void TerrainHitResolve() 
    {
        audioSource.Play();
        arrowSpeed = 0;
        arrowRigidBody.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, destroyTimer);
    }

    /// <summary>
    /// setting the damage number of the projectile to realise different base damage weapons
    /// </summary>
    /// <param name="damageNumber"></param>
    public void SetDamage(int damageNumber)
    {
        damage = damageNumber;
    }

    /// <summary>
    /// setting the projectile speed to realise different variants of bows
    /// </summary>
    /// <param name="speed"></param>
    public void SetProjectileSpeed(float speed)
    {
        arrowSpeed = speed;
    }
}
