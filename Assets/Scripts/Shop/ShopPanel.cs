using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    // the shop-item currently displayed by the panel
    public ShopItem item;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;
    public Image image;
    public TextMeshProUGUI description;
    public Button buyButton;
    public Animator animator;
    public CoinManager coinManager;

    // Panel, das angezeigt wird, wenn der Kauf erfolgreich war
    public GameObject successPanel;

    // set the values when the panel becomes enabled
    private void OnEnable()
    {
        itemName.text = item.ItemName;
        price.text = item.Price.ToString();
        image.sprite = item.Sprite;
        description.text = item.description;

        // Erfolgspanel beim Öffnen des ShopPanels deaktivieren
        if (successPanel != null)
            successPanel.SetActive(false);
    }

    // purchase the item
    public void Buy()
    {
        // check if player owns enough coins to buy the item
        if (GameData.Instance.coinCount >= item.Price)
        {
            // check which item is active and add +1 to the count in the game data
            switch (item.powerUp)
            {
                case PowerUps.Halsband:
                    GameData.Instance.halsBandCount++;
                    break;
                case PowerUps.Doppelsprung:
                    GameData.Instance.doubleJumpCount++;
                    break;
                case PowerUps.GigaBeller:
                    GameData.Instance.gigaBellerCount++;
                    break;
                case PowerUps.CoinMagnet:
                    GameData.Instance.coinMagnetCount++;
                    break;
                case PowerUps.Revive:
                    GameData.Instance.reviveCount++;
                    break;
                case PowerUps.Checkpoint:
                    GameData.Instance.checkPointCount++;
                    break;
                case PowerUps.DoubleCoins:
                    GameData.Instance.doubleCoinCount++;
                    break;
                default:
                    break;
            }

            // remove the spent coins from the coin count
            coinManager.RemoveCoins(item.Price);
            GameData.Instance.SaveData();

            // Erfolgspanel anzeigen, wenn vorhanden
            if (successPanel != null)
                successPanel.SetActive(true);
        }
        else
        {
            animator.SetTrigger("Broke");
        }
    }
}
