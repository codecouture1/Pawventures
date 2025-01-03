using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public RuntimeAnimatorController defaultAnimationController;
    public RuntimeAnimatorController halsbandAnimationController;
    private Animator animator;

    private PlayerScript pScript;
    private SpriteRenderer spriteRenderer;
    private bool isIFrameAnimationRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        pScript = GetComponent<PlayerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.SetBool("slowed", false);     
    }

    // Update is called once per frame
    void Update()
    {
        animator.runtimeAnimatorController =
            pScript.health > 1 ? halsbandAnimationController : defaultAnimationController;


        //crouch
        animator.SetBool("crouch", false);
        if (pScript.isCrouching)
        {
            animator.SetBool("crouch", true);
        }

        //slow
        if(!pScript.slowed && animator.GetBool("slowed"))      
            animator.SetBool("slowed", false);       
        if (pScript.slowed && !animator.GetBool("slowed"))       
            animator.SetBool("slowed", true);       

        //jump
        animator.SetBool("jump", false);
        if (!pScript.isGrounded())
        {
            animator.SetBool("jump", true);
        }

        //death
        if (!pScript.alive())
        {
            animator.SetTrigger("death");
        }

        //iframe
        if (pScript.iFrameActive && !isIFrameAnimationRunning)
        {
            StartCoroutine(IFrameAnimation());
        }

    }

    //stumble
    public void DoStumbleAnimation()
    {
        animator.SetTrigger("stumble");
    }

    public IEnumerator IFrameAnimation()
    {
        isIFrameAnimationRunning = true; // Mark the coroutine as running

        float elapsedTime = 0f;

        // Cache the original color
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < 2f)
        {
            // Use Time.time for consistent PingPong oscillation
            float alpha = Mathf.PingPong(Time.time * 5f, 1f); // Adjust speed as needed

            // Set the new color with modified alpha
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restore the original color after the animation ends
        spriteRenderer.color = originalColor;

        isIFrameAnimationRunning = false; // Mark the coroutine as finished
    }
}

