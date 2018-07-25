using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Ability primaryAbility;
    public Ability secondaryAbility;
    public GameObject shieldEffect;

    public static bool isImmune;
    public static bool isAttacking;

    bool secondaryAttack;
    bool primaryAttack;

    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        primaryAbility.CacheAnimator(animator);
        secondaryAbility.CacheAnimator(animator);
    }

    private void Update()
    {
        if (Movement.isCrouching || Movement.isJumping || Movement.isRolling) return;
        RecieveInput();
        PrimaryAttack();
        SecondaryAttack();
    }

    void RecieveInput()
    {
        switch (primaryAbility.inputType)
        {
            case Ability.InputType.OnPress:
                primaryAttack = Input.GetKeyDown(KeyCode.Mouse0);
                break;
            case Ability.InputType.OnHold:
                primaryAttack = Input.GetKey(KeyCode.Mouse0);
                break;
        }

        switch (secondaryAbility.inputType)
        {
            case Ability.InputType.OnPress:
                secondaryAttack = Input.GetKeyDown(KeyCode.Mouse1);
                break;
            case Ability.InputType.OnHold:
                secondaryAttack = Input.GetKey(KeyCode.Mouse1);
                break;
        }

        if (primaryAbility.deActivationType == Ability.DeActivationType.Manual && Input.GetKeyUp(KeyCode.Mouse0))
            primaryAbility.DeActivateAbility();

        if (secondaryAbility.deActivationType == Ability.DeActivationType.Manual && Input.GetKeyUp(KeyCode.Mouse1))
            secondaryAbility.DeActivateAbility();
    }

    void PrimaryAttack()
    {
        if(!isAttacking && primaryAttack)
        {
            if(!primaryAbility.OnCooldown())
            {
                primaryAbility.ActivateAbility();
                primaryAbility.TriggerCooldown();
            }
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }

    public void ResetPrimaryAttack()
    {
        primaryAbility.DeActivateAbility();
    }

    void SecondaryAttack()
    {
        if (!isAttacking && secondaryAttack)
        {
            if (!secondaryAbility.OnCooldown())
            {
                secondaryAbility.ActivateAbility();
                secondaryAbility.TriggerCooldown();
            }
        }
    }

    public void ResetSecondaryAttack()
    {
        primaryAbility.DeActivateAbility();
        isAttacking = false;
    }
}
