using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityName;
    public float abilityCooldown;

    protected bool isActive;

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

    public bool IsActive()
    {
        return isActive;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }
}
