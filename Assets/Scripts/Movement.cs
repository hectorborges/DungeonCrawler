using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float runSpeed;
    public float sprintSpeed;

    [Space]
    public float runToIdleSpeed;
    public float idleToRunSpeed;
    public float runToSprintSpeed;
    public float sprintToRunSpeed;

    [Space]
    public float rotationSpeed;

    [Space, Header("Jump Variables")]
    public float jumpForce;
    public float distanceToGround;
    public LayerMask groundLayer;

    [Space, Header("Crouch Variables")]
    public float crouchSpeed;
    public float resizeSpeed;
    public CapsuleCollider crouchCollider;

    [Space, Header("Roll Variables")]
    public float rollForce;
    public float rollLength;

    //Input
    float moveX;
    float moveY;

    bool sprint;
    bool jump;
    bool crouch;
    bool roll;

    //Gameplay
    Vector3 movement;
    Vector3 airMovement;
    Quaternion direction;
    Quaternion rollDirection;

    float speed;
    float jumpSpeed;

    float baseRunSpeed;
    float baseSprintSpeed;

    float colliderHeight;

    float rollStartTime;

    public static bool isJumping;
    public static bool isCrouching;
    public static bool isRolling;

    Animator animator;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        colliderHeight = crouchCollider.height;
        baseRunSpeed = runSpeed;
        baseSprintSpeed = sprintSpeed;
    }

    private void Update()
    {
        if (Combat.isImmune)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        RecieveInput();
        Move();
        Jump();
        Crouch();
        Roll();
    }

    void RecieveInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        sprint = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
        crouch = Input.GetKeyDown(KeyCode.LeftControl);
        roll = Input.GetKeyDown(KeyCode.LeftAlt);
    }

    void Move()
    {
        if(!isRolling)
        {
            movement = new Vector3(moveX, 0, moveY);
            movement = Vector3.ClampMagnitude(movement, 1);
            movement *= speed * Time.deltaTime;
        }

       // if(!isJumping)
            movement = Camera.main.transform.TransformDirection(movement);

        if(!isRolling)
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (isCrouching)
            sprint = false;

        if (movement == Vector3.zero && speed > 0)
            speed -= runToIdleSpeed * Time.deltaTime;
        else if (!sprint && speed < runSpeed)
            speed += idleToRunSpeed * 2 * Time.deltaTime;
        else if (!isCrouching && sprint && speed < sprintSpeed)
            speed += runToSprintSpeed * Time.deltaTime;
        else if (!sprint && speed > runSpeed)
            speed -= sprintToRunSpeed * Time.deltaTime;

        animator.SetFloat("Speed", speed);
        animator.SetFloat("AirVelocity", rb.velocity.y);

        if (movement == Vector3.zero || isJumping || isRolling) return;

        //if (!Combat.isAttacking)
            direction = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
        //else
        //    direction = Quaternion.Euler(TargetManager.instance.closestTarget.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (isCrouching || isRolling) return;
        if (!isJumping && rb.velocity.y > 6 || !isJumping && rb.velocity.y < -6)
        {
            isJumping = true;
            animator.SetBool("Jumping", true);
            animator.SetBool("Movement", false);
        }

        if(jump && !isJumping)
        {
            isJumping = true;
            animator.SetBool("Jumping", true);
            animator.SetBool("Movement", false);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        if (isJumping && IsGrounded())
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Movement", true);
            isJumping = false;
        }
    }

    void Crouch()
    {
        if (isJumping || isRolling) return;

        if (crouch)
            isCrouching = !isCrouching;

        if (isCrouching)
        {
            animator.SetBool("Crouching", true);
            runSpeed = crouchSpeed;
            sprintSpeed = crouchSpeed;
            crouchCollider.height = Mathf.Lerp(crouchCollider.height, colliderHeight / 2, resizeSpeed * Time.deltaTime);
            crouchCollider.center = Vector3.Lerp(crouchCollider.center, new Vector3(0, .4375f, 0), resizeSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Crouching", false);
            runSpeed = baseRunSpeed;
            sprintSpeed = baseSprintSpeed;
            crouchCollider.height = Mathf.Lerp(crouchCollider.height, colliderHeight, resizeSpeed * Time.deltaTime);
            crouchCollider.center = Vector3.Lerp(crouchCollider.center, new Vector3(0, .875f, 0), resizeSpeed * Time.deltaTime);
        }
    }

    void Roll()
    {
        if (isJumping || isCrouching || movement == Vector3.zero) return;

        if(roll && !isRolling)
        {
            isRolling = true;
            animator.SetBool("Rolling", true);
            rollStartTime = Time.time;
            rollDirection = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
        }

        if(isRolling)
        {
            rb.velocity = transform.forward * rollForce * Time.deltaTime;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rollDirection, rotationSpeed * Time.deltaTime);

            if (Time.time - rollStartTime >= rollLength)
            {
                isRolling = false;
                animator.SetBool("Rolling", false);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);
    }
}
