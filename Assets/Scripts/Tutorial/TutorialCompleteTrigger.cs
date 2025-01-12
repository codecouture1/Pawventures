using UnityEngine;

public class TutorialCompleteTrigger : MonoBehaviour
{
    public Tutorial turorialScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            turorialScript.CompletedTutorial();
        }
    }
}
