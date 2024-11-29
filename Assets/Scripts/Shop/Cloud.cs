using UnityEngine;



public class Cloud : MonoBehaviour

{

  public float floatStrength = 0.5f; // Controls how high the cloud floats

  public float floatDistance = 0.5f;  // Distance of the float

  public float moveSpeed = 2.0f;    // Speed of horizontal movement



  private Vector3 startPosition;



  void Start()

  {

    startPosition = transform.position; // Store the initial position

  }



  void Update()

  {

    // Floating motion using a fixed speed

    float newY = startPosition.y + Mathf.Sin(Time.time) * floatDistance;

    transform.position = new Vector3(transform.position.x, newY, transform.position.z);



    // Horizontal movement (always moving left)

    transform.position += Vector3.left * moveSpeed * Time.deltaTime;

  }

}