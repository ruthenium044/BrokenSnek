using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Random = System.Random;

public class BloodBoard : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    private static List<GameObject> bloodBoard = new List<GameObject>();
    private static Random pseudoRandom;
    public static Random PseudoRandom => pseudoRandom;

    private void Awake()
    {
        SetRandom();
    }

    public void AddBlood(Vector3 pos)
    { 
        GameObject temp = Instantiate(blood, pos, Quaternion.identity);
        DontDestroyOnLoad(temp);
        bloodBoard.Add(temp);
    }
    
    private void SetRandom()
    {
        string seed;
        seed = System.DateTime.Now.ToString(CultureInfo.InvariantCulture);
        pseudoRandom = new Random(seed.GetHashCode());
    }

    public void Cleanup()
    {
        foreach (var obj in bloodBoard)
        {
            Destroy(obj);
        }
        bloodBoard.Clear();
    }
    
    private void OnApplicationQuit()
    {
        Cleanup();
    }
}

