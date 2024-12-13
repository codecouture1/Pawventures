using System.Collections;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private playerScript pScript;
    private SpriteRenderer spriteRenderer;
    private bool isIFrameAnimationRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        pScript = GetComponent<playerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

