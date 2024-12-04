using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private playerScript pScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        pScript = GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //crouch
        animator.SetBool("crouch", false);
        if (pScript.isCrouching)
        {
            animator.SetBool("crouch", true);
        }

        //slow
        animator.SetBool("slowed", false);
        if (pScript.slowed)
        {
            animator.SetBool("slowed", true);
        }

        //death
        if (!pScript.alive())
        {
            animator.SetTrigger("death");
        }
    }

    //stumble
    public void DoStumbleAnimation()
    {
        animator.SetTrigger("stumble");
    }


}
