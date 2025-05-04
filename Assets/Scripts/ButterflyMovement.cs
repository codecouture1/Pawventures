using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public float speed = 100f;
    public float directionChangeInterval = 2f;
    private RectTransform rectTransform;
    private Vector2 direction;
    private float timer;

    private RectTransform canvasRect;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = rectTransform.root.GetComponent<Canvas>().GetComponent<RectTransform>();
        PickNewDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Bewege den Schmetterling
        rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

        // Richtungswechsel nach Zeit
        if (timer >= directionChangeInterval)
        {
            PickNewDirection();
            timer = 0f;
        }

        // Begrenzung auf Canvas: bei Rand – Richtung umkehren
        Vector2 pos = rectTransform.anchoredPosition;
        Vector2 halfSize = canvasRect.sizeDelta / 2f;

        if (Mathf.Abs(pos.x) > halfSize.x || Mathf.Abs(pos.y) > halfSize.y)
        {
            direction = Vector2.Reflect(direction, GetWallNormal(pos, halfSize));
        }
    }

    void PickNewDirection()
    {
        // Zufällige, etwas horizontallastige Richtung
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-0.5f, 0.5f); // Weniger vertikal
        direction = new Vector2(x, y).normalized;
    }

    Vector2 GetWallNormal(Vector2 pos, Vector2 halfSize)
    {
        // Bestimmt Wandnormalen für Reflektion
        if (Mathf.Abs(pos.x) > halfSize.x)
            return new Vector2(-Mathf.Sign(pos.x), 0);
        if (Mathf.Abs(pos.y) > halfSize.y)
            return new Vector2(0, -Mathf.Sign(pos.y));
        return Vector2.zero;
    }
}
