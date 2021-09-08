using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    private AudioController audioController;
   
    private void Start()
    {
        movement = GetComponent<Movement>();
        movement.OnInput(Movement.directions[0]);
        StartCoroutine(movement.MoveOneStep());
    }

    private void Update()
    {
        Vector2Int dir = InputDirection();
        if (dir != Vector2Int.zero)
        {
            movement.OnInput(dir);
        }
    }
    
    Vector2Int InputDirection()
    {
        Vector2Int direction = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Movement.directions[0];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Movement.directions[1];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Movement.directions[2];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Movement.directions[3];
        }
        return direction;
    }
}