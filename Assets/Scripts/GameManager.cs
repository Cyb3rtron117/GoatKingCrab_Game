using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Action onGameStart;
    bool gameStarted;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            onGameStart?.Invoke();
            gameStarted = true;
        }

    }
}
