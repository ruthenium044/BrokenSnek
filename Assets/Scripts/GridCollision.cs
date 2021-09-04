using UnityEngine;

public class GridCollision : MonoBehaviour
{
    [SerializeField] private FoodController food;
    private BoardController boardController;
    private BodyController bodyController;
    private UserInterface userInterface;
    private Death death;
    private AudioController audio;

    private void Awake()
    {
        boardController = food.transform.parent.GetComponent<BoardController>();
        bodyController = GetComponent<BodyController>();
        userInterface = GetComponent<UserInterface>();
        death = GetComponent<Death>();
        audio = GetComponent<AudioController>();
    }

    private void Update()
    {
        if (CheckPlayerPosition(boardController.WorldToGrid(food.transform.position)) && food.IsVisible)
        {
            audio.Play(Random.Range(1, 4));
            userInterface.Score += 1;
            food.MakeVisible(false);
            bodyController.AddBodyParts();
        }

        for (int i = 3; i < bodyController.BodyParts.Count; i++)
        {
            if (CheckPlayerPosition(boardController.WorldToGrid(bodyController.BodyParts[i].transform.position)))
            {
                death.ExecuteSnake();
            }
        }
    }
    
    private bool CheckPlayerPosition(Vector2Int pos)
    {
        if (boardController.WorldToGrid(transform.position) == pos)
        {
            return true;
        }
        return false;
    }
}
