using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Cinemachine;

public class stumbleScript : MonoBehaviour
{
    private BoxCollider2D coll;
    private GameObject player;
    private playerScript pScript;
    public GameObject camera;
    private cameraScript camScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        coll = GetComponent<BoxCollider2D>();
        pScript = player.GetComponent<playerScript>();
        camScript = camera.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(SlowDownCoroutine(15.5f, 0.6f));
        StartCoroutine(camScript.Rumble(4f, 0.6f));
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