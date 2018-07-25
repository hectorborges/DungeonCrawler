using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMeleeAbility : Ability
{
    public int damage;
    public Collider[] damageColliders;
    public GameObject[] damageEffects;
    public float comboResetTimer;

    int currentAttack;
    Coroutine combo;

    public override void ActivateAbility()
    {
        if (currentAttack < damageColliders.Length)
            currentAttack++;
        else
            currentAttack = 1;

        isActive = true;
        damageColliders[currentAttack - 1].enabled = true;

        animator.SetInteger("Attack", currentAttack);

        if (combo != null)
            StopCoroutine(combo);
        
            combo = StartCoroutine(Combo());
    }

    public override void ActivateEffect(int effectIndex)
    {
        damageEffects[effectIndex - 1].SetActive(true);
    }

    public override void DeActivateAbility()
    {
        animator.SetInteger("Attack", 0);
        for (int i = 0; i < damageColliders.Length; i++)
        {
            damageColliders[i].enabled = false;
            damageEffects[i].SetActive(false);
        }
        isActive = false;
    }

    IEnumerator Combo()
    {
        yield return new WaitForSeconds(comboResetTimer);
        print("Reset Combo");
        currentAttack = 0;
    }
}
