using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public BoxCollider2D myBoxCollider2D;
    private Animator animator;

    public float jumpStrength;
    public float moveSpeed;
    public readonly float DEFAULT_MOVESPEED = 30f;

    public bool isCrouching = false;
    private bool crouchCooldown = false;
    private Coroutine crouchRoutine;

    public int health = 1;
    public bool doubleJump = false;
    public bool canJump = true; //disabled when player is slowed
    public bool slowed = false; //if true, hunter will slow down with player
    public bool slowChallengeFailed = false;
    

    //test
    void Start()
    {
        moveSpeed = DEFAULT_MOVESPEED;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (alive())
        {
            //rennen
            run(moveSpeed);          

            //springen
            if (Input.GetKeyDown(KeyCode.W) == true && isGrounded() && canJump)
            {
                if (isCrouching)
                {
                    stopCrouch();
                }
                myRigidbody2D.linearVelocity = Vector2.up * jumpStrength;
            }

            //schnelles landen
            if (Input.GetKeyDown(KeyCode.S) == true && !isGrounded())
            {
                myRigidbody2D.linearVelocity = Vector2.down * 30;
            }

            //ducken
            if (Input.GetKey(KeyCode.S) == true && isGrounded() && !slowed)
            {
                if (!isCrouching && !crouchCooldown)
                {
                    crouchRoutine = StartCoroutine(crouch(1.0f));
                }
            }

        } else
        {

        }
    }

    //ground check
    public Vector2 boxSize;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public float castDistance;
    public bool isGrounded() 
    { 
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer) || Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, obstacleLayer))
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
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    //running
    void run(float multiplier)
    {
        transform.position = transform.position + (Vector3.right * multiplier) * Time.deltaTime;
    }

    //set collider size & offset (for crouching)
    void setCollider(float sizeX, float sizeY, float offsetX, float offsetY)
    {
        myBoxCollider2D.size = new Vector2(sizeX, sizeY);
        myBoxCollider2D.offset = new Vector2(offsetX, offsetY);
    }

    //crouch Coroutine
    IEnumerator crouch(float duration)
    {
        isCrouching = true;

        //crouch height
        setCollider(6f, 2.4f, 0f, -1.2f);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (Input.GetKey(KeyCode.W))
            {
                stopCrouch();
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //default height
        stopCrouch();

        isCrouching = false;
    }

    public void stopCrouch()
    {
        if (crouchRoutine != null)
        {
            StopCoroutine(crouchRoutine);
        }

        setCollider(6f, 4.8f, 0f, 0f);

        isCrouching = false;
        StartCoroutine(CrouchCooldown());
    }

    private IEnumerator CrouchCooldown()
    {
        crouchCooldown = true;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(0.3f);

        crouchCooldown = false;
    }


    //check if player alive
    public bool alive()
    {
        if (health <= 0)
        {
            return false;
        }
        return true;
    }

}

