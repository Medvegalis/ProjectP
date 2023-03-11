using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float progress;

    [SerializeField]
    private float arrowSpeed = 5f;
    [SerializeField]
    private float destroyTimer = 2f;
    [SerializeField]
    private int damage = 1;

    private Rigidbody2D arrowRigidBody;
    
    void Start()
    {
        arrowRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //arrow movement
        arrowRigidBody.velocity = (Vector2)transform.up * arrowSpeed;
    }

    //Happens when the arrow collides with other rigidbody
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.transform.tag) 
        {
            //if the other rigid body has enemy tag try to deal damage
            case "Enemy": DamageEnemy(other); break;
            // if it hit terrain make it linger for a bit and destroy it
            case "Terrain": TerrainHitResolve(); break;
            //If projectile hits other projectile destroy it
            //(possible future fixes/implimentations for other interact option)
            case "Projectile": Destroy(gameObject, 0.1f); break;
            default: break;
        }
    }

    private void DamageEnemy(Collision2D enemy)
    {
        //immediately destroy the arrow
        Destroy(gameObject);

        //get the enemy health script to be able to damage it
        var healthScript = enemy.collider.GetComponent<EnemyHealth>();
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
        arrowSpeed = 0;
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
