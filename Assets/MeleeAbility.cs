using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAbility : Ability
{
    public int damage;
    public Collider damageCollider;

    public override void ActivateAbility()
    {
        damageCollider.enabled = true;
    }

    public override void DeActivateAbility()
    {
        damageCollider.enabled = false;
    }
}
