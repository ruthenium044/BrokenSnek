using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private UserInterface userInterface;
    [SerializeField] private BloodBoard bloodBoard;
    private AudioController audioController;
    private bool gameOver = false;
    private float timer = 5f;
    
    public bool GameOver => gameOver;
    
    public void ExecuteSnake()
    {
        if (!gameOver)
        {
            gameOver = true;
            StopAllCoroutines();
            bloodBoard.AddBlood(transform.position);
        
            GetComponent<AudioController>().Play(0);
            StartCoroutine(ReloadScene());
        }
    }

    private IEnumerator ReloadScene()
    {
        userInterface.GameOver();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(timer);
        userInterface.RestartButton();
    }
}