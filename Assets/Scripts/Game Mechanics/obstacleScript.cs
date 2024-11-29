using TMPro.Examples;
using UnityEngine;
using System.Collections;

public class obstacleScript : MonoBehaviour
{
    private playerScript pScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pScript = GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Kill")
        {
            pScript.health--;
        }
    }
}

