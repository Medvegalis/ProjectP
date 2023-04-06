using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hadles the collectable type recognition and value of the collectable
/// </summary>
public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        ICollectable collectable = coll.GetComponent<ICollectable>();

        if(collectable != null)
        {
           collectable.Collect();
        }
    }
}
