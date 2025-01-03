using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IPowerUp
{
    //the sprite that represents this powerup
    public Sprite sprite { get;}

    //the audioclip that plays when the powerup is applied
    public AudioClip sound { get; }

    //define the behavior of this powerup here
    public abstract void ApplyPowerup();
}
