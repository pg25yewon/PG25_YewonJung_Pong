using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Loads scene based on string entered in editor
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Quits the application once in a .exe
    //will not do anything in editor runtime
    public void QuitGame()
    {
        Application.Quit();
    }
}