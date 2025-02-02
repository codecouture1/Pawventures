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


    public void AddTimer(float duration, Sprite sprite)
    {
        foreach (GameObject timerObj in timers)
        {
            if (!timerObj.activeSelf)
            {
                timerObj.SetActive(true);
                Timer timer = timerObj.GetComponent<Timer>();
                timer.Set(duration, sprite);
                break;
            }
        }
    }

}
