using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSceneBTM : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
        }
    }
}
