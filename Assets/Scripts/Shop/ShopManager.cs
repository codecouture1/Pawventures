using UnityEngine;
using UnityEngine.UI;
using Shop;

public class ShopManager : MonoBehaviour
{
    public Button[] itemButtons;
    public GameObject ShopPanel;
    private ShopPanel shopPanelScript;

    public Sprite halsbandSprite;
    public Sprite gigaBellerSprite;
    public Sprite doppelsprungSprite;
    public Sprite münzmagnetSprite;

    public void ActivatePanel(IShopItem item)
    {
        ShopPanel.SetActive(true);
        shopPanelScript.SetItem(item);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        shopPanelScript = ShopPanel.GetComponent<ShopPanel>();
    }

    void Start()
    {
        itemButtons[0].onClick.AddListener(() => ActivatePanel(new Shop.GigaBeller()));
        itemButtons[1].onClick.AddListener(() => ActivatePanel(new Shop.Doppelsprung()));
        itemButtons[2].onClick.AddListener(() => ActivatePanel(new Shop.Halsband()));
        itemButtons[3].onClick.AddListener(() => ActivatePanel(new Shop.CoinMagnet()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
