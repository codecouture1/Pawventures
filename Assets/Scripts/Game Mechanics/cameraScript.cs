using UnityEngine;

public class cameraScript : MonoBehaviour
{

    private GameObject player;
    private playerScript pScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        pScript = player.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}