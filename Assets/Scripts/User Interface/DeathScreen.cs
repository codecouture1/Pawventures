using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public AudioSource deathSound;
    public GameObject revive;
    private Timer reviveTimer;
    public GameObject buttons;
    public TextMeshProUGUI reviveCounter;

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
        reviveCounter.text = $"x{GameData.Instance.reviveCount}";
        if(GameData.Instance.reviveCount > 0)
        {
            revive.SetActive(true);
            buttons.SetActive(false);
            reviveTimer.Set(4f);
        } 
        else
        {
            revive.SetActive(false);
            buttons.SetActive(true);
        }

        deathSound.Play();
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
        referenceManager.hunterScript.animator.SetTrigger("restart");
        GameData.Instance.reviveCount--;
        GameData.Instance.SaveData();
    }
}
