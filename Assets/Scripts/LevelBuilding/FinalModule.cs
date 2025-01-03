using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalModule : MonoBehaviour
{
    public int sceneIndex;

    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    private Animator animator;


    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        animator = referenceManager.exitAnimator;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            referenceManager.cameraScript.SetTrackingTarget(null);
            animator.SetTrigger("SceneExit");
            StartCoroutine(WaitThenLoadScene(2f));
        }
    }

    private IEnumerator WaitThenLoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneIndex);
        yield return null;
    }

}
