using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BloodBoardController : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    [SerializeField] private GameObject blood;
    
    static private Dictionary<Vector2Int, GameObject> bloodBoard = new Dictionary<Vector2Int, GameObject>();
    
    public void AddBlood(Vector3 pos)
    {
        Vector2Int currentKey = boardController.WorldToGrid(pos);
        if (!bloodBoard.ContainsKey(currentKey))
        {
            GameObject temp = Instantiate(blood, pos, quaternion.identity);
            temp.transform.position = pos;
            DontDestroyOnLoad(temp);
            bloodBoard.Add(currentKey, temp);
        }
    }
    
    private void OnApplicationQuit()
    {
        foreach (var obj in bloodBoard)
        {
            Destroy(obj.Value);
        }
    }
}
