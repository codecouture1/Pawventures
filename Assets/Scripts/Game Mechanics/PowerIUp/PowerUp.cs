using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{

    //TODO: Destroy when action complete?

    private SpriteRenderer spriteRenderer;

    public abstract void ApplyPowerup();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (collision.CompareTag("Player"))
        {
            ApplyPowerup();
            spriteRenderer.enabled = false;
        }
    }

}
