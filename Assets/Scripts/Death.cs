using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    private AudioController audio;
    private bool gameOver = false;
    
    public bool GameOver
    {
        get => gameOver;
    }
    
    public void ExecuteSnake()
    {
        gameOver = true;
        GetComponent<AudioController>().Play(0);
    }
    
}
