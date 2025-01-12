using UnityEngine;

public class ResetTutorial : MonoBehaviour
{
    public void TutorialReset()
    {
        GameData.Instance.tutorialCompleted = false;
        GameData.Instance.SaveData();
    }
}
