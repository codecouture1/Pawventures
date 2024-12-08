using Unity.Cinemachine;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    private CinemachineCamera cinemachineCamera;
    private cameraScript camScript;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cinemachineCamera = cam.GetComponent<CinemachineCamera>();
        camScript = cinemachineCamera.GetComponent<cameraScript>();
        startpos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
   
        float dist = ((cam.transform.position.x) * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
