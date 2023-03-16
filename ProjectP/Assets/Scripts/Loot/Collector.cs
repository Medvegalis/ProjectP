using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
