using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToMainMenu : MonoBehaviour
{

    public string mainMenuName;
    public string gameSceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenuName);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
