using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] private int timesToSmooth = 5;
    [SerializeField] private int randomFillPercent = 49;
    
    private SpriteRenderer spriteRenderer;
    private Vector2Int spriteSize = new Vector2Int(16, 16);

    private List<Color32> pixels = new List<Color32>();
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
        red = reds[BloodBoard.PseudoRandom.Next(0, reds.Count)];
        GeneratePixels();
        spriteRenderer.sprite = CreateSprite();
    }

    private Sprite CreateSprite()
    {
        Texture2D texture = new Texture2D(spriteSize.x, spriteSize.y);
        texture.filterMode = FilterMode.Point;
        texture.SetPixels32(pixels.ToArray());;
        texture.Apply();

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, spriteSize.x, spriteSize.y), Vector2.one * 0.5f, 16);
        return sprite;
    }

    private void GeneratePixels()
    {
        do
        {
            RandomFill();
            for (int i = 0; i < timesToSmooth; i++)
            {
                Smooth();
            }
        } while (IsEmpty());
    }

    private bool IsEmpty()
    {
        foreach (var obj in pixels)
        {
            if (obj != Color.clear)
            {
                return false;
            }
        }
        pixels.Clear();
        return true;
    }
    
    private void RandomFill()
    {
        for (int x = 0; x < spriteSize.x; x++)
        {
            for (int y = 0; y < spriteSize.y; y++)
            {
                pixels.Add((BloodBoard.PseudoRandom.Next(0, 100) < randomFillPercent) ? red : (Color32) Color.clear);
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
                {
                    pixels[(y * spriteSize.y) + x] = red;
                }
                else if (neighbourWallTiles < 4)
                {
                    pixels[(y * spriteSize.y) + x] = Color.clear;
                }
            }
        }
    }

    private int GetNeighbourCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int x = gridX - 1; x <= gridX + 1; x++)
        {
            for (int y = gridY - 1; y <= gridY + 1; y++)
            {
                if (x < 0 || x >= spriteSize.x || y < 0 || y >= spriteSize.y)
                {
                    continue;
                }
                if (x != gridX || y != gridY)
                {
                    wallCount += pixels[(y * spriteSize.y) + x].Equals(red) ? 1 : 0;
                }
            }
        }
        return wallCount;
    }
}



