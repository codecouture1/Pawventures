using UnityEngine;
using System;
using System.Collections;
using NUnit.Framework;

[System.Serializable]
public class PlayerData
{
    //coins
    public int coinCount;

    //amount of purchased items in inventory
    public int gigaBellerCount;
    public int doubleJumpCount;
    public int coinMagnetCount;
    public int halsBandCount;

    //equipped powerUps
    public PowerUps firstPowerUp;
    public PowerUps secondPowerUp;
}
