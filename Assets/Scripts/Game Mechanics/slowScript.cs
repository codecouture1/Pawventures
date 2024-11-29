using TMPro.Examples;
using UnityEngine;

public class slowScript : MonoBehaviour
{
    private BoxCollider2D coll;
    private GameObject player;
    private playerScript pScript;
    public GameObject cam;
    private cameraScript camScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        coll = GetComponent<BoxCollider2D>();
        pScript = player.GetComponent<playerScript>();
        camScript = cam.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("enter");
        pScript.slowed = true;
        //TODO
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exit");
        pScript.slowed = false;
    }

}
