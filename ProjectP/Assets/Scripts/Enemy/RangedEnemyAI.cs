using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{

    private Transform target;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float nextWaypointDistace = 0.5f;
    [SerializeField] private float distanceToStop = 10f;
    [SerializeField] public GameObject bullet;

    [SerializeField] public Animator animator;

    [SerializeField]
    private AudioSource shootingAudioSourceMain;

    public SpriteRenderer GFX;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public bool hasEnteredRanged = false;
    [SerializeField] private float defaultShootCooldown = 3f;
    private float shootCooldown = 0;
    public bool isInRange = false;


    private Seeker seeker;
    private Rigidbody2D rb;
    private float transformToTargerDistance;

    // Start is called before the first frame update
    void Start()
    {
        
        Physics2D.IgnoreLayerCollision(10,11);// ignores collisions enemy and enemyProjectile

        seeker = GetComponent<Seeker>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
       // GFX = GetComponentInParent<SpriteRenderer>();//NOT SAFE but will do for now sorry

        InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);
    }

    private void Update()
    {
        Shoot();

        if (isInRange)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = new Vector2(-direction.x, -direction.y) * speed/2;
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.normalized.magnitude);
            Console.WriteLine("Runing away");

        }
        else 
        { 
        
        }

    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (hasEnteredRanged)
            {     
                seeker.StartPath(rb.position, target.position, OnPathComplete);

            }
        }
    }
    private void Shoot()
    {
        if (isInRange)
        {
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            transform.up = direction;

            if (shootCooldown <= 0)
            {
                shootingAudioSourceMain.Play();
                Instantiate(bullet, transform.position, transform.rotation);
                shootCooldown = defaultShootCooldown;
            }
            else
            {
                shootCooldown -= Time.deltaTime;
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            hasEnteredRanged = true;
            isInRange = true;

        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            isInRange = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (!hasEnteredRanged)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
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

        if (!isInRange)
            rb.velocity = new Vector2(direction.x, direction.y) * speed;

        float distace = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distace < nextWaypointDistace)
        {
            currentWaypoint++;
        }

        if(!isInRange)
            if (rb.velocity.x > 0f)
            {
                GFX.flipX = false;
            }
            else if (rb.velocity.x < 0f)
            {
                GFX.flipX = true;
            }
    }
}
