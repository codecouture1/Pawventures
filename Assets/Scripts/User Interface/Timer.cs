using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Image bar; //the progress bar of the timer

    public Image powerUpImg; //the powerUp sprite (may not be assigned if timer is not used by powerup)

    [HideInInspector] public PowerUps powerUp;

    public bool changeColor;

    Color startColor = Color.green;  // Initial color (Green)
    Color midColor = Color.yellow;   // Midway color (Yellow)
    Color endColor = Color.red;      // Final color (Red)

    private Coroutine timerCoroutine;

    public bool Finished { get; private set; } //returns true as soon as the timer is finished

    public void Set(float duration)
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(StartTimer(duration));
    }

    //set timer UI with custom Sprite for PowerUps
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
        //set Finished to false
        Finished = false;

        //ensure fill amount is max
        bar.fillAmount = 1f;
        for (float elapsedTime = 0; elapsedTime <= duration; elapsedTime += Time.deltaTime)
        {
            bar.fillAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            //change color
            if (changeColor)
            {
                float progress = elapsedTime / duration; // Normalized time (0 to 1)

                if (progress < 0.5f)
                {
                    // First half (Green to Yellow)
                    bar.color = Color.Lerp(startColor, midColor, progress * 2);
                }
                else
                {
                    // Second half (Yellow to Red)
                    bar.color = Color.Lerp(midColor, endColor, (progress - 0.5f) * 2);
                }
            }


            yield return null;
        }

        Stop();
    }

    public void Stop()
    {
        timerCoroutine = null;
        Finished = true;
        gameObject.SetActive(false);
    }
}
