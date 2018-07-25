using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityName;
    public float abilityCooldown;
    public enum InputType
    {
        OnPress,
        OnHold
    };

    public InputType inputType;

    public enum DeActivationType
    {
        Manual,
        Animation
    };

    public DeActivationType deActivationType;

    protected bool isActive;

    bool onCooldown;
    protected Animator animator;

    public void CacheAnimator(Animator _animator)
    {
        animator = _animator;
    }

    public virtual void ActivateAbility() { }
    public virtual void DeActivateAbility() { }
    public virtual void ActivateEffect(int effectIndex) { }

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
