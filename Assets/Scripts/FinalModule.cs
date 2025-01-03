using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalModule : MonoBehaviour
{
    public int sceneIndex;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

}
