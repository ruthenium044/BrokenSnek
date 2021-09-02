using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    private bool gameOver = false;
    
    public bool GameOver
    {
        get => gameOver;
        set => gameOver = value;
    }
    
}
