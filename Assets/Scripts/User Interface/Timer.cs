using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
    public Image bar; // The progress bar of the timer
    public Image powerUpImg; // The powerUp sprite (may not be assigned if timer is not used by powerup)

    [HideInInspector] public PowerUps powerUp;

    public bool changeColor;

    Color startColor = Color.green;  // Initial color (Green)
    Color midColor = Color.yellow;   // Midway color (Yellow)
    Color endColor = Color.red;      // Final color (Red)

    private Coroutine timerCoroutine;

    public bool Finished { get; private set; } // Returns true when the timer is finished

    // Event triggered when the timer finishes
    public event Action<Timer> OnTimerFinished;

    public void Set(float duration)
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(StartTimer(duration));
    }

    // Set timer UI with custom sprite for PowerUps
    public void Set(float duration, Sprite sprite, PowerUps powerUp)
    {
        this.powerUp = powerUp;
        powerUpImg.sprite = sprite;

        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(StartTimer(duration));
    }

    private IEnumerator StartTimer(float duration)
    {
        // Set Finished to false
        Finished = false;

        // Ensure fill amount is max
        bar.fillAmount = 1f;
        for (float elapsedTime = 0; elapsedTime <= duration; elapsedTime += Time.deltaTime)
        {
            bar.fillAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Change color
            if (changeColor)
            {
                float progress = elapsedTime / duration; // Normalized time (0 to 1)

                bar.color = progress < 0.5f ?
                    Color.Lerp(startColor, midColor, progress * 2) :
                    Color.Lerp(midColor, endColor, (progress - 0.5f) * 2);
            }

            yield return null;
        }

        Stop();
    }

    public void Stop()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        Finished = true;

        // Invoke the event when the timer finishes
        OnTimerFinished?.Invoke(this);
       
        if(changeColor)
            Destroy(gameObject);
    }
}
