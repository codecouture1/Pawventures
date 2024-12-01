using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject spamW;
    public GameObject player;
    private playerScript pScript;
    void Start()
    {
        pScript = player.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spamW.SetActive(pScript.slowed && !pScript.slowChallengeFailed);
    }
}
