using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private Text pointsText;
    private int points;
    
    public int Points
    {
        get => points;
        set => points = value;
    }
    
    void Update()
    {
        pointsText.text = "Points: " + Points;
    }
}
