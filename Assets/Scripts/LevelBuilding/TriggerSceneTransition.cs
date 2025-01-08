using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerSceneTransition : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            referenceManager.cameraScript.SetTrackingTarget(null);
            StartCoroutine(WaitThenTrigger(1f));
        }
    }

    private IEnumerator WaitThenTrigger(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        referenceManager.exitAnimator.SetTrigger("SceneExit");
    }


}
