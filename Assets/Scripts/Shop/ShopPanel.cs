using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Shop;

public class ShopPanel : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;
    public Image image;
    public TextMeshProUGUI description;
    public Button buyButton;
    public Animator animator;
    public CoinManager coinManager;
    public IShopItem Item { get; private set; }
    
    public void SetItem(IShopItem item)
    {
        itemName.text = item.ItemName;
        price.text = item.Price.ToString();
        image.sprite = item.Sprite;
        description.text = item.description;
        Item = item;
    }

    public void PurchaseItem()
    {
        if (GameData.Instance.coinCount >= Item.Price)
        {
            switch (Item)
            {
                case Shop.Halsband:
                    GameData.Instance.halsBandCount++;
                    break;
                case Shop.Doppelsprung:
                    GameData.Instance.doubleJumpCount++;
                    break;
                case Shop.GigaBeller:
                    GameData.Instance.gigaBellerCount++;
                    break;
                case Shop.CoinMagnet:
                    GameData.Instance.coinMagnetCount++;
                    break;
                default:
                    break;
            }
            GameData.Instance.SaveData();
            coinManager.RemoveCoins(Item.Price);
        }
        else
        {
            animator.SetTrigger("Broke");
        }
    }
}
