using System;
using System.Collections;
using UnityEngine;

public class Easing : MonoBehaviour
{
    [SerializeField] private Vector3 end;
    private Vector3 start;

    void Start()
    {
        start = transform.position;
        StartCoroutine(EaseInSin(start, end));
    }

    float EaseIn(float a, float b, float t)
    {
        t = Mathf.Sin(t * Mathf.PI / 2);
        return a * (1 - t) + b * t;
    }

    Vector2 EaseIn(Vector2 a, Vector2 b, float t)
    {
        return new Vector2(EaseIn(a.x, b.x, t), EaseIn(a.y, b.y, t));
    }

    private IEnumerator EaseInSin(Vector3 startPos, Vector3 endPos)
    {
        float t = 0;
        while (t < 1.0f)
        {
            transform.position = EaseIn(startPos, endPos, t);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = endPos;
    }
}
