using UnityEngine;
// Integration Scenen
using UnityEngine.SceneManagement;

public class MainMenü : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartLevelOne()
    {
        SceneManager.LoadScene(3);
    }

     public void StartLevelTwo()
    {
            SceneManager.LoadScene(4);
    }

     public void StartLevelThree()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadInterviewSequence()
    {
        SceneManager.LoadScene(6);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
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