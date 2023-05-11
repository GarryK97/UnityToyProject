using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyControl : MonoBehaviour
{   
    public GameObject PlayerCamera;
    Rigidbody rb;
    Animator _animator;
    CapsuleCollider PlayerCollider;
    public float walk_speed;
    public float sprint_speed;
    public float jumpForce;
    public float jumpBreakMultiplier;
    public float sprintStamina;
    public float JumpStamina;
    public float minFallHeight;
    public float FallDamageMultiplier;
    private bool isJumpPressed = false;
    private bool isMidAir = false;
    private bool isFalling = false;
    private float MidAirTime;
    private Vector3 prev_position;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        PlayerCollider = GetComponent<CapsuleCollider>();
        prev_position = rb.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }

        // Heal (Temp)
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerState.HealPlayer(PlayerState.PLAYERMAXHP);
        }
        // Heal (Temp) END --

        if (!isMidAir)
        {
            prev_position = rb.position;
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        // updateAnimation(xMove, zMove);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 offset = PlayerCamera.transform.forward;
            offset.y = 0;
            transform.LookAt(transform.position + offset);
        }
    }

	// 계산된 방향으로 물리적인 이동 구현
    private void FixedUpdate()
    {
        if (isJumpPressed)
        {   
            Jump();
        }
        
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        if (!isMidAir)
        {
            if (PlayerState.isRunning)
            {
                Run(zMove);
            }
            else
            {
                Walk(xMove, zMove);
            }
        }
        
    }

    // private void OnCollisionEnter(Collision collision)
    // {   
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //     {   
    //         float fallHeight = -(rb.position - prev_position).y;
    //         // Debug.Log(fallHeight);
    //         if (fallHeight > minFallHeight)
    //         {
    //             PlayerState.takeDamage(fallHeight * fallHeight * FallDamageMultiplier);
    //         }

    //         isMidAir = false;
    //         isFalling = false;
    //         _animator.SetTrigger("GroundHit");
    //     }
            
    // }

    // private void OnCollisionStay(Collision collision)
    // {   
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //     {
    //         isMidAir = false;
    //         _animator.SetBool("isFalling", isFalling);
    //         MidAirTime = 0;
    //     }
            
    // }

    // private void OnCollisionExit(Collision collision)
    // {   
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //     {
    //         isMidAir = true;
    //     }
    // }

    private void Walk(float xMove, float zMove)
    {
        Vector3 newVel = new Vector3(xMove* walk_speed, rb.velocity.y, zMove* walk_speed);

        newVel = rb.transform.TransformDirection(newVel);   // 플레이어가 보고있는 방향으로 벡터 변환
        rb.velocity = newVel;
    }

    private void Run(float zMove)
    {
        PlayerState.s_UseStamina(sprintStamina);
        Vector3 newVel = new Vector3(0, rb.velocity.y, zMove* sprint_speed);

        newVel = rb.transform.TransformDirection(newVel);   // 플레이어가 보고있는 방향으로 벡터 변환
        rb.velocity = newVel;
    }

    private void Jump()
    {   
        isJumpPressed = false;

        if (!PlayerState.isMidAir && PlayerState.s_IsStaminaAvailable())
        {   
            PlayerState.s_UseStamina(JumpStamina);
            rb.velocity = rb.velocity * jumpBreakMultiplier;    // Make Player slow when jumping for realism
            rb.AddForce(transform.up * rb.mass * jumpForce, ForceMode.Impulse); // JUMP!!
            _animator.SetTrigger("jumpPressed");
            PlayerState.isMidAir = true;
        }
    }
}
