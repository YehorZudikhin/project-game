using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	// =//=//=//=//=//= Start of: CharacterController2D Class =//=//=//=//=//=
	[Header("Physics/Jump")]
	[SerializeField] private float m_JumpForce = 500f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	[Header("Dash")]
	[SerializeField] private float dashDistance = 10f;
	[SerializeField] private float dashSpeed = 40f;
	[SerializeField] private float afterDashSpeed = 10f;
	//private bool canDash = true;
	//private bool isDashing = false;
	//[SerializeField] private LayerMask enemeyLayer;
	//private Coroutine dashCoroutine;
	//private bool groundSliding = false;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private bool m_DoubleJumped = false;
	private bool m_wasCrouching = false;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }	// Creates a class BoolEvent, which inherits from UnityEvent, so that there is a UnityEvent that accepts a bool ... UnityEvent<bool>

	public BoolEvent OnCrouchEvent;	// Makes use of the newly defined BoolEvent class to create an OnCrouch-Event.

	public BoolEvent OnDashEvent;	// Adds an Event for the Dash move.
	//private bool m_wasDashing = false;

	// =====/////===== Start of: Unity Lifecycle Functions =====/////=====
	// Awake gets called if the Script even exists, it initializes whether the script is active or not. (can be used like a "high priority" constructor)

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
		
		if(OnDashEvent == null)
        {
			OnDashEvent = new BoolEvent();
        }
		
	}
	// FixedUpdate is a Unity function that is called at a fixed interval, typically synchronized with the physics engine, and is used for performing physics-related calculations and updates.
	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// Check for ground collision
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
				{
					OnLandEvent.Invoke();
					m_DoubleJumped = false; // Reset double jump flag on landing
				}
			}
		}
	}

	// =====/////===== End of: Unity Lifecycle Functions =====/////=====
	// ========== Start of: Character Control Functions ==========
	public void Move(float move, bool crouch, bool jump, bool dashing)
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
			} else
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

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

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
			// Player is on the ground and wants to jump
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_DoubleJumped = false; // Reset double jump flag
		}
		else if (!m_Grounded && !m_DoubleJumped && jump)
		{
			// Player is in the air and wants to double jump
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_DoubleJumped = true; // Set double jump flag
		}

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

	private IEnumerator DashCoroutine()
	{
		OnDashEvent.Invoke(true);
		float currentDistance = 0f;
		Vector2 dashDirection = m_FacingRight ? Vector2.right : Vector2.left;

		while (currentDistance < dashDistance)
		{
			float dashDistanceRemaining = dashDistance - currentDistance;
			float dashDistanceThisFrame = Mathf.Min(dashSpeed * Time.fixedDeltaTime, dashDistanceRemaining);

			// Move the character
			Vector2 targetVelocity = dashDirection * dashSpeed;
			m_Rigidbody2D.velocity = targetVelocity;

			currentDistance += dashDistanceThisFrame;

			yield return new WaitForFixedUpdate();
		}
		m_Rigidbody2D.velocity = dashDirection * afterDashSpeed;
        OnDashEvent.Invoke(false);
    }

    public void Dash()
	{
		StartCoroutine(DashCoroutine());
	}

	/*
	public void GroundSlide()
    {
		groundSliding = true;
	}

	public void GroundSlideEnd()
    {
		groundSliding = false;
    }
	*/
	// ========== End of: Character Control Functions ==========
	// =//=//=//=//=//= Start of: CharacterController2D Class =//=//=//=//=//=
}
