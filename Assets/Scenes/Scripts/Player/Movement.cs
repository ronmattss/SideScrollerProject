using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


namespace SideScrollerProject
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        LayerMask lmWalls;
        [SerializeField]
        float fJumpVelocity = 5;

        Rigidbody2D rigid;

        float fJumpPressedRemember = 0;
        [SerializeField]
        float fJumpPressedRememberTime = 0.2f;
        float dashCD = 1;
        float dashCurrent;
        float fGroundedRemember = 0;
        [SerializeField]
        float fGroundedRememberTime = 0.25f;

        [SerializeField]
        float fHorizontalAcceleration = 1;
        [SerializeField]
        [Range(0, 1)]
        float fHorizontalDampingBasic = 0.5f;
        [SerializeField]
        [Range(0, 1)]
        float fHorizontalDampingWhenStopping = 0.5f;
        [SerializeField]
        [Range(0, 1)]
        float fHorizontalDampingWhenTurning = 0.5f;

        [SerializeField]
        [Range(0, 1)]
        float fCutJumpHeight = 0.5f;

        //  [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = 1f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
        [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
        public static Movement instance;
        const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        int jumpCount = 0;
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        public Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public float dashSpeed;
        public float smallMovementSpeed;
        private float dashTime;
        public float startDashTime;
        public int dashCoolDown = 1;
        public int dashCoolDownStart;
        public bool isDashing = false;
        public bool isAttackForward = false;
        public bool isOnOneWayPlatform = false;
        public bool canGoDown = false;
        Animator animator;
        public SpriteRenderer playerSprite;
        public Sprite sprite;
        public Material dashMaterial;
        public ParticleSystem dashAfterImage;
        public Vector2 playerVelocity;
        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        public BoolEvent OnCrouchEvent;
        private bool m_wasCrouching = false;
        private bool doubleJump = false;
        public int dashCount = 2;
        public int availableDash;
        private AudioSource source;
        public AudioClip dashSound;
        public AudioClip jumpSound;

        public float jumpForce;
        public bool isJumping;
        public bool jump;
        private float jumpTimeCounter;
        public float jumpTime;
        public float xMovement;
        //bool wasGrounded;

        private void Awake()
        {
            dashCurrent = dashCD;
            instance = this;
            source = GetComponent<AudioSource>();
            dashTime = startDashTime;
            dashCoolDownStart = dashCoolDown;
            availableDash = dashCount;
            animator = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();


            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }

        private void Update()
        {
            Jump();
        }
        public void Jump()
        {
            playerVelocity = m_Rigidbody2D.velocity;
            fGroundedRemember -= Time.deltaTime;
            if (m_Grounded)
            {
                fGroundedRemember = fGroundedRememberTime;
                // m_Rigidbody2D.velocity = new Vector2(xMovement, 1 * jumpForce);
                // isJumping = true;
                // jump = true;
                // jumpTimeCounter = jumpTime;


            }
            fJumpPressedRemember -= Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                fJumpPressedRemember = fJumpPressedRememberTime;
            }
            if (Input.GetButtonUp("Jump"))
            {
                if (m_Rigidbody2D.velocity.y > 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * fCutJumpHeight);
                }
            }

            if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
            {
                fJumpPressedRemember = 0;
                fGroundedRemember = 0;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, fJumpVelocity);
                animator.SetBool("Jumping", true);
                animator.SetBool(AnimatorParams.Attacking.ToString(), false);
                PlayerParticleSystemManager.instance.StartParticle(PlayerParticles.JumpDust);
                source.clip = jumpSound;
                source.Play();
            }
            animator.SetBool("IsGrounded", m_Grounded);
        }

        private void FixedUpdate()
        {


            bool wasGrounded = m_Grounded;
            //if (jumpCount == 2)
            m_Grounded = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    //                Debug.Log(colliders[i].gameObject.transform.name);
                    m_Grounded = true;
                    doubleJump = false;
                    jumpCount = 0;
                    // animator.SetBool("Jumping",false);
                    //  OnLandEvent.Invoke();
                    if (!wasGrounded && m_Rigidbody2D.velocity.y <= 0)
                        OnLandEvent.Invoke();
                }
                if (colliders[i].gameObject.GetComponent<PlatformEffector2D>() != null)
                {
                    Debug.Log("is on a platform");
                    isOnOneWayPlatform = true;
                    if (canGoDown)
                    {
                        colliders[i].gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
                    }
                }
                else
                {
                    isOnOneWayPlatform = false;
                    canGoDown = false;
                }
                if (m_Grounded && !wasGrounded)
                {
                    animator.SetBool("Jumping", false);
                    // jumpDust.Play();
                }

            }
            //       Jump();
        }


        public void Move(float move, float dashSpeed, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {

                Vector2 movement = m_Rigidbody2D.velocity;
                float fHorizontalVelocity = m_Rigidbody2D.velocity.x;
                fHorizontalVelocity += (move);
                if (Mathf.Abs(move) < 0.01f)
                    fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
                else if (Mathf.Sign(move) != Mathf.Sign(fHorizontalVelocity))
                    fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
                else
                    fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);

                m_Rigidbody2D.velocity = new Vector2(fHorizontalVelocity, m_Rigidbody2D.velocity.y);

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
            if (isAttackForward)
                SmallMovement();

        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        public void SmallDash()
        {

            if (isDashing && dashCurrent <= 0)
            {
                // playerSprite.sprite = sprite;
                animator.SetBool("isDashing", isDashing);
                dashMaterial.SetTexture("_MainTex", sprite.texture);
                dashAfterImage.GetComponent<ParticleSystemRenderer>().material = dashMaterial;
                dashAfterImage.GetComponent<ParticleSystemRenderer>().flip = new Vector2(-this.transform.localScale.x, 0);
                PlayerParticleSystemManager.instance.StopAllParticles();
                PlayerParticleSystemManager.instance.StartParticle(PlayerParticles.DashImage, PlayerParticles.JumpDust);
                // source.clip = dashSound;
                //  source.Play();

                //dashAfterImage.Play();

                if (dashTime <= 0)
                {
                    dashTime = startDashTime;
                    dashCurrent = dashCD;
                    isDashing = false;
                    animator.SetBool("isDashing", isDashing);
                    dashAfterImage.Stop();
                    //  m_Rigidbody2D.velocity = Vector2.zero;


                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (this.transform.localScale.x > 0)
                    {
                        m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                    }
                    else if (this.transform.localScale.x < 0)
                    {
                        m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                    }

                }




            }
            else
            {
                isDashing = false;
                dashCurrent -= Time.deltaTime;
            }
        }

        public void SmallMovement()
        {

            m_Rigidbody2D.AddForce(new Vector2(PlayerManager.instance.playerSpriteDirection * smallMovementSpeed, 1), ForceMode2D.Impulse);
            Debug.Log("Small Movement");
            isAttackForward = false;
        }

        public void Recharge()
        {
            StartCoroutine(RechargeDash());
        }
        IEnumerator RechargeDash()
        {
            yield return new WaitForSeconds(dashCoolDown);
            availableDash = dashCount;
            yield return null;
        }
        public void DashSound()
        {
            source.clip = dashSound;
            source.Play();
        }

        /// <summary>
        /// Callback to draw gizmos that are pickable and always drawn.
        /// </summary>
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
        }
        public int GetFallVelocity()
        {
            return (int)m_Rigidbody2D.velocity.y;
        }
        public void PlayFootStep(string sound)
        {
            AudioManager.instance.Play(sound);
        }
    }

}