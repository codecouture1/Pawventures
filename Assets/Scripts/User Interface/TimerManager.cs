using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameObject timerPrefab;

    // Adds a new Timer to the UI
    public void AddTimer(float duration, Sprite sprite, PowerUps powerUp)
    {
        Timer timer;

        // Check if a timer for that powerup is already running and reset that timer
        foreach (Transform child in transform)
        {
            timer = child.gameObject.GetComponent<Timer>();

            if (timer.powerUp.Equals(powerUp))
            {
                timer.Set(duration, sprite, powerUp);
                return;
            }
        }

        // Instantiate a new timer GameObject
        GameObject TimerObj = Instantiate(timerPrefab, transform);
        TimerObj.SetActive(true);

        // Get the next free position and move the timer GameObject there
        RectTransform rectTransform = TimerObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = GetNextPosition();

        // Get the timer component and set it
        timer = TimerObj.GetComponent<Timer>();
        timer.Set(duration, sprite, powerUp);

        // Subscribe to the timer's completion event
        timer.OnTimerFinished += HandleTimerFinished;
    }

    // Handle when a timer finishes
    private void HandleTimerFinished(Timer timer)
    {
        // Unsubscribe from the event to avoid memory leaks
        timer.OnTimerFinished -= HandleTimerFinished;

        // Destroy the timer GameObject
        Destroy(timer.gameObject);

        // Reorganize remaining timers
        ReorganizeTimers();
    }

    // Reorganize timers when one is removed
    private void ReorganizeTimers()
    {
        float initialX = 674f;
        float offset = 410f;

        int index = -1;
        foreach (Transform child in transform)
        {
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            float targetX = initialX - (index * offset);

            // Smoothly animate the movement
            LeanTween.moveX(rectTransform, targetX, 0.2f).setEase(LeanTweenType.easeOutQuad);

            index++;
        }
    }

    // Determine the next free position
    private Vector2 GetNextPosition()
    {
        float initialX = 674f;
        float offset = 410f;

        int activeTimers = -1;
        foreach (Transform child in transform)
            activeTimers++;

        float nextPositionX = initialX - (activeTimers * offset);
        return new Vector2(nextPositionX, -310f);
    }
}
