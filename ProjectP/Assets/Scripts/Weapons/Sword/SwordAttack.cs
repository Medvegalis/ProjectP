using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour, IHasAttack
{
    [SerializeField]
    private float chainTimer = 3f;

    [SerializeField]
    private int damage = 5;

    PlayerControls controls;
    BoxCollider2D swordHitbox;

    public bool isInChainTimer;
    public bool canAttack;
    private int chainAttackIndex;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        swordHitbox = gameObject.GetComponent<BoxCollider2D>();
        isInChainTimer = false;
        chainAttackIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    IEnumerable UpdateChainTimer()
    {
        isInChainTimer = true;
        yield return new WaitForSeconds(chainTimer);

        yield return new WaitForSeconds(0.1f);
        isInChainTimer = false;
    }

    public void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        if (!AttackButtonPressed())
        {
            return;
        }

        if (!isInChainTimer)
        {
            chainAttackIndex = 0;
        }
        
        swordHitbox.enabled = true;
        UpdateChainTimer();

        switch (chainAttackIndex)
        {
            case 0: Attack1(); break;
            case 1: Attack2(); break;
            case 2: Attack3(); break;
        }
    }

    private void Attack1() 
    {
        float angle = Utility.AngleTowardsMouse(gameObject.transform.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle + 30f));

        gameObject.transform.rotation = rot;
        
    }

    private void Attack2()
    {
        float angle = Utility.AngleTowardsMouse(gameObject.transform.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

        gameObject.transform.rotation = rot;
    }

    private void Attack3()
    {
        float angle = Utility.AngleTowardsMouse(gameObject.transform.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));

        gameObject.transform.rotation = rot;
        chainAttackIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            //DamageEnemy(collision);
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

    public bool AttackButtonPressed()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        return true;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }
}
