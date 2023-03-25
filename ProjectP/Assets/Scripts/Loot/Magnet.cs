using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// For moving the collectable to the player
/// </summary>
public class Magnet : MonoBehaviour
{
 private void OnTriggerStay2D(Collider2D coll)
    {
        ICollectable collectable = coll.GetComponent<ICollectable>();

        if(collectable != null)
        {
            collectable.SetTarget(transform.parent.position);
        }
    }
}
