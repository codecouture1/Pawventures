using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    private AudioSource audioSource;

    void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!referenceManager.playerScript.alive() )
        {
            audioSource.Stop();
        }
        else if (referenceManager.pauseMenu.activeSelf)
        {
            audioSource.Pause();
        }
        else if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
