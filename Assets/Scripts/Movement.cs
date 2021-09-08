using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    private BodyController bodyController;
    private Death death;
    
    public static readonly List<Vector2Int> directions = new List<Vector2Int>
            {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
    private Vector2Int direction;
    private Vector2Int inputDirection;

    private float timeBetweenSteps = 0.4f;

    private void Awake()
    {
        bodyController = GetComponent<BodyController>();
        death = GetComponent<Death>();
    }

    public void OnInput(Vector2Int dir)
    {
        if (dir != -direction && !death.GameOver)
        {
            inputDirection = dir;
            RotateSprite(transform, inputDirection);
        }
    }
    
    public IEnumerator MoveOneStep()
    {
        while (!death.GameOver)
        {
            Move(inputDirection);
            yield return new WaitForSeconds (timeBetweenSteps);
        }
    }
    
    private void Move(Vector2Int dir)
    {
        direction = dir;
        Vector2Int positionOnBoard = boardController.WorldToGrid(transform.position);
        positionOnBoard += dir;

        if (IsOutOfBounds(positionOnBoard))
        {
            death.ExecuteSnake();
        }
        positionOnBoard = ClampToBounds(positionOnBoard);
        Vector3 position = boardController.GridToWorld(positionOnBoard);
        
        if (position != transform.position)
        {
            bodyController.MoveBodyParts();
        }
        transform.position = position;
        bodyController.RotateBodyParts();
    }

    private bool IsOutOfBounds(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > boardController.BoardSize.x - 1 || pos.y < 0 || pos.y > boardController.BoardSize.y - 1)
        {
            return true;
        }
        return false;
    }

    private Vector2Int ClampToBounds(Vector2Int pos)
    {
        pos.x = Mathf.Clamp(pos.x, 0, boardController.BoardSize.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, boardController.BoardSize.y - 1);
        return pos;
    }

    public void RotateSprite(Transform obj, Vector2Int dir)
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
