using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    // ========== Start of: Sound Effect Helper Functions ==========
    private void PlayPlayerBasicAttack()
    {
        SoundManager.PlayPlayerBasicAttack(transform.position);
    }

    private void PlayPlayerSlideGround()
    {
        SoundManager.PlayPlayerSlideGround(transform.position);
    }

    private void PlayPlayerDashAttack()
    {
        SoundManager.PlayPlayerDashAttack(transform.position);
    }

    private void PlayPlayerDash()
    {
        SoundManager.PlayPlayerDash(transform.position);
    }

    private void PlayPlayerDeath()
    {
        SoundManager.PlayPlayerDeath(transform.position);
    }

    private void PlayPlayerJump()
    {
        SoundManager.PlayPlayerJump(transform.position);
    }

    private void PlayPlayerDoubleJump()
    {
        SoundManager.PlayPlayerDoubleJump(transform.position);
    }

    public void PlayPlayerLanding()
    {
        SoundManager.PlayPlayerLanding(transform.position);
    }

    private void PlayPlayerHit()
    {
        SoundManager.PlayPlayerHit(transform.position);
    }

    private void PlayPlayerHeal()
    {
        SoundManager.PlayPlayerHeal(transform.position);
    }




    // ========== End of: Sound Effect Helper Functions ==========
}
