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
            referenceManager.LoadOnClick = nextSceneIndex;
            if (skipInventoryPopup)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                referenceManager.inventory.SetActive(true);
                inventorySript.DisplayExitButton(false);
            }
        }
        Destroy(collision.gameObject);
    }
}
