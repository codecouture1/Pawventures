using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalModule : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    InventoryInterface inventorySript;
    public bool finalChapter; //set this to true in the editor if this level is a final chapter
    public int nextSceneIndex; //the scene index of the next chapter

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        inventorySript = referenceManager.inventoryScript;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (finalChapter)
            {
                int thisLevel = MainMenu.GetLevel();
                //in case of a final chapter, all checkpoints will be deleted upon level completion
                GameData.Instance.levelStart[thisLevel] = MainMenu.GetFirstChapter(thisLevel);
                GameData.Instance.levelRemain[thisLevel] = 0;

                //in case of a final chapter, inventory sequence skipped and the next scene is loaded instantly
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                //save player health, so that player may carry their Halsband to the next level
                GameData.Instance.playerHealth = referenceManager.playerScript.health;
                GameData.Instance.SaveData();

                //display inventory so the player can set powerups for the next chapter
                referenceManager.inventory.SetActive(true);
                inventorySript.DisplayExitButton(false);

                //change the scene that gets loaded to the next scene
                referenceManager.inventoryScript.LoadOnClick = nextSceneIndex;
            }
        }
        Destroy(collision.gameObject);
    }
}
