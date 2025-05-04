using UnityEngine;

public class RotateSlowly : MonoBehaviour
{
    public float speed = 5f;
    public float maxAngle = 15f;  // Maximale Drehung in Grad
    private float currentAngle = 0f;
    private bool rotatingClockwise = true;  // Gibt an, in welche Richtung die Sonne sich dreht

    void Update()
    {
        // Berechnung der Drehung pro Frame
        float rotationChange = speed * Time.deltaTime;

        // Wenn wir im Uhrzeigersinn drehen
        if (rotatingClockwise)
        {
            // Wenn der maximale Winkel erreicht ist, wechseln wir die Drehrichtung
            if (currentAngle + rotationChange >= maxAngle)
            {
                rotationChange = maxAngle - currentAngle; // Verhindert, dass wir über den max Winkel hinausgehen
                rotatingClockwise = false; // Richtung ändern
            }
            transform.Rotate(Vector3.forward * rotationChange);
            currentAngle += rotationChange;
        }
        else  // Wenn wir gegen den Uhrzeigersinn drehen
        {
            // Wenn der Winkel -maxAngle erreicht ist, wechseln wir die Drehrichtung
            if (currentAngle - rotationChange <= -maxAngle)
            {
                rotationChange = currentAngle + maxAngle; // Verhindert, dass wir unter den -maxAngle-Winkel gehen
                rotatingClockwise = true; // Richtung ändern
            }
            transform.Rotate(Vector3.forward * -rotationChange);
            currentAngle -= rotationChange;
        }
    }
}
