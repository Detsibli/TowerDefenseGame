using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Temp B";
    public string levelInstructions = "Temp K";
    public SceneFader sceneFader;
    public void Play()
    {
        Debug.Log("Play");
        sceneFader.FadeTo(levelToLoad);
    }
    public void Quit()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }
    public void Instructions()
    {
        Debug.Log("Instructions");
        sceneFader.FadeTo(levelInstructions);
    }
}
