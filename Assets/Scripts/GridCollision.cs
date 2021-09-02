using UnityEngine;

public class GridCollision : MonoBehaviour
{
    [SerializeField] private FoodController food;
    private Board board;
    private BodyController bodyController;
    private UserInterface userInterface;
    private Death death;

    private void Start()
    {
        board = food.transform.parent.GetComponent<Board>();
        bodyController = GetComponent<BodyController>();
        userInterface = GetComponent<UserInterface>();
        death = GetComponent<Death>();
    }

    private void Update()
    {
        if (CheckPlayerPosition(board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            userInterface.Score += 1;
            food.MakeVisible(false);
            bodyController.AddBodyParts();
        }

        for (int i = 3; i < bodyController.BodyParts.Count; i++)
        {
            if (CheckPlayerPosition(board.WorldToGrid(bodyController.BodyParts[i].transform.position)))
            {
                death.GameOver = true;
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
