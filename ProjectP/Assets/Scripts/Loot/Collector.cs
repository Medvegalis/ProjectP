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

            switch(coll.transform.name)
            {
                case string name when name.Contains("Gem"):
                    collectable.Collect(10);
                break;

                case string name when name.Contains("Coin"):
                    collectable.Collect(1);
                break;

                default:
                    break;
            }
        }
    }
}
