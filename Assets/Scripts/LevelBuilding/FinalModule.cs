using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalModule : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    InventoryInterface inventorySript;
    public bool skipInventoryPopup;
    public int nextSceneIndex;

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
            //get next scene index from referencemanager
            referenceManager.LoadOnClick = nextSceneIndex;

            //if this is the last chapter of the level, the inventory popup is going to be skipped
            if (skipInventoryPopup)
            {
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
            }
        }
        Destroy(collision.gameObject);
    }
}
