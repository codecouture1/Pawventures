using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlaySoundOnCollide : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        {
            if (col.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
                Debug.Log("Boom");
            }
        }
    }
}
