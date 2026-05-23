using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Start UI")]
    [SerializeField] GameObject startText;

    [SerializeField] GameObject puasePanel;

    void Start()
    {
        GameManager.instance.onGameStart += StartGame;
    }

    void StartGame()
    {
        //Disable start text
        startText.SetActive(false);

    }



}
