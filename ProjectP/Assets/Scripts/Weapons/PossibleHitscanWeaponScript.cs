using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleHitscanWeaponScript : MonoBehaviour
{
    [SerializeField] private Transform bowPoint;
    [SerializeField] private GameObject arrowTrail;
    [SerializeField] private float weaponRange = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(
                bowPoint.position,
                transform.right,
                weaponRange
                );

            var trail = Instantiate(
                arrowTrail,
                bowPoint.position,
                transform.rotation
                );

            var trailScript = trail.GetComponent<ArrowTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
            }
            else
            {
                var endPosition = bowPoint.position + transform.right * weaponRange;
                trailScript.SetTargetPosition(endPosition);
            }
        }
    }
}
