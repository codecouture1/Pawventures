using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Shop;

public class ShopPanel : MonoBehaviour
{
    private PlayerData playerData = new();

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
        playerData = PlayerDataManager.LoadData();
        if (playerData.coinCount >= Item.Price)
        {
            switch (Item)
            {
                case Shop.Halsband:
                    playerData.halsBandCount++;
                    break;
                case Shop.Doppelsprung:
                    playerData.doubleJumpCount++;
                    break;
                case Shop.GigaBeller:
                    playerData.gigaBellerCount++;
                    break;
                case Shop.CoinMagnet:
                    playerData.coinMagnetCount++;
                    break;
                default:
                    break;
            }
            PlayerDataManager.SaveData(playerData);
            coinManager.RemoveCoins(Item.Price);
        }
        else
        {
            animator.SetTrigger("Broke");
        }
    }

    private void Awake()
    {
        playerData = PlayerDataManager.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
