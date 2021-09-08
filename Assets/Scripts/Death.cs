using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private BloodBoard bloodBoard;
    private AudioController audioController;
    private UserInterface userInterface;
    private bool gameOver = false;
    private float timer = 5f;
    
    public bool GameOver => gameOver;

    private void Awake()
    {
        userInterface = GetComponent<UserInterface>();
    }
    
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
        yield return new WaitForSeconds(timer);
        userInterface.RestartButton();
    }
    
}
