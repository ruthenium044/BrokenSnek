using System;
using System.Collections;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [SerializeField] private Vector3 endPos;
    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.position;
        StartCoroutine(SwipeIn());
    }
    
    private IEnumerator SwipeIn()
    {
        float t = 0;
        while (t < 1.0f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = endPos;
    }
}
