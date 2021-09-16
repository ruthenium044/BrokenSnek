using UnityEngine;

public class GridCollision : MonoBehaviour
{
    [SerializeField] private UserInterface userInterface;
    [SerializeField] private Food food;

    public void Collide(Board board, Body body, Death death)
    {
        CollideFood(board, body);
        CollideBody(board, body, death);
    }

    private void CollideFood(Board board, Body body)
    {
        if (CheckPlayerPosition(board,board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            GetComponent<AudioController>().Play(Random.Range(1, 4));
            userInterface.IncreaseScore();
            food.MakeVisible(false);
            body.AddBodyParts();
        }
    }

    private void CollideBody(Board board, Body body, Death death)
    {
        for (int i = 3; i < body.BodyParts.Count; i++)
        {
            if (CheckPlayerPosition(board,board.WorldToGrid(body.BodyParts[i].transform.position)))
            {
                death.ExecuteSnake();
            }
        }
    }

    private bool CheckPlayerPosition(Board board, Vector2Int pos)
    {
        if (board.WorldToGrid(transform.position) == pos)
        {
            return true;
        }
        return false;
    }
}
