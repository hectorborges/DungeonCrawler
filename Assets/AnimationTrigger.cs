using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Ability ability;

    public void ActivateAbility(int abilityIndex)
    {
        ability.ActivateEffect(abilityIndex);
    }
}
