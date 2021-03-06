using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;
    
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;
    
    public SceneFader sceneFader;
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;
    }

    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("LEVEL WON!");
        PlayerPrefs.GetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }
}
