using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HunterScript : MonoBehaviour
{
    //-------Objects & Components-------
    private Rigidbody2D myRigidbody;
    private GameObject player;
    private PlayerScript pScript;
    public Animator animator;
    private Coroutine resetPositionCoroutine;

    //--------------Stats---------------
    public float jumpStrength;
    public float playerOffset; //the default distance to the player
    private float moveSpeed;
    private bool jumping = false;
    private float CurrentXPosition;
    public bool close; //true if hunter is close to player

    //----------ground check------------
    public Vector2 boxSize;
    private Vector3 offsetPosition;
    public LayerMask groundLayer;
    public float castDistance;
    public float offset;
    private float offsetPositionX;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        pScript = player.GetComponent<PlayerScript>();
        close = false;

        Physics2D.IgnoreLayerCollision(9, 8);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = pScript.DEFAULT_MOVESPEED;

        if (pScript.slowed && !pScript.slowChallengeFailed)
        {
            moveSpeed = pScript.moveSpeed;
        }

        if (pScript.alive() || jumping) //&& !isTransitioning
        {
            run(moveSpeed);
        } else
        {
            animator.SetTrigger("stand");
        }

        //jumps automatically if not grounded
        jump();

        if (close && resetPositionCoroutine == null)
        {
            resetPositionCoroutine = StartCoroutine(ResetCountdown());
        }
    }

    //ground check
    public bool IsGrounded()
    {
        offsetPositionX = transform.position.x + offset;
        offsetPosition = new(offsetPositionX, transform.position.y, transform.position.z);
        if (Physics2D.BoxCast(offsetPosition, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            animator.SetBool("jumping", false);
            return true;
        }
        else
        {
            animator.SetBool("jumping", true);
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        offsetPositionX = transform.position.x + offset;
        offsetPosition = new(offsetPositionX, transform.position.y, transform.position.z);
        Gizmos.DrawWireCube(offsetPosition - transform.up * castDistance, boxSize);
    }

    //running
    void run(float multiplier)
    {
        transform.position = transform.position + (Vector3.right * multiplier) * Time.deltaTime;
    }

    //jumping
    private void jump()
    {
        if (!IsGrounded() && !jumping)
        {
            myRigidbody.linearVelocity = Vector2.up * jumpStrength;
            jumping = true;
        }
        if (IsGrounded())
        {
            jumping = false;
        }
    }

    public bool positionResetComplete { get; private set; } = false;

    public IEnumerator ResetPositionCoroutine()
    {
        float duration = 6f;
        moveSpeed = 0;
        float tolerance = 0.01f; // Define a small margin of error

        float time = 0;
        while (time < duration)
        {
            // Calculate target position
            float targetPosition = player.transform.position.x - playerOffset;

            // Interpolate towards the target
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Lerp(transform.position.x, targetPosition, time / duration);

            // Update position
            transform.position = currentPosition;

            // Check if we are close enough to the target
            if (Mathf.Abs(transform.position.x - targetPosition) < tolerance)
            {
                break; // Exit the loop early
            }

            // Continue with the loop
            yield return null;
            time += Time.deltaTime;
        }
        Debug.Log("Hunter reached the target position.");

        // Restore the player's move speed
        moveSpeed = pScript.moveSpeed;
        positionResetComplete = true;
        positionResetComplete = false;
        close = false;
        resetPositionCoroutine = null;
    }

    public void ResetPosition()
    {
        float targetPosition = player.transform.position.x - playerOffset;
        Vector3 currentPosition = new(targetPosition, 1f, 0f);
        transform.position = currentPosition;
        close = false;
    }

    private IEnumerator ResetCountdown()
    {
        yield return new WaitForSeconds(20f);

        StartCoroutine(ResetPositionCoroutine());
    }
}

    
