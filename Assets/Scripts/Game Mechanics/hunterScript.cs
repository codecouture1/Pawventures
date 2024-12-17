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

    //--------------Stats---------------
    public float jumpStrength;
    private float moveSpeed;
    private bool jumping = false;
    private float CurrentXPosition;

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

        Physics2D.IgnoreLayerCollision(9, 8);

    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = pScript.DEFAULT_MOVESPEED;

        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(ResetPositionCoroutine());
        }



        if (pScript.slowed && !pScript.slowChallengeFailed)
        {
            moveSpeed = pScript.moveSpeed;
        }

        if (pScript.alive() || jumping) //&& !isTransitioning
        {
            run(moveSpeed);
        }

        //jumps automatically if not grounded
        jump();
    }

    //ground check
    public bool isGrounded()
    {
        offsetPositionX = transform.position.x + offset;
        offsetPosition = new(offsetPositionX, transform.position.y, transform.position.z);
        if (Physics2D.BoxCast(offsetPosition, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
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
        if (!isGrounded() && !jumping)
        {
            myRigidbody.linearVelocity = Vector2.up * jumpStrength;
            jumping = true;
        }
        if (isGrounded())
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
            float targetPosition = player.transform.position.x - 21f;

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

        // Restore the player's move speed
        moveSpeed = pScript.moveSpeed;
        positionResetComplete = true;
        positionResetComplete = false;
    }
}

    
