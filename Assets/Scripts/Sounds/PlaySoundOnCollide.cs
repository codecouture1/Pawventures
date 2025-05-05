using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlaySoundOnCollide : MonoBehaviour
{
    private AudioSource audioSource;
    public bool keepPlayingOnExit = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (keepPlayingOnExit)
            {
                return;
            }
            StartCoroutine(WaitUntil(0.5f));
        }
    }

    private IEnumerator WaitUntil(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.Stop();
        yield return null;
    }
}
