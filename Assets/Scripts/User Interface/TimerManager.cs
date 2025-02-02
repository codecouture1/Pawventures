using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private const float pos1 = 674f;
    private const float pos2 = 264f;
    private const float pos3 = -144f;

    public GameObject[] timers;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTimer(float duration, Sprite sprite, PowerUps powerUp)
    {
        GameObject inactiveTimerObj = null;

        foreach (GameObject timerObj in timers)
        {
            Timer timer = timerObj.GetComponent<Timer>();

            // If a timer for this power-up is found, reset it and return
            if (timer.powerUp.Equals(powerUp))
            {
                timerObj.SetActive(true);
                timer.Set(duration, sprite, powerUp);
                return;
            }

            // Store the first available inactive timer
            if (!timerObj.activeSelf && inactiveTimerObj == null)
            {
                inactiveTimerObj = timerObj;
            }
        }

        // If no active timer was found, start a new timer if an inactive one exists
        if (inactiveTimerObj != null)
        {
            Timer timer = inactiveTimerObj.GetComponent<Timer>();
            inactiveTimerObj.SetActive(true);
            timer.Set(duration, sprite, powerUp);
        }
    }

}
