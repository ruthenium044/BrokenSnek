using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : MonoBehaviour
{
    private Board board;
    
    private Vector2 timeFoodSpawn = new Vector2(5f, 8f);
    private Vector2 timeFoodDeSpawn = new Vector2(1f, 3f);

    private SpriteRenderer spriteRenderer;
    private  bool isVisible = false;

    public bool IsVisible
    {
        get => isVisible;
        set => isVisible = value;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        board = transform.parent.GetComponent<Board>();
        
        MakeVisible(false);
        StartCoroutine(SpawnFood());
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            PlaceFood(new Vector2Int(Random.Range(0, board.BoardSize.x - 1), Random.Range(0, board.BoardSize.y - 1)));
            yield return new WaitForSeconds (Random.Range(timeFoodSpawn.x, timeFoodSpawn.y));
            
            MakeVisible(false);
            yield return new WaitForSeconds (Random.Range(timeFoodDeSpawn.x, timeFoodDeSpawn.y));
        }
    }

    private void PlaceFood(Vector2Int pos)
    {
        MakeVisible(true);
        transform.position = board.GridToWorld(pos);
    }

    public void MakeVisible(bool state)
    {
        IsVisible = state;
        if (state)
        {
            spriteRenderer.color = Color.white;
            
        }
        else
        {
            spriteRenderer.color = Color.clear;
        }
    }
    
}
