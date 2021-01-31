using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{


    public string loadGameScene;


    public void LoadGame()
    {
        SceneManager.LoadScene(loadGameScene);
    }


}
