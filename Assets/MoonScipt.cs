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
        transform.position = transform.position + (Vector3.right * referenceManager.playerScript.GetMoveSpeed() * Time.deltaTime);
    }
}
