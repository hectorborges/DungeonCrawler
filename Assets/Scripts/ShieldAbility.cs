using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability
{
    public GameObject shieldEffect;

    public override void ActivateAbility()
    {
        Combat.isImmune = true;
        shieldEffect.SetActive(true);
        animator.SetBool("Blocking", true);
    }

    public override void DeActivateAbility()
    {
        Combat.isImmune = false;
        shieldEffect.SetActive(false);
        animator.SetBool("Blocking", false);
    }
}
