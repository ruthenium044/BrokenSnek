using UnityEngine;

public class GridCollision : MonoBehaviour
{
    [SerializeField] private FoodController food;
    private Board board;
    private BodyController bodyController;
    private UserInterface userInterface;
    private Death death;
    private AudioController audio;

    private void Awake()
    {
        board = food.transform.parent.GetComponent<Board>();
        bodyController = GetComponent<BodyController>();
        userInterface = GetComponent<UserInterface>();
        death = GetComponent<Death>();
        audio = GetComponent<AudioController>();
    }

    private void Update()
    {
        if (CheckPlayerPosition(board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            audio.Play(Random.Range(1, 4));
            userInterface.Score += 1;
            food.MakeVisible(false);
            bodyController.AddBodyParts();
        }

        for (int i = 3; i < bodyController.BodyParts.Count; i++)
        {
            if (CheckPlayerPosition(board.WorldToGrid(bodyController.BodyParts[i].transform.position)))
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
