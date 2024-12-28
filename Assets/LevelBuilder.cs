using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class LevelBuilder : MonoBehaviour
{
    //TODO: Coins und PowerUp platzieren und diese mit einer bestimmten wahrscheinlichkeit spawnen lassen

    public GameObject[] prefab;
    private GameObject current;
    private GameObject previous;
    private float spawnAtX = 0;

    void Start()
    {
        for (int c = 0; c < 10; c++)
        {
            current = prefab[Random.Range(0, prefab.Length)];
            if(previous != null)
            {
                spawnAtX += GetPrefabWidth(previous);
                Instantiate(current, new Vector3(spawnAtX, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(current, new Vector3(0, 0, 0), Quaternion.identity);
            }
            previous = current;
        }
    }

    float GetPrefabWidth(GameObject prefab)
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
}
