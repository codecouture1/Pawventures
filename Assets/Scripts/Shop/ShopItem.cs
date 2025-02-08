using UnityEngine;

//this class represents an item within the shop menu. All values are to be assigned within the editor.
public class ShopItem : MonoBehaviour
{
    public ShopPanel shopPanel;
    public PowerUps powerUp;
    public string ItemName;
    public int Price;
    public Sprite Sprite;
    [TextArea]
    public string description;

    //passes this item to the shop panel
    public void SetItem()
    {
        shopPanel.item = this;
        shopPanel.gameObject.SetActive(true);
    }
}
