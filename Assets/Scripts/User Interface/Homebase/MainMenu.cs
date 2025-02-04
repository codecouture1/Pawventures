using System.Collections;
using UnityEngine;
// Integration Scenen
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator exitAnimator;
    public GameObject disclaimer;

    //-------------Menu-------------

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadInterviewSequence()
    {
        SceneManager.LoadScene(3);
    }

    //-------------Level-------------

    public void StartLevelOne()
    {
        SceneManager.LoadScene(4);
    }

     public void StartLevelTwo()
    {
        SceneManager.LoadScene(7);
    }

     public void StartLevelThree()
    {
        SceneManager.LoadScene(10);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadSceneWithDelay(int sceneIndex)
    {
        StartCoroutine(WaitThenLoadScene(1f, sceneIndex));
        exitAnimator.SetTrigger("SceneExit");
    }

    private IEnumerator WaitThenLoadScene(float seconds, int sceneIndex)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneIndex);
        yield return null;
    }

    //returns the current level as an integer number (starting with 0)
    public static int GetLevel()
    {
        int lvl = SceneManager.GetActiveScene().buildIndex;

        if (lvl >= 4 && lvl <= 6)
            return 0;

        if (lvl >= 7 && lvl <= 9)
            return 1;

        if (lvl >= 10 && lvl <= 12)
            return 2;

        throw new System.Exception($"current scene ({lvl}) is not part of a level");
    }

    //return the the scene index of the first chapter of this level
    public static int GetFirstChapter(int level)
    {
        switch (level)
        {
            case 0:
                return 4;
            case 1:
                return 7;
            case 2:
                return 10;
        }

        throw new System.Exception($"current scene ({level}) is not part of a level");
    }


    private void Start()
    {
        if (disclaimer != null && !GameData.Instance.readDisclaimer)
        {
            disclaimer.SetActive(true);
        }
    }

    public void ReadDisclaimer()
    {
        GameData.Instance.readDisclaimer = true;
    }


    public void Exit()
    {
#if UNITY_EDITOR
        // Im Unity Editor wird dieser Code ausgeführt
           UnityEditor.EditorApplication.isPlaying = false;
#else
        // Im Build wird dieser Code ausgeführt
        Application.Quit();
#endif
    }
}