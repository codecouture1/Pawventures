using UnityEngine;

public class Car_Background : MonoBehaviour
{
    [SerializeField] private float delay = 1f; // Delay in seconds, editable in the Unity Editor

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartAnimationAfterDelay());
    }

    private System.Collections.IEnumerator StartAnimationAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        animator.SetTrigger("StartAnimation"); // Trigger the animation
    }
}
