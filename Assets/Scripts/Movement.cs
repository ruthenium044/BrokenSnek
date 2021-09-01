using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Board board;
    private BodyController bodyController;
    
    public static readonly List<Vector2Int> directions = new List<Vector2Int>
            {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
    private Vector2Int direction;
    private float timeBetweenSteps = 0.4f;

    private void Awake()
    {
        bodyController = GetComponent<BodyController>();
    }

    public void OnInput(Vector2Int dir)
    {
        if (dir != -direction)
        {
            direction = dir;
            FlipSprite(transform, direction);
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
            bodyController.MoveBodyParts();
        }
        transform.position = position;
    }

    Vector2Int ClampToBounds(Vector2Int pos)
    {
        pos.x = Mathf.Clamp(pos.x, 0, board.BoardSize.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, board.BoardSize.y - 1);
        return pos;
    }

    public void FlipSprite(Transform obj, Vector2Int dir)
    {
        if (dir == directions[0])
        {
            obj.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (dir == directions[1])
        {
            obj.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (dir == directions[2])
        {
            obj.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dir == directions[3])
        {
            obj.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
