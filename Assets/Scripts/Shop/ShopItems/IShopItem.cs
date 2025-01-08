using UnityEngine;

 public interface IShopItem
    {
        public string ItemName { get; }
        public int Price { get; }
        public Sprite Sprite { get; }
        public string description { get; }
    }
