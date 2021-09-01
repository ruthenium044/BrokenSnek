using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private Vector2Int direction = Vector2Int.up;

    public Vector2Int Direction
    {
        get => direction;
        set => direction = value;
    }
}
