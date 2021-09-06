using System.Collections.Generic;
using UnityEngine;

public class BloodBoardController : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    private Dictionary<Vector2Int, GameObject> bloodBoard = new Dictionary<Vector2Int, GameObject>();

    [SerializeField] private BoardController boardController;

    public void AddBlood()
    {
        Vector2Int currentKey = boardController.WorldToGrid(transform.position);
        if (!bloodBoard.ContainsKey(currentKey))
        {
            GameObject temp = Instantiate(blood, transform.position, Quaternion.identity);
            bloodBoard.Add(currentKey, temp);
            
            DontDestroyOnLoad(temp);
        }
    }
}
