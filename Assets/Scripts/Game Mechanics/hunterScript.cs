using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class hunterScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    private BoxCollider2D coll;

    public float jumpStrength;
    public float moveSpeed;
    public Vector2 boxSize;
    public LayerMask groundLayer;
    public float castDistance;
    private bool jumping = false;

    public GameObject player;
    private playerScript pScript;

    void Start()
    {
        //Ignore the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)
        //TODO: Player sollte auch auf Hindernissen grounded() = true sein dürfen
        Physics2D.IgnoreLayerCollision(8, 7);
        pScript = player.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pScript.health != 0)
        {
            run(moveSpeed);
        }
        jump();
    }

    //ground check
    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
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

    
