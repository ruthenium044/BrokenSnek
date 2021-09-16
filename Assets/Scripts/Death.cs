using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private UserInterface userInterface;
    [SerializeField] private BloodBoard bloodBoard;
    private bool gameOver = false;
    private float timer = 3f;
    
    public bool GameOver => gameOver;
    
    public void ExecuteSnake()
    {
        if (!gameOver)
        {
            gameOver = true;
            StopAllCoroutines();
            
            bloodBoard.AddBlood(transform.position);
            GetComponent<AudioController>().Play(0);
            
            userInterface.GameOver();
            Time.timeScale = 0;
            StartCoroutine(WaitForReload());
        }
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSecondsRealtime(timer);
        userInterface.RestartButton();
    }
}