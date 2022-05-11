using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public string levelLoad = "MainMenu";
    public SceneFader sceneFader;
    
    public void Back()
    {
        sceneFader.FadeTo(levelLoad);
    }
}
