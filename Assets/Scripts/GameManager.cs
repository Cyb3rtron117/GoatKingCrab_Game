using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Action onGameStart;
    bool gameStarted;


    void Update()
    {

        onGameStart?.Invoke();
        gameStarted = true;
        

    }


}
