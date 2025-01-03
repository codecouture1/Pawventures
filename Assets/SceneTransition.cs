using TMPro;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public TextMeshProUGUI LevelAndChapter;
    public TextMeshProUGUI Title;

    public int level;
    public int chapter;
    public string title;

    void Awake()
    {
        gameObject.SetActive(true);
    }


    void Start()
    {
        if(LevelAndChapter != null && Title != null)
        {
            LevelAndChapter.text = $"Level {level.ToString()} - Kapitel {chapter.ToString()}";
            Title.text = title;
        }
    }
}
