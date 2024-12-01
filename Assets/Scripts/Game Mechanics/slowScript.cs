using TMPro.Examples;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Cinemachine;

public class slowScript : MonoBehaviour
{
    private BoxCollider2D coll;
    private GameObject player;
    private playerScript pScript;
    public GameObject camera;
    private cameraScript camScript;

    private Coroutine zoomCoroutine;
    private Coroutine slowChallengeCoroutine;

    private bool done = false;

    public float slowSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        pScript = player.GetComponent<playerScript>();
        camScript = camera.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pScript.alive() && !done)
        {
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
                zoomCoroutine = null;
            }
            StartCoroutine(camScript.Zoom(camScript.DEFAULT_FOV, camScript.DEFAULT_TARGET_OFFSET_X, 0.75f));
            camScript.Rumble(0f);
            done = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
            pScript.stopCrouch();
            pScript.slowChallengeFailed = false;
            pScript.slowed = true;

            //zoom Coroutine
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            zoomCoroutine = StartCoroutine(camScript.Zoom(12f, 5f, 20f));

            //slow Challeenge Coroutine
            slowChallengeCoroutine = StartCoroutine(SlowChallenge());

            camScript.Rumble(2f);
            pScript.moveSpeed = slowSpeed;
            pScript.canJump = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            Debug.Log("exit");
            pScript.slowed = false;

            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
                zoomCoroutine = null;
            }
            StartCoroutine(camScript.Zoom(camScript.DEFAULT_FOV, camScript.DEFAULT_TARGET_OFFSET_X, 0.75f));

            if (slowChallengeCoroutine != null)
            {
                StopCoroutine(slowChallengeCoroutine);
                slowChallengeCoroutine = null;
            }

            camScript.Rumble(0f);

            pScript.moveSpeed = pScript.DEFAULT_MOVESPEED;
            pScript.canJump = true;
        }
        
    }

    private IEnumerator SlowChallenge()
    {
        float elapsedTime = 0f;
        yield return new WaitForSeconds(0.75f); //wait to give player time to react
        Debug.Log("Slow Challenge Started");
        while (elapsedTime < 0.15f)
        {
            if (Input.GetKey(KeyCode.W)) //spam w to reset timer
            {
                elapsedTime = 0f;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Slow Challenge Failed");
        pScript.slowChallengeFailed = true;
    }

}
