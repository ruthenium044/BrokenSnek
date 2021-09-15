using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Sprite body;
    [SerializeField] private Sprite tail;
    
    private List<GameObject> bodyParts = new List<GameObject>();
    public List<GameObject> BodyParts => bodyParts;
    
    void Start()
    {
        bodyParts.Add(gameObject);
    }
    
    public void AddBodyParts()
    {
        GameObject temp = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].transform.position, Quaternion.identity);
        temp.transform.rotation = bodyParts[bodyParts.Count - 1].transform.rotation;
        bodyParts.Add(temp);
    }

    public void MoveBodyParts()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            if (i == bodyParts.Count - 1)
            {
                bodyParts[i].GetComponent<SpriteRenderer>().sprite = tail;
            }
            else
            {
                bodyParts[i].GetComponent<SpriteRenderer>().sprite = body;
            }
            bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
        }
    }

    public void RotateBodyParts(Movement movement)
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            Vector3 temp = bodyParts[i - 1].transform.position - bodyParts[i].transform.position;
            temp = temp.normalized;
            movement.RotateSprite(bodyParts[i].transform, new Vector2Int((int) temp.x, (int) temp.y));
        }
    }
}
