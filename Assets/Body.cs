using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    private List<GameObject> bodyParts = new List<GameObject>();

    void Start()
    {
        bodyParts.Add(gameObject);
    }

    public void AddBodyParts()
    {
        GameObject temp = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
        bodyParts.Add(temp);
    }

    public void MoveBodyParts()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
        }
    }
}
