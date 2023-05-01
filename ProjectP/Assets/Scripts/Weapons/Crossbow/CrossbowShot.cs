using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowShot : MonoBehaviour
{
    [SerializeField]
    private float shotSpeed = 5f;
    [SerializeField]
    private float destroyTimer = .1f;
    [SerializeField]
    private int damage = 10;

    private Rigidbody2D shotRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        shotRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shotRigidBody.bodyType == RigidbodyType2D.Static)
        {
            return;
        }
        shotRigidBody.velocity = (Vector2)transform.up * shotSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }

        switch (collision.transform.tag)
        {
            //if the other rigid body has enemy tag try to deal damage
            case "Enemy": DamageEnemy(collision); break;
            // if it hit terrain make it linger for a bit and destroy it
            case "Terrain": TerrainHitResolve(); break;
            default: break;
        }
    }

    private void DamageEnemy(Collider2D enemy)
    {
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
        shotSpeed = 0;
        shotRigidBody.bodyType = RigidbodyType2D.Static;
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
        shotSpeed = speed;
    }
}
