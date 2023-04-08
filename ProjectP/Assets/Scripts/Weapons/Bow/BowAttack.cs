using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour, IHasAttack
{
    public GameObject ArrowPrefab;
    public Transform BowPoint;

    public Stat playerDamageStat;

    [SerializeField]
    private int weaponDamage = 2;
    [SerializeField]
    private float weaponProjectileSpeed = 10f;

    public bool canAttack;

    void Start()
    {
        
    }

    void Update()
    {
        Attack();
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

        //Gets the agle of the bow
        float angle = Utility.AngleTowardsMouse(BowPoint.position);
        //rotates 90 degress so it shots forward
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));

        var arrow = Instantiate(
            ArrowPrefab,
            BowPoint.position,
            rot
        );

        var arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetDamage(weaponDamage * playerDamageStat.currentValue);
        arrowScript.SetProjectileSpeed(weaponProjectileSpeed);
    }

    public bool AttackButtonPressed()
    {
        if (Time.timeScale == 0)
        {
            return false;
        }

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
