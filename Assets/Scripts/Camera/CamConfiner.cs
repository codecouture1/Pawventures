using System;
using UnityEngine;

public class CamConfiner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Collider2D coll;
    private bool _playerEntered = false;
    public bool PlayerEntered
    {
        get { return _playerEntered; }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _playerEntered = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _playerEntered = false;
        }
    }
}
