using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    [HideInInspector] public bool isDead;
    protected int health;
    protected Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health = maxHealth;
    }

    public virtual void TookDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");

        if(!isDead && health <= 0)
        {
            isDead = true;
            Died();
        }
    }

    protected void Died()
    {
        animator.SetBool("Died", true);
    }
}
