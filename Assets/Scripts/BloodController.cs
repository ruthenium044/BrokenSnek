using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector2Int spriteSize = new Vector2Int(16, 16);

    private int timesToSmooth = 5;
    private int randomFillPercent = 49;

    private List<Color32> tempList = new List<Color32>();
    private List<Color32> reds = new List<Color32>
    {
        new Color32(0x52, 0x0b, 0x20, 0xFF),
        new Color32(0x7d, 0x0f, 0x1f, 0xFF), 
        new Color32(0xa8, 0x14, 0x1d, 0xFF)
    };
    private Color32 red;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        red = reds[BloodBoardController.PseudoRandom.Next(0, reds.Count)];
        do
        {
            Generate();
        } while (IsEmpty());
        
        spriteRenderer.sprite = CreateSprite();
    }

    private Sprite CreateSprite()
    {
        Texture2D texture = new Texture2D(spriteSize.x, spriteSize.y);
        texture.filterMode = FilterMode.Point;

        texture.SetPixels32(GenerateBloodArray());
        texture.Compress(false);
        texture.Apply();

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, spriteSize.x, spriteSize.y), Vector2.one * 0.5f, 16);
        return sprite;
    }

    private Color32[] GenerateBloodArray()
    {
        List<Color32> colors = new List<Color32>();
        colors = tempList;
        return colors.ToArray();
    }

    private void Generate()
    {
        RandomFill();

        for (int i = 0; i < timesToSmooth; i++)
        {
            Smooth();
        }
    }

    private bool IsEmpty()
    {
        foreach (var obj in tempList)
        {
            if (obj != Color.clear)
            {
                return false;
            }
        }
        tempList.Clear();
        return true;
    }
    
    private void RandomFill()
    {
        for (int x = 0; x < spriteSize.x; x++)
        {
            for (int y = 0; y < spriteSize.y; y++)
            {
                tempList.Add((BloodBoardController.PseudoRandom.Next(0, 100) < randomFillPercent) ? red : (Color32) Color.clear);
            }
        }
    }

    private void Smooth()
    {
        for (int x = 0; x < spriteSize.x; x++)
        {
            for (int y = 0; y < spriteSize.y; y++)
            {
                int neighbourWallTiles = GetNeighbourCount(x, y);

                if (neighbourWallTiles > 4)
                    tempList[(y * spriteSize.y) + x] = red;
                else if (neighbourWallTiles < 4)
                    tempList[(y * spriteSize.y) + x] = Color.clear;
            }
        }
    }

    private int GetNeighbourCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < spriteSize.x && 
                    neighbourY >= 0 && neighbourY < spriteSize.y)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        if (tempList[(neighbourY * spriteSize.y) + neighbourX].Equals(red))
                        {
                            wallCount += 1;
                        }
                        
                    }
                }
            }
        }
        return wallCount;
    }
}



