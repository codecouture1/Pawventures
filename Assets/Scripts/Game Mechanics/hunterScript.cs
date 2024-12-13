using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class hunterScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    private BoxCollider2D coll;

    public GameObject player;
    private playerScript pScript;

    public float jumpStrength;
    public float moveSpeed;

    public Vector2 boxSize;
    public LayerMask groundLayer;
    public float castDistance;
    public float offset;
    private float offsetPositionX;
    private Vector3 offsetPosition;


    private bool jumping = false;

    //public float transitionSpeed = 5f;
    //private Vector3 targetPosition;
    //private bool isTransitioning = false;

    void Start()
    {
        pScript = player.GetComponent<playerScript>();

        //Ignore the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)
        Physics2D.IgnoreLayerCollision(9, 8);
    }

    // Update is called once per frame
    void Update()
    {

        //for testing purposes only / anpassen wenn Gigabeller implementiert
        if (Input.GetKeyDown(KeyCode.T))
        {
            //TODO: smooth transition
            transform.position = player.transform.position + (Vector3.left * 22f);
        }

        moveSpeed = pScript.DEFAULT_MOVESPEED;

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
    void jump()
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
}

    
