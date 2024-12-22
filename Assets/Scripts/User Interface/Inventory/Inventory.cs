using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OpenInventory()
    {
        inventory.SetActive(true);
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
