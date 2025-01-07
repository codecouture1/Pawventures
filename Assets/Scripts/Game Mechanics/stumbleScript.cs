using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Cinemachine;

public class StumbleScript : MonoBehaviour
{
    private BoxCollider2D coll;
    private GameObject player;
    private PlayerScript pScript;
    private PlayerAnimationManager animationManager;
    private GameObject m_Camera;
    private CameraScript camScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("Camera");
        player = GameObject.Find("Player");
        pScript = player.GetComponent<PlayerScript>();
        coll = GetComponent<BoxCollider2D>();
        animationManager = player.GetComponent<PlayerAnimationManager>();
        camScript = m_Camera.GetComponent<CameraScript>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animationManager.DoStumbleAnimation();
            pScript.stopCrouch();
            StartCoroutine(SlowDownCoroutine(15.5f, 0.6f));
            StartCoroutine(camScript.Rumble(4f, 0.6f));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

    }

    private IEnumerator SlowDownCoroutine(float slowAmount, float slowTime)
    {
        pScript.moveSpeed = slowAmount;
        yield return new WaitForSeconds(slowTime);
        pScript.moveSpeed = pScript.DEFAULT_MOVESPEED;
    }
}