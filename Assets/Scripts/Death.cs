using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private BloodBoardController bloodBoardController;
    [SerializeField] private float timer = 5f;
    private AudioController audioController;
    private UserInterface userInterface;
    private bool gameOver = false;

    private void Awake()
    {
        userInterface = GetComponent<UserInterface>();
    }

    public bool GameOver
    {
        get => gameOver;
    }
    
    public void ExecuteSnake()
    {
        StopAllCoroutines();
        bloodBoardController.AddBlood(transform.position);
        gameOver = true;
        GetComponent<AudioController>().Play(0);
        userInterface.GameOver();
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(timer);
        userInterface.RestartButton();
    }
    
}
