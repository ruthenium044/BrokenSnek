using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    private List<GameObject> bodyParts = new List<GameObject>();

    public List<GameObject> BodyParts => bodyParts;

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

    public void RotateBodyParts()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            FlipSprite(bodyParts[i].transform, bodyParts[i - 1].transform);
        }
    }
    
    private void FlipSprite(Transform currentObj, Transform nextObj)
    {
        Vector3 temp = nextObj.position - currentObj.position;
        temp = temp.normalized;
        movement.FlipSprite(currentObj, new Vector2Int((int) temp.x, (int) temp.y));
    }
}
