using UnityEngine;

namespace Shop
{
    public class CoinMagnet : IShopItem
    {
        public GameObject shopManagerObj;
        private ShopManager shopManager;

        public string ItemName { get; } = "M�nzmagnet";

        public int Price { get; } = 200;

        public Sprite Sprite
        {
            get { return shopManager.m�nzmagnetSprite; }
        }

        public string description { get; } = "WORK IN PROGRESS!";

        public CoinMagnet()
        {
            shopManagerObj = GameObject.Find("ShopManager");
            shopManager = shopManagerObj.GetComponent<ShopManager>();
        }
    }
}
