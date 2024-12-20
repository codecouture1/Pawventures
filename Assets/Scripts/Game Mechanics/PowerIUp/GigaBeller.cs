using System.Collections;
using UnityEngine;

public class GigaBeller : PowerUp
{
    private GameObject hunter;
    private HunterScript hunterScript;

    private GameObject m_camera;
    private CameraScript camScript;

    void Start()
    {
        hunter = GameObject.Find("Hunter");
        hunterScript = hunter.GetComponent<HunterScript>();
        m_camera = GameObject.FindGameObjectWithTag("Camera");
        camScript = m_camera.GetComponent<CameraScript>();
    }

    public override void  ApplyPowerup()
    {
        StartCoroutine(hunterScript.ResetPositionCoroutine());
        StartCoroutine(camScript.Rumble(5f, 0.5f));
    }

}
