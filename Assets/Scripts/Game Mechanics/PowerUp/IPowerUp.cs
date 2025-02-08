using UnityEngine;

public interface IPowerUp
{
    //the PowerUps enum that represents this powerup
    public PowerUps PowerUp { get; }

    //the sprite that represents this powerup
    public Sprite Sprite { get;}

    //the audioclip that plays when the powerup is applied
    public AudioClip Sound { get; }

    //the duration of the effect (may not be assigned if effect is instant)
    public float Duration { get; }

    //define the behavior of this powerup here
    public abstract void ApplyPowerup();

    //create a powerup object based on the corresponding enum;
    public static IPowerUp GetPowerUp(PowerUps powerUp)
    {
        switch (powerUp)
        {
            case PowerUps.Halsband:
                return new Halsband();
            case PowerUps.Doppelsprung:
                return new Doppelsprung();
            case PowerUps.GigaBeller:
                return new GigaBeller();
            case PowerUps.CoinMagnet:
                return new CoinMagnet();
            case PowerUps.DoubleCoins:
                return new DoubleCoins();
            case PowerUps.None:
                return null;
            default:
                return null;
        }
    }
}
