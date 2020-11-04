using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Only used if object doesn't have the BattleStats script")]
    private float m_JumpForce = 1f;                                             // Amount of force added when the player jumps.
    private float m_RunSpeed = 60f;                                             // Amount of speed applied to the run movement
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private float m_AirControl = 0.3f;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;


    float lastYPosition;
    private BattleStats battleStats;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private bool isFloating;
    

    private void Awake()
    {
        battleStats = GetComponent<BattleStats>();

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        
        updateValues();
    }

    private void Update()
    {

        lastYPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jump)
    {
        move *= m_RunSpeed;
        


        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl != 0)
        {
        

            if (!isFloating)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity;

                if (!m_Grounded)    // If in the air, adjust the speed
                {
                    if (m_Rigidbody2D.velocity.x + move * 10f * m_AirControl > move * 10f && move * 10f >= 0)
                    {
                        targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    }
                    else if (m_Rigidbody2D.velocity.x + move * 10f * m_AirControl < move * 10f && move * 10f < 0)
                    {
                        targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                    }
                    else
                    {
                        targetVelocity = new Vector2(m_Rigidbody2D.velocity.x + move * 10f * m_AirControl, m_Rigidbody2D.velocity.y);
                    }
                }
                else
                {
                    targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
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
            Debug.Log("Ennemy jump!");
            Jump();
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
    }


    private IEnumerator Freeze()
    {
        yield return new WaitForSeconds(0.2f);
        isFloating = false;

    }
}
