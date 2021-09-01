using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Board board;
    private Body body;
    
    public static readonly List<Vector2Int> directions = new List<Vector2Int>
            {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
    private Vector2Int direction;
    private float timeBetweenSteps = 0.4f;

    private void Awake()
    {
        body = GetComponent<Body>();
    }

    public void OnInput(Vector2Int dir)
    {
        if (dir != -direction)
        {
            direction = dir;
            FlipSprite();
        }
    }
    
    public IEnumerator MoveOneStep()
    {
        while (true)
        {
            Move(direction);
            yield return new WaitForSeconds (timeBetweenSteps);
        }
    }
    
    private void Move(Vector2Int dir)
    {
        Vector2Int positionOnBoard = board.WorldToGrid(transform.position);
        positionOnBoard += dir;
        positionOnBoard = ClampToBounds(positionOnBoard);
        Vector3 position = board.GridToWorld(positionOnBoard);
        if (position != transform.position)
        {
            body.MoveBodyParts();
        }
        transform.position = position;
    }

    Vector2Int ClampToBounds(Vector2Int pos)
    {
        pos.x = Mathf.Clamp(pos.x, 0, board.BoardSize.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, board.BoardSize.y - 1);
        return pos;
    }

    private void FlipSprite()
    {
        if (direction == directions[0])
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (direction == directions[1])
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (direction == directions[2])
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction == directions[3])
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
