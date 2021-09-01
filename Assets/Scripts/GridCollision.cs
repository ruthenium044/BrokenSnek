using UnityEngine;

public class GridCollision : MonoBehaviour
{
    [SerializeField] private FoodController food;
    private Board board;
    private BodyController _bodyController;
    private UserInterface userInterface;

    private void Start()
    {
        _bodyController = GetComponent<BodyController>();
        userInterface = GetComponent<UserInterface>();
    }

    private void Update()
    {
        board = food.transform.parent.GetComponent<Board>();
        if (CheckPosition(board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            userInterface.Score += 1;
            food.MakeVisible(false);
            _bodyController.AddBodyParts();
        }
    }

    private bool CheckPosition(Vector2Int pos)
    {
        if (board.WorldToGrid(transform.position) == pos)
        {
            return true;
        }
        return false;
    }
}
