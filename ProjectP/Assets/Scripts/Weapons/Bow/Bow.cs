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
        if (Input.GetMouseButtonDown(0) && Time.timeScale !=0) 
        {
            Shoot();
        }
    }

    private void Shoot()
    { 
        //Gets the agle of the bow
        float angle = Utility.AngleTowardsMouse(BowPoint.position);
        //rotates 90 degress so it shots forward
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle+90f));
        
        var arrow = Instantiate(
            ArrowPrefab,
            BowPoint.position,
            rot
        );

        var arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetDamage(weaponDamage);
        arrowScript.SetProjectileSpeed(weaponProjectileSpeed);
    }
}
