using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public TextMeshProUGUI checkpointCounter;

    private void OnEnable()
    {
        checkpointCounter.text = $"x{GameData.Instance.checkPointCount}";
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void SetCheckpoint()
    {
        gameObject.SetActive(false);
    }

    public void CloseCheckpointMenu()
    {
        gameObject.SetActive(false);
    }
}
