using UnityEngine;

public class TailController : MonoBehaviour
{
    private PlayerController player = null;
    private Movement movement = null;

    private void Start()
    {
        movement = GetComponent<Movement>();
        player = transform.parent.GetComponent<PlayerController>();
        
        movement.OnInput(player.prevDirection);
        StartCoroutine(movement.MoveOneStep());
    }

    private void Update()
    {
        Vector2Int dir = player.prevDirection;
        if (dir != Vector2Int.zero)
        {
            movement.OnInput(dir);
        }
    }
}
