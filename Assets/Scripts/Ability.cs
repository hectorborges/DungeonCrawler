using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityName;
    public float abilityCooldown;

    bool onCooldown;

    public virtual void ActivateAbility() { }
    public virtual void DeActivateAbility() { }

    public virtual void TriggerCooldown()
    {
        if(!onCooldown)
        {
            onCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    public bool OnCooldown()
    {
        return onCooldown;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }
}
