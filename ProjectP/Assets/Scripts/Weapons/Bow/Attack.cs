using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform BowPoint;

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
        if (Time.timeScale == 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) 
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
