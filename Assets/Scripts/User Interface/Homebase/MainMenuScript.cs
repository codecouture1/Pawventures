using UnityEngine;
// Integration Scenen
using UnityEngine.SceneManagement;

public class MainMenü : MonoBehaviour
{
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