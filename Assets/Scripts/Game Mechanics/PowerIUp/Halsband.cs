using UnityEngine;

public class Halsband : PowerUp
{
    private GameObject player;
    private PlayerScript pScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pScript = player.GetComponent<PlayerScript>();
    }
    public override void ApplyPowerup()
    {
        pScript.health++;
    }
}
