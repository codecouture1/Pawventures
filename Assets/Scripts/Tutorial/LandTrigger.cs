using UnityEngine;

public class LandTrigger : MonoBehaviour
{
    public Tutorial turorialScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(turorialScript.LandInstruction());
        }
    }
}
