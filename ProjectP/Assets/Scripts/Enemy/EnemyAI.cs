using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistace = 3f;

    private SpriteRenderer GFX;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private bool hasEnteredRanged = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        GFX = GetComponentInChildren<SpriteRenderer>();//NOT SAFE but will do for now sorry

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            if(hasEnteredRanged)
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }


    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            hasEnteredRanged = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
            return;

        if(!hasEnteredRanged)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        rb.velocity = new Vector2(direction.x, direction.y) * speed;

        float distace = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distace < nextWaypointDistace)
        {
            currentWaypoint++;
        }

        if(rb.velocity.x > 0f)
        {
            GFX.flipX = false;
        }
        else if(rb.velocity.x < 0f)
        {
            GFX.flipX = true;
        }
    }
}
