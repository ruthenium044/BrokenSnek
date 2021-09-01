using System;
using UnityEngine;

public class Collide : MonoBehaviour
{
    [SerializeField] private FoodController food;
    private Board board;
    private Body body;
    private UIcontroller UIcontroller;

    private void Start()
    {
        body = GetComponent<Body>();
        UIcontroller = GetComponent<UIcontroller>();
    }

    private void Update()
    {
        board = food.transform.parent.GetComponent<Board>();
        if (CheckPosition(board.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            UIcontroller.Score += 1;
            food.MakeVisible(false);
            body.AddBodyParts();
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
