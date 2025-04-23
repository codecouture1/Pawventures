using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    //-------Objects & Components-------
    public Rigidbody2D myRigidbody2D;
    private GameObject m_Camera;
    private CameraScript camScript;
    private AudioSource audioSource;

    public BoxCollider2D myBoxCollider2D;
    private Vector2 defaultColliderSize;
    private Vector2 defaultColliderOffet;

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
    [HideInInspector] public bool canCrouch = true; //disabled when player is slowed
    public Coroutine doubleJumpCoroutine;

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

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    void Start()
    {
        health = GameData.Instance.playerHealth;
        if (GameData.Instance.playerHealth < 1)
            health = 1;

        defaultColliderSize = myBoxCollider2D.size;
        defaultColliderOffet = myBoxCollider2D.offset;

        moveSpeed = DEFAULT_MOVESPEED;
        m_Camera = GameObject.FindGameObjectWithTag("Camera");
        camScript = m_Camera.GetComponent<CameraScript>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = null;
        audioSource.playOnAwake = false;
    }
  
    void Update()
    {
        Debug.Log(doubleJumpCoroutine);

        if (alive())
        {
            //player rennt automatisch
            run(moveSpeed);

            //springen
            if (Input.GetKeyDown(KeyCode.W) && canJump && isGrounded() && doubleJumpCoroutine == null )           
                Jump();          

            //schnelles landen
            if (Input.GetKeyDown(KeyCode.S) == true && !isGrounded())
            {
                myRigidbody2D.linearVelocity = Vector2.down * 30;
            }

            //ducken
            if (Input.GetKey(KeyCode.S) == true && isGrounded() && !slowed && canCrouch)
            {
                if (!isCrouching && !crouchCooldown)
                {
                    crouchRoutine = StartCoroutine(crouch(1.0f));
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

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    //******************************jumping******************************

    public void Jump()
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

    public IEnumerator DoubleJump(float duration)
    {
        int jumpCounter = isGrounded() ? 0 : 1; // Start at 1 if not grounded, else 0
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (jumpCounter == 2)
            {
                // Wait for the player to land, but also check if the duration has expired
                while (!isGrounded() && elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Reset jump counter if grounded
                if (isGrounded())
                {
                    jumpCounter = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.W) && canJump && jumpCounter < 2)
            {
                Jump();
                jumpCounter++;
                Debug.Log(jumpCounter);
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the coroutine ends properly
        doubleJumpCoroutine = null;
    }

    //*****************************crouching*****************************
    void setCollider(float sizeX, float sizeY, float offsetX, float offsetY)
    {
        myBoxCollider2D.size = new Vector2(sizeX, sizeY);
        myBoxCollider2D.offset = new Vector2(offsetX, offsetY);
    }

    public IEnumerator crouch(float duration)
    {
        isCrouching = true;
        audioSource.Stop();
        audioSource.clip = slideSound;
        audioSource.Play();

        //set crouch height
        float crouchHeight = defaultColliderSize.y / 2;

        setCollider(
            defaultColliderSize.x, 
            crouchHeight, 
            defaultColliderOffet.x, 
            crouchHeight * -0.5f);

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

    //sets the player collider back to default values
    public void stopCrouch()
    {
        if (crouchRoutine != null)
        {
            StopCoroutine(crouchRoutine);
        }

        myBoxCollider2D.size = defaultColliderSize;
        myBoxCollider2D.offset = defaultColliderOffet;

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

    //***********************************revive***********************************
    public void Revive()
    {
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        health = 1;
        referenceManager.hunterScript.ResetPosition();
        StartCoroutine(iFrames());

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
            GameData.Instance.playerHealth--;
            GameData.Instance.SaveData();
        }

        if (collision.gameObject.tag == "Kill")
        {
            health = 0;  
        }
    }
}

