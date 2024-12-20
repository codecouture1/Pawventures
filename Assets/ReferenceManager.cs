using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    //private GameObject referenceManagerObj;
    //private ReferenceManager referenceManager;

    //referenceManagerObj = GameObject.Find("ReferenceManager");
    //referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

    public GameObject player;
    public PlayerScript playerScript { get; private set; }

    public GameObject hunter;
    public HunterScript hunterScript { get; private set; }

    public GameObject m_camera;
    public CameraScript cameraScript { get; private set; }

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        hunterScript = hunter.GetComponent<HunterScript>();
        cameraScript = m_camera.GetComponent<CameraScript>();
    }
}
