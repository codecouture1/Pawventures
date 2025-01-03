using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //-------Objects & Components-------
    public Rigidbody2D myRigidbody2D;
    public BoxCollider2D myBoxCollider2D;
    private GameObject m_Camera;
    private CameraScript camScript;
    private AudioSource audioSource;

    //--------------Stats---------------
    public float jumpStrength;
    public float moveSpeed;
    public readonly float DEFAULT_MOVESPEED = 30f;
    [HideInInspector] public int health = 1;
    [HideInInspector] public bool iFrameActive = false;

    //-------------movement-------------
    [HideInInspector] public bool isCrouching = false;
    private bool crouchCooldown = false;
    private Coroutine crouchRoutine;

    [HideInInspector] public bool canJump = true; //disabled when player is slowed
    [HideInInspector] public bool doubleJump = false;
    private Coroutine doubleJumpCoroutine;

    [HideInInspector] public bool slowed = false; //if true, hunter will slow down with player
    [HideInInspector] public bool slowChallengeFailed = false; //if slow challenge is failed hunter will catch up with player

    //-----------ground check------------
    public Vector2 boxSize;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public float castDistance;

    //--------------Sounds---------------
    public AudioClip jumpSound;
    public AudioClip slideSound;

    void Start()
    {
        moveSpeed = DEFAULT_MOVESPEED;
        m_Camera = GameObject.FindGameObjectWithTag("Camera");
        camScript = m_Camera.GetComponent<CameraScript>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = null;
        audioSource.playOnAwake = false;
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            doubleJump = true;
        }

        if (alive())
        {
            //player rennt automatisch
            run(moveSpeed);

            //springen
            if (canJump)
            {
                if (doubleJump && doubleJumpCoroutine == null)
                {
                    doubleJumpCoroutine = StartCoroutine(DoubleJump());                   
                } 
                else
                {
                    Jump();
                }
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
                    audioSource.Stop();
                    audioSource.clip = slideSound;
                    audioSource.Play();
                }
            }

            //iFrames
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(iFrames());
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    //***************************ground check***************************
    public bool isGrounded() 
    { 
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer) || 
            Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, obstacleLayer))
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

    //******************************running******************************
    void run(float multiplier)
    {
        transform.position = transform.position + (Vector3.right * multiplier) * Time.deltaTime;
    }

    private float GetMoveSpeed()
    {
        return moveSpeed;
    }

    //******************************jumping******************************

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) == true && isGrounded())
        {
            if (isCrouching)
            {
                stopCrouch();
            }
            myRigidbody2D.linearVelocity = Vector2.up * jumpStrength;
            audioSource.Stop();
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }
    IEnumerator DoubleJump()
    {
        int jumpCounter = isGrounded() ? 0 : 1; // Start at 1 if not grounded, else 0

        while (jumpCounter < 2)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (jumpCounter < 2) // Ensure jumpCounter is within limits
                {
                    jumpCounter++;
                    Debug.Log($"Grounded: {isGrounded()}, JumpCounter: {jumpCounter}");
                    if (isCrouching)
                    {
                        stopCrouch();
                    }
                    myRigidbody2D.linearVelocity = Vector2.up * jumpStrength;                
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.W)); // Wait for key release before proceeding
                    yield return null;
                }
            }
            yield return null; // Wait for the next frame
        }
        doubleJumpCoroutine = null;
        doubleJump = false; // Reset double jump state
    }

    //*****************************crouching*****************************
    void setCollider(float sizeX, float sizeY, float offsetX, float offsetY)
    {
        myBoxCollider2D.size = new Vector2(sizeX, sizeY);
        myBoxCollider2D.offset = new Vector2(offsetX, offsetY);
    }

    IEnumerator crouch(float duration)
    {
        isCrouching = true;

        //crouch height
        setCollider(0.65f, 0.25f, 0f, -0.125f);

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

        setCollider(0.65f, 0.5f, 0f, 0f);

        isCrouching = false;
        StartCoroutine(CrouchCooldown());
    }

    private IEnumerator CrouchCooldown()
    {
        crouchCooldown = true;

        // Wait for the cooldown duration until player can crouch again
        yield return new WaitForSeconds(0.3f);

        crouchCooldown = false;
    }


    //***************************check if player alive***************************
    public bool alive()
    {
        if (health <= 0)
        {
            return false;
        }
        return true;
    }

    //**********************************iFrames**********************************

    public IEnumerator iFrames()
    {
        Debug.Log("iFrames active");
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            iFrameActive = true;
            Physics2D.IgnoreLayerCollision(0, 8, true); //ignore collision with obstacles
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Physics2D.IgnoreLayerCollision(0, 8, false);
        iFrameActive = false;
        Debug.Log("iFrames stopped");
    }


    //***************************Damage from Obstacles***************************
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            StartCoroutine(camScript.Rumble(4f, 0.6f));
            if (health > 1)
            {
                StartCoroutine(iFrames());
            }
            health--;                 
        }

        if (collision.gameObject.tag == "Kill")
        {
            health = 0;  
        }
    }
}

