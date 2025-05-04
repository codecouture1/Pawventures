using UnityEngine;

public class MoonScipt : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 19.8f, transform.position.z);
    }
}
