using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HunterScript : MonoBehaviour
{
    //-------Objects & Components-------
    private Rigidbody2D myRigidbody;
    private GameObject player;
    private playerScript pScript;

    //--------------Stats---------------
    public float jumpStrength;
    private float moveSpeed;
    private bool jumping = false;

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
        pScript = player.GetComponent<playerScript>();
        
        //Physics2D.IgnoreLayerCollision(9, 8);
        //moveSpeed = pScript.DEFAULT_MOVESPEED;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            moveSpeed = 0;

            //transform.position = Vector3.Lerp(transform.position, newOffset, time / duration);
            //player.transform.position + (Vector3.left * 22f);
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

    
