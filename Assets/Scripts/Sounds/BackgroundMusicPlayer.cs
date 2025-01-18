using System;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    private AudioSource audioSource;

    void Awake()
    {
        try
        {
            referenceManagerObj = GameObject.Find("ReferenceManager");
            referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        } 
        catch (NullReferenceException) { /* No referencemanager found */ }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (referenceManager != null && !referenceManager.playerScript.alive() )
        {
            audioSource.Stop();
        }
        else if (PauseMenu.Instance.IsPaused)
        {
            audioSource.Pause();
        }
        else if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
