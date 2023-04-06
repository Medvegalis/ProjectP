using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Collectable : MonoBehaviour, ICollectable
{
    public static event Action<int> OnCollected; // An event for coin collection 
    Rigidbody2D rb; // Coin's rigid body
    bool hasTarget = false; // If has a target to follow
    Vector3 targetPos; // The position of a target that need to be followed
    public int value;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(hasTarget)
        {
            Vector2 targetDirection = (targetPos - transform.position).normalized; // The direction to the target
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * 8f; // Adds velocity to the targetDirection
        }
    }

    /// <summary>
    /// Method destroys the collectable and invokes onCollected event
    /// </summary>
    /// <param name="value"> The value of the collectable </param>
    public void Collect()
    {
        Destroy(gameObject);
        OnCollected.Invoke(value);
    }

    /// <summary>
    /// Sets the desired target
    /// </summary>
    /// <param name="position"> Position of the targer</param>
    public void SetTarget(Vector3 position)
    {
        targetPos = position;
        hasTarget = true;
    }

}



