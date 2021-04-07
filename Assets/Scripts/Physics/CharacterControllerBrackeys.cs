using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControllerBrackeys : MonoBehaviour
{
    [SerializeField][Tooltip("Only used if object doesn't have the BattleStats script")]
    private float m_JumpForce = 20f;                                            // Amount of force added when the player jumps.
    private float m_RunSpeed = 60f;                                             // Amount of speed applied to the run movement
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private float m_AirControl = 0.8f;                         // Coefficient at which a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is walled to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is walled.
    [SerializeField] private LayerMask m_WhatIsWall;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_WallCheck;
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_Walled;            // Whether or not the player is walled.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    // walljump
    public bool canWallJump = false;

    // dash
    public float DashSpeed;

    // jump
    private float jumpTimeCounter;
    public float jumpTime;

    // double jump
    private bool canDoubleJump = true;
    private bool isJumpUsed = false;
    

    Animator anim;
    float lastYPosition;
    private BattleStats battleStats;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private bool isFloating;

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

  

    private void Awake()
    {
        battleStats = GetComponent<BattleStats>();

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        anim = transform.GetChild(0).GetComponent<Animator>();
        updateValues();
    }

    private void Update()
    {
        //Change animation to fall
        if (transform.position.y < lastYPosition)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }

        lastYPosition = transform.position.y;

        if (battleStats.currentHP <=0)
        {
            anim.SetBool("isDead", true);
        }

        if(jumpTimeCounter > 0.0f)
        {
            jumpTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        bool wasWalled = m_Walled;
        m_Walled = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                isJumpUsed = false;
                anim.SetBool("isGrounded", true);
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedRadius, m_WhatIsWall);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].gameObject != gameObject)
            {
                m_Walled = true;
                anim.SetBool("isWalled", true);
                if (!wasWalled)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump, bool dash)
    {
        move *= m_RunSpeed;

        crouch = false; //Disable Crouch pour l'instant

        // If crouching, check to see if the character can stand up
        if (crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl != 0)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            if (!isFloating)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity;

                if (!m_Grounded)    // If in the air, adjust the speed
                {
                    if(m_Rigidbody2D.velocity.x + move * 10f * m_AirControl > move * 10f && move * 10f >= 0)
                    {
                        targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    }
                    else if (m_Rigidbody2D.velocity.x + move * 10f * m_AirControl < move * 10f && move * 10f <= 0)
                    {
                        targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    }
                    else
                    {
                        targetVelocity = new Vector2(m_Rigidbody2D.velocity.x + move * 10f * m_AirControl , m_Rigidbody2D.velocity.y);
                    }
                }
                else
                {
                    if (m_Rigidbody2D.velocity.x + move * 10f > move * 10f && move * 10f >= 0)
                    {
                        targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    }
                    else if (m_Rigidbody2D.velocity.x + move * 10f < move * 10f && move * 10f <= 0)
                    {
                        targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    }
                    else
                    {
                        targetVelocity = new Vector2(m_Rigidbody2D.velocity.x + move * 10f, m_Rigidbody2D.velocity.y);
                    }
                }

                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else
            {
                m_Rigidbody2D.velocity = Vector3.zero;
            }


            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            //m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
            jumpTimeCounter = 0.30f;
            Jump();
        } 

        // DOUBLE JUMP
        if(!m_Grounded && canDoubleJump && jump && !isJumpUsed && jumpTimeCounter <= 0)
        {
            isJumpUsed = true;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce/1.5f, ForceMode2D.Impulse);
            Jump();
        }

        // WALLJUMP
        if (m_Walled && jump && canWallJump == true)
        {
            // Add a vertical force to the player.
            //m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
            Jump();
        }

        // If the player should dash
        if (dash && m_FacingRight)
        {
            m_Rigidbody2D.velocity = Vector2.right * DashSpeed;
        }
        else if (dash && !m_FacingRight)
        {
            m_Rigidbody2D.velocity = Vector2.left * DashSpeed;
        }
    }

    public void StopVelocity()
    {
        StopAllCoroutines();
        isFloating = true;
        m_Rigidbody2D.velocity = Vector2.zero;
        StartCoroutine(Freeze());

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Change the player direction by rotating his model
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.y += 180;
        transform.rotation = Quaternion.Euler(rotationVector);

    }


    public void updateValues()
    {
        if (battleStats)
        {
            m_JumpForce = battleStats.finalJumpForce();
            m_RunSpeed = battleStats.finalRunSpeed();
        }
    }

    public void Jump()
    {
        m_Grounded = false;
        anim.SetBool("isGrounded", false);
        anim.SetTrigger("Jumping");
    }


    private IEnumerator Freeze()
    {
        yield return new WaitForSeconds(0.2f);
        isFloating = false;

    }
}
