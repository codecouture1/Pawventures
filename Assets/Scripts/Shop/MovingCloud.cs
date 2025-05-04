using UnityEngine;



public class MovingCloud : MonoBehaviour

{

public Image cloudImage;

  public float spawnInterval = 2f; // Time between spawns

  public Transform[] spawnPoints; // Array of spawn points



  private void Start()

  {

    // Start spawning clouds

    InvokeRepeating("SpawnCloud", 0f, spawnInterval);

  }



  void SpawnCloud() 

  {

    if (spawnPoints.Length == 0) 

    {

      Debug.Log("No spawn points available.");

      return; // No spawn points available

    }



    // Choose a random spawn point

    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];



    // Instantiate the cloud prefab at the spawn point

    Instantiate(cloudPrefab, spawnPoint.position, Quaternion.identity);

     

    Debug.Log("Spawned cloud at: " + spawnPoint.position);

  }



  private void OnDrawGizmos()

  {

    // Draw spawn points in the scene view

    Gizmos.color = Color.red;

    foreach (Transform spawnPoint in spawnPoints)

    {

      if (spawnPoint != null)

      {

        Gizmos.DrawSphere(spawnPoint.position, 0.5f);

      }

    }

  }

}