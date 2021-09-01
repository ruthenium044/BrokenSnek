using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    private List<GameObject> bodyParts = new List<GameObject>();

    void Start()
    {
        bodyParts.Add(gameObject);
        
    }

    public void AddBody()
    {
        GameObject temp = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
        bodyParts.Add(temp);
    }
    
    
}
