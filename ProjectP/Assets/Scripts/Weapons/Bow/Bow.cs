using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform BowPoint;

    [SerializeField] 
    private float weaponRange = 20f;
    //range is unused atm but possible to make limited range
    //for projectile weapons
    [SerializeField]
    private int weaponDamage = 2;
    [SerializeField]
    private float weaponProjectileSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        CheckInputAndShoot();
    }

    private void CheckInputAndShoot() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    private void Shoot()
    { 
        var arrow = Instantiate(
            ArrowPrefab,
            BowPoint.position,
            transform.rotation
        );

        var arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetDamage(weaponDamage);
        arrowScript.SetProjectileSpeed(weaponProjectileSpeed);
    }
}
