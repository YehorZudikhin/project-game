using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // =//=//=//=//=//= Start of: PlayerMovement Class =//=//=//=//=//=
    public CharacterController2D controller;
    public Animator animator;
    public PlayerAnimationHelper animationHelper;

    public CoinManager cm; 
    private bool m_CollectingCoin = false;

    private bool CollectingHealthPotion = false;
    
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private bool m_DoubleJumped = false;

    //[Header("Dash")]
    private bool canDash = true;
    private bool dashing = false;

    [SerializeField] private float nudgeForce = 5f;

    public static event Action OnGameEnd;

    // =====/////===== Start of: Unity Lifecycle Functions =====/////=====
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && dashing)
        {
            Debug.Log("DashAttack was triggered successfully!");
            animator.SetBool("IsDashAttacking", true);
            return;
        }

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

        if (Input.GetButtonDown("Dash"))
        {
            if(!dashing && canDash && !crouch)
            {
                animator.SetTrigger("Dash");
                dashing = true;
                canDash = false;
                Debug.Log("Dash was triggered successfuly.");
                return;
            }
            //controller.Dash();
        }

    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dashing);
        jump = false;
    }

    // ===== Collision Detection
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            m_CollectingCoin = true;
        }
        if (other.gameObject.CompareTag("HealthPotion"))
        {
            CollectingHealthPotion = true;
        }

        if (other.gameObject.CompareTag("Portal1"))
        {
            SceneManager.LoadScene("Level 2");
            //Time.timeScale = 0f;
        }
        
        if (other.gameObject.CompareTag("Portal"))
        {
            OnGameEnd?.Invoke();
            Time.timeScale = 0f;
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
        if (other.gameObject.CompareTag("HealthPotion") && CollectingHealthPotion)
        {
            Destroy(other.gameObject);
            CollectingHealthPotion = false;
            GetComponent<Health>().HealDamage(20);
            SoundManager.PlayCollectPotion(transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Vector3 playerPosition = transform.position;
            Vector3 enemyPosition = collision.gameObject.transform.position;
            if(playerPosition.y > enemyPosition.y)
            {
                Vector3 nudgeDirection = playerPosition - enemyPosition;
                nudgeDirection.Normalize();
                GetComponent<Rigidbody2D>().AddForce(nudgeDirection * nudgeForce, ForceMode2D.Impulse);
            }
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
        /*
        if (dashing)
        {
            animator.SetTrigger("GroundSlide");
        }
        */
    }

    public void DashEnd()
    {
        Debug.Log("Dash was ended.");
        dashing = false;
        animator.SetBool("IsDashing", false);
        //animator.SetBool("IsDashAttacking", false);
        canDash = true;
    }

    public void OnDash(bool isDashing)
    {
        if(isDashing == true)
        {
            Debug.Log("isDashing was set to true.");
        }else if(isDashing == false)
        {
            Debug.Log("isDashing was set to false.");
        }
        else
        {
            Debug.Log("isDashing is undefined.");
        }
        dashing = isDashing;
        animator.SetBool("IsDashing", isDashing);
        if (!isDashing)
        {
            DashEnd();
        }
    }

    // ========== End of: Event Functions ==========
    // =//=//=//=//=//= End of: PlayerMovement Class =//=//=//=//=//=
}
