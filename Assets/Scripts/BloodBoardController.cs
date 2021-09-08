using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class BloodBoardController : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    private BoardController boardController;
    static private List<GameObject> bloodBoard = new List<GameObject>();
    
    private static System.Random pseudoRandom;
    public static Random PseudoRandom => pseudoRandom;

    private void Awake()
    {
        boardController = GetComponent<BoardController>();
        SetRandom();
    }

    public void AddBlood(Vector3 pos)
    {
        Vector2Int currentKey = boardController.WorldToGrid(pos);
        
         GameObject temp = Instantiate(blood, pos, quaternion.identity);
         temp.transform.position = pos;
         DontDestroyOnLoad(temp);
         bloodBoard.Add(temp);
    }

    public void Cleanup()
    {
        foreach (var obj in bloodBoard)
        {
            Destroy(obj);
        }
    }
    
    private void OnApplicationQuit()
    {
        Cleanup();
    }
    
    private void SetRandom()
    {
        string seed;
        seed = System.DateTime.Now.ToString();
        pseudoRandom = new System.Random(seed.GetHashCode());
    }
}

