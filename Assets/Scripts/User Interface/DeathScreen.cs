using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public AudioSource deathSound;
    public GameObject revive;
    private Timer reviveTimer;
    public GameObject buttons;

    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        reviveTimer = revive.GetComponent<Timer>();
    }

    private void OnEnable()
    {
        revive.SetActive(true);
        buttons.SetActive(false);

        deathSound.Play();
        reviveTimer.Set(4f);
    }

    private void Update()
    {
        if (reviveTimer.Finished)
        {
            revive.SetActive(false);
            buttons.SetActive(true);
        }
    }

    public void RevivePlayer()
    {
        referenceManager.playerScript.Revive();
    }
}
