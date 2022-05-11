using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text roundsText;

    public string menuSceneName = "MainMenu";
   // public string loadSceneName = "Temp B";
    public PausedMenu pausedMenu;
    public SceneFader sceneFader;
    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }
    public void Retry()
    {
        pausedMenu.Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        pausedMenu.Toggle();
        Debug.Log("Go to menu.");
        sceneFader.FadeTo(menuSceneName);
    }
}
