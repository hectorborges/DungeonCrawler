using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Image healthBar;

    public override void TookDamage(int damage)
    {
        if (Combat.isBlocking) return;
        health -= damage;
        animator.SetTrigger("Hit");

        healthBar.fillAmount = (float)health / (float)maxHealth;

        if (!isDead && health <= 0)
        {
            isDead = true;
            Died();
        }
    }
}
