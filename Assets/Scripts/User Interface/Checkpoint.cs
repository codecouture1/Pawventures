using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public TextMeshProUGUI checkpointCounter; //how many checkpoints items the player owns
    public TextMeshProUGUI remainingUses; //how many checkpoint uses are remaining

    public GameObject CheckpointMenu;
    public GameObject CheckpointRemaining;

    public bool Set { get; private set; } //returns true if this checkpoint has been set
    int thisLevel; //the current level

    private void Awake()
    {
        checkpointCounter.text = $"x{GameData.Instance.checkPointCount}";

        //set thisLevel to the current level number
        thisLevel = MainMenu.GetLevel();

        //checks if a checkpoint has been set for this level
        if (GameData.Instance.levelRemain[thisLevel] > 0)
            Set = true;
        else
            Set = false;

        //open the checkpoint menu if a checkpoint hasn't been set and the player owns checkpoints
        if(!SceneHasCheckpoint() && GameData.Instance.checkPointCount > 0)
        {
            CheckpointMenu.SetActive(true);
        }

        //check if the current scene has a checkpoint
        if (SceneHasCheckpoint())
        {
            //if remaining checkpoint uses are >1, decrease the remaining uses by 1
            if (GameData.Instance.levelRemain[thisLevel] > 0)
            {
                DecreaseRemaining();
            }

            //if only one use remains, reset the next scene load to the first chapter
            if (GameData.Instance.levelRemain[thisLevel] == 0)
            {
                CheckpointMenu.SetActive(true);
                //if the checkpoint expired, reset level start to first chapter
                GameData.Instance.levelStart[thisLevel] = MainMenu.GetFirstChapter(thisLevel);
                Debug.Log("No checkpoint uses remaining, reset starting point to chapter 1");
                GameData.Instance.SaveData();
                Set = false;
            }

            //show remaining checkpoint uses
            CheckpointRemaining.SetActive(true);
            remainingUses.text = $"Noch {GameData.Instance.levelRemain[thisLevel]}";
        }    

    }

    //set a new checkpoint at the current chapter
    public void SetCheckpoint()
    {
        GameData.Instance.checkPointCount--;
        GameData.Instance.levelStart[thisLevel] = SceneManager.GetActiveScene().buildIndex;
        GameData.Instance.levelRemain[thisLevel] = 5;
        Debug.Log($"Set this checkpoint for Level {MainMenu.GetLevel() + 1}");
        Set = true;

        GameData.Instance.SaveData();
        gameObject.SetActive(false);
    }

    //decrease the remaining uses of the checkpoint by 1. If no uses remain, set the level start back to chapter 1
    public void DecreaseRemaining()
    {
        //decrease remaining checkpoint uses
        GameData.Instance.levelRemain[thisLevel]--;
        Debug.Log($"Remaining checkpoint uses in this chapter: {GameData.Instance.levelRemain[thisLevel]}");
        
        GameData.Instance.SaveData();
    }

    //closes the checkpoint menu
    public void CloseCheckpointMenu()
    {
        gameObject.SetActive(false);
    }

    //return true if the current scene has a checkpoint
    private bool SceneHasCheckpoint()
    {
        return (SceneManager.GetActiveScene().buildIndex.Equals(GameData.Instance.levelStart[thisLevel]));
    }
}
