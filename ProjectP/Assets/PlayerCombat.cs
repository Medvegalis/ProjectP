using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Update is called once per frame

    public Animator animator;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Attack();
        }

        void Attack()
        {
            animator.SetTrigger("Attack");
        }
    }
}
