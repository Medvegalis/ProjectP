using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour, IHasAttack
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] swordAttacks; 

    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float knockback = 5f;

    private PlayerControls controls;
    private BoxCollider2D swordHitbox;
    private AudioSource audioSource;

    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controls = new PlayerControls();
        swordHitbox = gameObject.GetComponent<BoxCollider2D>();
        for (int i = 0; i < swordAttacks.Length; i++)
        {
            var script = swordAttacks[i].GetComponent<AttackHit>();
            script.setAttackDamage(damage);
            script.setKnockback(knockback);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        if (!AttackButtonPressed())
        {
            return;
        }

        var animator = player.GetComponent<Animator>();
        audioSource.Play();
        animator.SetTrigger("AttackWithSword");
    }

    public bool AttackButtonPressed()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        return true;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }
}
