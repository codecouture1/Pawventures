using UnityEngine;

namespace Shop
{
    public class Doppelsprung : IShopItem
    {
        public GameObject shopManagerObj;
        private ShopManager shopManager;

        public string ItemName { get; } = "Giga Sprung";

        public int Price { get; } = 100;

        public Sprite Sprite
        {
            get { return shopManager.doppelsprungSprite; }
        }

        public string description { get; } = "Beim nächsten Sprung kannst du doppelt Springen!";

        public Doppelsprung()
        {
            shopManagerObj = GameObject.Find("ShopManager");
            shopManager = shopManagerObj.GetComponent<ShopManager>();
        }
    }
}
