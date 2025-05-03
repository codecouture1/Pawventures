using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LevelModule
{
    public enum Collectible
    {
        None,
        Coins,
        PowerUp,
        Polaroid,
    }
    public readonly int id;

    public readonly float width;

    public GameObject prefab; //the prefab that this module represents
    public GameObject instance; //the instance of this modules prefab

    GameObject coins;
    GameObject powerUp;

    public bool ContainsPolaroid { get; private set; }
    public bool ContainsCoins { get; private set; } = false;
    public bool ContainsPowerUp { get; private set; } = false;

    private const int coinIndex = 0; //the coin container must always have the index 0 within the prefab
    private const int powerUpIndex = 1; //the powerup container must always have the index 0 within the prefab

    public LevelModule(GameObject prefab, int id)
    {
        this.prefab = prefab;
        width = GetWidth();
        this.id = id;
    }


    public float GetWidth()
    {
        // Get the Renderer component of the prefab
        Renderer renderer = prefab.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Return the width of the prefab (size in the x-axis)
            return renderer.bounds.size.x;
        }

        // If no Renderer is found, return a default width
        return 1f;
    }

    public void Spawn(Collectible collectible, bool condition)
    {
        switch (collectible)
        {
            case Collectible.Coins:
                SpawnCoins(condition);
                break;
            case Collectible.PowerUp:
                SpawnPowerUp(condition);
                break;
            case Collectible.Polaroid:
                SpawnPolaroid();
                break;
        }
    }

    private void SpawnCoins(bool condition)
    {
        coins = instance.transform.GetChild(coinIndex).gameObject;
        if (coins.CompareTag("CoinContainer")) //checks if object is a CoinContainer 
        {
            ContainsCoins = false;
            if (condition)
            {
                coins.SetActive(true);
                ContainsCoins = true;
            }
        }              
    }

    private void SpawnPowerUp(bool condition)
    {
        powerUp = instance.transform.GetChild(powerUpIndex).gameObject;
        if (powerUp.CompareTag("PowerUp")) //checks if object is a PowerUp
        {
            ContainsPowerUp = false;
            if (condition)
            {
                powerUp.SetActive(true);
                ContainsPowerUp = true;
            }
        }                 
    }

    // TODO: implement
    private void SpawnPolaroid()
    {
        ContainsPolaroid = true;
    }

}
