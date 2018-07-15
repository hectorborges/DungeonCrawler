using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Collider damageCollider;

    public static bool isBlocking;
    public static bool isAttacking;

    bool block;
    bool attack;

    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Movement.isCrouching || Movement.isJumping || Movement.isRolling) return;
        RecieveInput();
        Attack();
        Block();
    }

    void RecieveInput()
    {
        attack = Input.GetKeyDown(KeyCode.Mouse0);
        block = Input.GetKey(KeyCode.Mouse1);
    }

    void Attack()
    {
        if(!isAttacking && attack)
        {
            isAttacking = true;
            int randomAttack = Random.Range(1, 4);
            animator.SetInteger("Attack", randomAttack);
            damageCollider.enabled = true;
        }
    }

    public void ResetAttack()
    {
        damageCollider.enabled = false;
        animator.SetInteger("Attack", 0);
        isAttacking = false;
    }

    void Block()
    {
       isBlocking = block;
       animator.SetBool("Blocking", block);
    }
}
