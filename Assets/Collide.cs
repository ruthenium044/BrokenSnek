using UnityEngine;

public class Collide : MonoBehaviour
{
    [SerializeField] private FoodController food;
    private Board board;
    
    private void Update()
    {
        board = food.transform.parent.GetComponent<Board>();
        if (CheckPosition(board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            Debug.Log("OOOIII");
            food.MakeVisible(false);
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
