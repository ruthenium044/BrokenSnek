using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : MonoBehaviour
{
    [SerializeField] private Death death;
    [SerializeField] private Vector2 timeFoodSpawn = new Vector2(5f, 8f);
    [SerializeField] private Vector2 timeFoodDeSpawn = new Vector2(0.5f, 2f);
    
    private BoardController boardController;
    private SpriteRenderer spriteRenderer;
    private  bool isVisible;

    public bool IsVisible
    {
        get => isVisible;
        private set => isVisible = value;
    }

    private void Awake()
    {
        boardController = transform.parent.GetComponent<BoardController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        MakeVisible(false);
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood() //todo clean up this into two or something
    {
        while (!death.GameOver)
        {
            PlaceFood(new Vector2Int(Random.Range(0, boardController.BoardSize.x - 1), Random.Range(0, boardController.BoardSize.y - 1)));
            yield return new WaitForSeconds (Random.Range(timeFoodSpawn.x, timeFoodSpawn.y));

            if (!death.GameOver)
            {
                MakeVisible(false);
                yield return new WaitForSeconds (Random.Range(timeFoodDeSpawn.x, timeFoodDeSpawn.y));
            }
        }
    }

    private void PlaceFood(Vector2Int pos)
    {
        MakeVisible(true);
        transform.position = boardController.GridToWorld(pos);
    }

    public void MakeVisible(bool state)
    {
        IsVisible = state;
        spriteRenderer.color = state? Color.white : Color.clear;
    }
}
