using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    [SerializeField] private Vector2 timeFoodSpawn = new Vector2(5f, 8f);
    [SerializeField] private Vector2 timeFoodDeSpawn = new Vector2(0.5f, 2f);
    
    private Board board;
    private SpriteRenderer spriteRenderer;
    private  bool isVisible;

    public bool IsVisible
    {
        get => isVisible;
        private set => isVisible = value;
    }

    private void Awake()
    {
        board = transform.parent.GetComponent<Board>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        MakeVisible(false);
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood() //todo still bad ;-;
    {
        PlaceFood(new Vector2Int(Random.Range(0, board.BoardSize.x - 1), Random.Range(0, board.BoardSize.y - 1)));
        yield return new WaitForSeconds (Random.Range(timeFoodSpawn.x, timeFoodSpawn.y));
        
        MakeVisible(false);
        yield return new WaitForSeconds (Random.Range(timeFoodDeSpawn.x, timeFoodDeSpawn.y));
        StartCoroutine(SpawnFood());
    }

    private void PlaceFood(Vector2Int pos)
    {
        MakeVisible(true);
        transform.position = board.GridToWorld(pos);
    }

    public void MakeVisible(bool state)
    {
        IsVisible = state;
        spriteRenderer.color = state? Color.white : Color.clear;
    }
}
