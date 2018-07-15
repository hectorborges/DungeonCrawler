using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiColliderAbility : Ability
{
    public int damage;
    public Collider[] damageColliders;
    public float activationFrequency;
    public float extraEffectWaitTime;
    public GameObject abilityEffect;

    public override void ActivateAbility()
    {
        StartCoroutine(ActivationFrequency());
    }

    IEnumerator ActivationFrequency()
    {
        abilityEffect.SetActive(true);
        for (int i = 0; i < damageColliders.Length; i++)
        {
            damageColliders[i].enabled = true;
            yield return new WaitForSeconds(activationFrequency);
            damageColliders[i].enabled = false;
        }
        yield return new WaitForSeconds(extraEffectWaitTime);
        abilityEffect.SetActive(false);
    }
}
