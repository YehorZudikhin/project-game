using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // =//=//=//=//=//= Start of: PlayerMovement Class =//=//=//=//=//=
    public CharacterController2D controller;
    public Animator animator;
    public PlayerAnimationHelper animationHelper;

    public CoinManager cm; 
    private bool m_CollectingCoin = false;
    
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private bool m_DoubleJumped = false;

    // =====/////===== Start of: Unity Lifecycle Functions =====/////=====
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            if (controller.m_Grounded)
            {
                // Player is on the ground and wants to jump
                jump = true;
                animator.SetBool("IsJumping", true);
                m_DoubleJumped = false; // Reset double jump flag on ground jump
                
            }
            else if (!m_DoubleJumped)
            {
                // Player is in the air and wants to double jump
                jump = true;
                animator.SetBool("IsJumping", true);
                m_DoubleJumped = true; // Set double jump flag
                
            }
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            //animator.SetBool("IsCrouching", true);
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            //animator.SetBool("IsCrouching", false);
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    // ===== Collision Detection
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            m_CollectingCoin = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin") && m_CollectingCoin)
        {
            Destroy(other.gameObject);
            cm.coinCount++;
            m_CollectingCoin = false;
            SoundManager.PlayCollectCoin(transform.position);
        }
    }
    // =====/////===== End of: Unity Lifecycle Functions =====/////=====
    // ========== Start of: Event Functions ==========
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        m_DoubleJumped = false; // Reset double jump flag on landing
        animationHelper.PlayPlayerLanding();// Playing the sound from a function because there is no landing animation to trigger it from
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    
    public void OnDash(bool isDashing)
    {
        animator.SetBool("IsDashing", isDashing);
    }

    // ========== End of: Event Functions ==========
    // =//=//=//=//=//= End of: PlayerMovement Class =//=//=//=//=//=
}
