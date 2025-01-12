using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    PlayerScript playerScript;

    public GameObject tutorialCourse;
    public GameObject turotialUI;
    public Sprite[] keySprites;
    public Image keyImage;
    public TextMeshProUGUI instruction;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    void Start()
    {
        playerScript = referenceManager.playerScript;
        if (!GameData.Instance.tutorialCompleted)
        {
            referenceManager.player.transform.position = new Vector3(-590f, 1, 0);
            referenceManager.hunter.transform.position = new Vector3(-612f, 6.2f, 0);

            playerScript.canJump = false;
            playerScript.canCrouch = false;

            tutorialCourse.SetActive(true);
        }
    }

    public IEnumerator JumpInstruction()
    {
        turotialUI.SetActive(true);
        SetUI(keySprites[0], "SPRINGEN");
        Time.timeScale = 0f;

        yield return new WaitUntil(() => Input.GetKey(KeyCode.W));

        turotialUI.SetActive(false);
        Time.timeScale = 1f;
        playerScript.Jump();
    }

    public IEnumerator LandInstruction()
    {
        turotialUI.SetActive(true);
        SetUI(keySprites[1], "(gedrückt halten) \n SCHNELLER LANDEN");
        Time.timeScale = 0f;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S));

        turotialUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator CrouchInstruction()
    {
        turotialUI.SetActive(true);
        SetUI(keySprites[1], "DUCKEN");
        Time.timeScale = 0f;

        yield return new WaitUntil(() => Input.GetKey(KeyCode.S));

        turotialUI.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(playerScript.crouch(1.0f));
    }

    public IEnumerator PowerUpInstruction()
    {
        turotialUI.SetActive(true);
        SetUI(keySprites[2], "POWERUP AKTIVIEREN");
        Time.timeScale = 0f;

        yield return new WaitUntil(() => Input.GetKey(KeyCode.E));

        turotialUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CompletedTutorial()
    {
        GameData.Instance.tutorialCompleted = true;
        GameData.Instance.SaveData();
        playerScript.canJump = true;
        playerScript.canCrouch = true;
    }

    private void SetUI(Sprite key, string instruction)
    {
        keyImage.sprite = key;
        this.instruction.text = instruction;
    }


}
