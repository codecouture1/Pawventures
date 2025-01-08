using UnityEngine;

namespace Shop
{
    public class GigaBeller : IShopItem
    {
        public GameObject shopManagerObj;
        private ShopManager shopManager;

        public string ItemName { get; } = "Gigabeller";

        public int Price { get; } = 300;

        public Sprite Sprite
        {
            get { return shopManager.gigaBellerSprite; }
        }

        public string description { get; } = "Stößt den Jäger zurück!";

        public GigaBeller()
        {
            shopManagerObj = GameObject.Find("ShopManager");
            shopManager = shopManagerObj.GetComponent<ShopManager>();
        }
    }
}
