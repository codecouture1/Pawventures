using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IPowerUp
{
    public Sprite sprite { get;}
    public AudioClip sound { get; }

    public abstract void ApplyPowerup();
}
