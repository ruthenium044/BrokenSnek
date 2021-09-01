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
            bodyParts[i].transform.position = bodyParts[i - 1].transform.position;

            /*if (bodyParts[i].TryGetComponent(out Body body1))
            {
                if (bodyParts[i - 1].TryGetComponent(out Body body2))
                {
                    body1.Direction = body2.Direction;
                }
            }*/
            //bodyParts[i].GetComponent<Body>().Direction = bodyParts[i - 1].GetComponent<Body>().Direction;
            //movement.FlipSprite(bodyParts[i].transform, bodyParts[i].GetComponent<Body>().Direction);
           
        }
    }
    
    
}
