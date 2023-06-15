using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_A_AnimationHelper : MonoBehaviour
{
    // ========== Start of: Sound Effect Helper Functions ==========
    private void PlaySkeletonAttack1()
    {
        SoundManager.PlaySkeletonAttack1(transform.position);
    }

    private void PlaySkeletonAttack2()
    {
        SoundManager.PlaySkeletonAttack2(transform.position);
    }

    private void PlaySkeletonHit()
    {
        SoundManager.PlaySkeletonHit(transform.position);
    }

    private void PlaySkeletonDeath()
    {
        SoundManager.PlaySkeletonDeath(transform.position);
    }

    private void PlaySkeletonHeal()
    {
        SoundManager.PlaySkeletonHeal(transform.position);
    }
    // ========== End of: Sound Effect Helper Functions ==========
}
