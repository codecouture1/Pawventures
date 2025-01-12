using UnityEngine;

namespace Shop
{
    public class Halsband : IShopItem
    {
        public GameObject shopManagerObj;
        private ShopManager shopManager;

        public string ItemName { get; } = "Halsband";

        public int Price { get; } = 50;

        public Sprite Sprite
        {
            get { return shopManager.halsbandSprite; }
        }

        public string description { get; } = "verleiht dir einen Extra-Treffer!";

        public Halsband()
        {
            shopManagerObj = GameObject.Find("ShopManager");
            shopManager = shopManagerObj.GetComponent<ShopManager>();
        }

    }
}
