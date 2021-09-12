using UnityEngine;

public class GridCollision : MonoBehaviour
{
    [SerializeField] private UserInterface userInterface;
    [SerializeField] private Food food;
    private Board board;
    private Body body;
    private Death death;
    private AudioController audioController;

    private void Awake()
    {
        board = food.transform.parent.GetComponent<Board>();
        body = GetComponent<Body>();
        death = GetComponent<Death>();
        audioController = GetComponent<AudioController>();
    }

    private void Update()
    {
        if (CheckPlayerPosition(board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            audioController.Play(Random.Range(1, 4));
            userInterface.Score += 1;
            food.MakeVisible(false);
            body.AddBodyParts();
        }

        for (int i = 3; i < body.BodyParts.Count; i++)
        {
            if (CheckPlayerPosition(board.WorldToGrid(body.BodyParts[i].transform.position)))
            {
                death.ExecuteSnake();
            }
        }
    }
    
    private bool CheckPlayerPosition(Vector2Int pos)
    {
        if (board.WorldToGrid(transform.position) == pos)
        {
            return true;
        }
        return false;
    }
}
