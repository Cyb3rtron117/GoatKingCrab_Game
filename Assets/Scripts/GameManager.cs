using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Action onGameStart;
    bool gameStarted;

    public void OnStartPressed()
    {
        AudioManager.instance.PlayMusic(1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            onGameStart?.Invoke();
            gameStarted = true;
        }

    }

    public void GameOver()
    {
        AudioManager.instance.PlayMusic(0);
    }
}
