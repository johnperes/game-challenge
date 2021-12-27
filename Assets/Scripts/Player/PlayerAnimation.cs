using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void Idle() {
        animator.SetBool("Walk", false);
    }

    public void Walk() {
        animator.SetBool("Walk", true);
    }

    public void Attack() {
        animator.SetTrigger("Attack");
    }
    public void GetHit()
    {
        animator.SetTrigger("GetHit");
    }
}
