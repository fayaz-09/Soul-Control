using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public CharacterController controller;
    public Player myPlayer;
    private Animator playerAnims;
    public float speed;
    public float smoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravity = -9.18f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDist = 0.1f;
    public LayerMask groundMask;

    public float jumpHeight;
    [SerializeField] bool isGrounded;
    bool canMove;

    public float attackCooldown = 2f;
    private float nextAttackTime = 0.5f;
    [SerializeField] public int noOfClicks = 0;
    [SerializeField] float lastClickTime = 0f;
    float maxComboDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6;
        jumpHeight = 3f;
        playerAnims = GetComponent<Animator>();
        canMove = true;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        
        if (canMove)
        {
            if (direction.magnitude >= 0.1f)
            {
                playerAnims.SetBool("isMoving", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
                
            }
            else if (direction.magnitude < 0.1f)
            {
                playerAnims.SetBool("isMoving", false);
            }
        }
        else
        {
            playerAnims.SetBool("isMoving", false);  
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (playerAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 2f && playerAnims.GetCurrentAnimatorStateInfo(0).IsName("RetargetAttack"))
        {
            playerAnims.SetBool("isAttacking", false);
        }
        if (playerAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 2f && playerAnims.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2"))
        {
            playerAnims.SetBool("attack2", false);
        }
        if (playerAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.55f && playerAnims.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack3"))
        {
            playerAnims.SetBool("attack3", false);
            noOfClicks = 0;
        }

        lastClickTime += Time.deltaTime;

        if (lastClickTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if(lastClickTime > nextAttackTime)
        {
            if (Input.GetButton("Fire1"))
            {
                lastClickTime = 0f;
                attackOnInput();
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    void attackOnInput()
    {
        //lastClickTime = Time.time;
        if(noOfClicks == 0)
        {
            noOfClicks++;
        }
        if(noOfClicks == 1)
        {
            if (!playerAnims.GetCurrentAnimatorStateInfo(0).IsName("RetargetAttack"))
            { 
                playerAnims.SetBool("isAttacking", true); 
            }
            else if(playerAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.85f && playerAnims.GetCurrentAnimatorStateInfo(0).IsName("RetargetAttack"))
            {
                noOfClicks++;
            }
        }
        //noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks == 2)
        {
            if (!playerAnims.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2"))
            {
                playerAnims.SetBool("isAttacking", false);
                playerAnims.SetBool("attack2", true);
            }
            else if (playerAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.85f && playerAnims.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2"))
            {
                noOfClicks++;
            }
        }

        if (noOfClicks == 3)
        {
            if (!playerAnims.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack3"))
            {
                playerAnims.SetBool("attack2", false);
                playerAnims.SetBool("attack3", true);
            }
            //else if (playerAnims.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f && playerAnims.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack3"))
            //{
            //    noOfClicks = 0;
            //}
        }

    }

    void setMove()
    {
        if(canMove)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    void setAttackAnim()
    {
        playerAnims.SetBool("isAttacking", false);
    }
}
