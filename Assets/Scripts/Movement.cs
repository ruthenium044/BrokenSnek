using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Board board;
    private Body body;
    private Death death;
    private GridCollision gridCollision;
    
    public static readonly List<Vector2Int> directions = new List<Vector2Int>
            {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
    private Vector2Int direction;
    private Vector2Int inputDirection;

    private float timeBetweenSteps = 0.4f;

    private void Awake()
    {
        body = GetComponent<Body>();
        death = GetComponent<Death>();
        gridCollision = GetComponent<GridCollision>();
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
        Move(inputDirection);
        gridCollision.Collide(board, body, death);
        yield return new WaitForSeconds(timeBetweenSteps);
        StartCoroutine(MoveOneStep());
    }
    
    private void Move(Vector2Int dir)
    {
        direction = dir;
        Vector2Int positionOnBoard = board.WorldToGrid(transform.position);
        positionOnBoard += dir;

        if (IsOutOfBounds(positionOnBoard))
        {
            death.ExecuteSnake();
        }
        positionOnBoard = ClampToBounds(positionOnBoard);
        Vector3 position = board.GridToWorld(positionOnBoard);
        
        if (position != transform.position)
        {
            body.MoveBodyParts();
        }
        transform.position = position;
        body.RotateBodyParts(this);
    }

    private bool IsOutOfBounds(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > board.BoardSize.x - 1 || pos.y < 0 || pos.y > board.BoardSize.y - 1)
        {
            return true;
        }
        return false;
    }

    private Vector2Int ClampToBounds(Vector2Int pos)
    {
        pos.x = Mathf.Clamp(pos.x, 0, board.BoardSize.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, board.BoardSize.y - 1);
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
