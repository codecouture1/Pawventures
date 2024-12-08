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
    public GameObject m_Camera;
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
        camScript = m_Camera.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if camera zoom is set to default if a zoom coroutine is currently active
        if (!pScript.alive() && !done)
        {
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
                zoomCoroutine = null;
            }
            StartCoroutine(camScript.Zoom(camScript.DEFAULT_FOV, camScript.DEFAULT_TARGET_OFFSET_X, camScript.DEFAULT_TARGET_OFFSET_Y, 0.75f, true));
            camScript.Rumble(0f);
            done = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            pScript.stopCrouch();
            pScript.slowChallengeFailed = false;
            pScript.slowed = true;

            //zoom Coroutine (zooming in)
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            zoomCoroutine = StartCoroutine(camScript.Zoom(27f, 0f, 0f, 5f, false));
            camScript.Rumble(1.25f);

            //slow Challeenge Coroutine
            slowChallengeCoroutine = StartCoroutine(SlowChallenge());

            pScript.moveSpeed = slowSpeed;
            pScript.canJump = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")){

            //zoom Coroutine (zooming out)
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
                zoomCoroutine = null;
            }
            StartCoroutine(camScript.Zoom(camScript.DEFAULT_FOV, camScript.DEFAULT_TARGET_OFFSET_X, camScript.DEFAULT_TARGET_OFFSET_Y, 0.75f, true));
            camScript.Rumble(0f);

            //stop slow Challeenge Coroutine
            if (slowChallengeCoroutine != null)
            {
                StopCoroutine(slowChallengeCoroutine);
                slowChallengeCoroutine = null;
            }

            StartCoroutine(ResetPlayerSpeed(0.75f)); //MUST have same value as zooming out duration
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

    //delay player regaining default movementspeed in order for the zooming out to work properly
    private IEnumerator ResetPlayerSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        pScript.slowed = false;
        pScript.moveSpeed = pScript.DEFAULT_MOVESPEED;
        pScript.canJump = true;
    }

}
