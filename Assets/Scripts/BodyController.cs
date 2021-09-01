using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    private List<GameObject> bodyParts = new List<GameObject>();

    [SerializeField] private Sprite body;
    [SerializeField] private Sprite tail;

    private Movement movement = null;
    
    void Start()
    {
        movement = GetComponent<Movement>();
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
            if (i == bodyParts.Count - 1)
            {
                bodyParts[i].GetComponent<SpriteRenderer>().sprite = tail;
            }
            else
            {
                bodyParts[i].GetComponent<SpriteRenderer>().sprite = body;
            }
            Vector3 temp = bodyParts[i - 1].transform.position - bodyParts[i].transform.position;
            temp = temp.normalized;
            movement.FlipSprite(bodyParts[i].transform, new Vector2Int((int) temp.x, (int) temp.y));
            
            bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
        }
    }
    
    
}
