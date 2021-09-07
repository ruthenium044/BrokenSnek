using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector2Int spriteSize = new Vector2Int(16, 16);
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandom();
        GenerateMap();
    }

    private void Start()
    {
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

    private string seed;
    private bool useRandomSeed = true;
    private int timesToSmooth = 4;

    [Range(0, 100)]
    private int randomFillPercent = 50;

    private System.Random pseudoRandom;
    
    private Color32 theRed = new Color32(0xa5, 0x30, 0x30, 0xFF);
    
    private Color32[] GenerateBloodArray()
    {
        List<Color32> colors = new List<Color32>();

        colors = list;

        return colors.ToArray();
    }

    private void SetRandom()
    {
	    if (useRandomSeed)
	    {
		    seed = System.DateTime.Now.ToString();
	    }
	    pseudoRandom = new System.Random(seed.GetHashCode());
    }
    
    private void GenerateMap()
    {
        RandomFillMap();

        for (int i = 0; i < timesToSmooth; i++)
        {
            SmoothMap();
        }
    }

    private List<Color32> list = new List<Color32>();
    
    //ref
    //https://learn.unity.com/project/procedural-cave-generation-tutorial
    void RandomFillMap()
    {
        for (int x = 0; x < spriteSize.x; x++)
        {
            for (int y = 0; y < spriteSize.y; y++)
            {
                list.Add((pseudoRandom.Next(0, 100) < randomFillPercent) ? theRed : (Color32) Color.clear);
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < spriteSize.x; x++)
        {
            for (int y = 0; y < spriteSize.y; y++)
            {
                int neighbourWallTiles = GetNeighbourCount(x, y);

                if (neighbourWallTiles > 4)
                    list[(y * spriteSize.y) + x] = theRed;
                else if (neighbourWallTiles < 4)
                    list[(y * spriteSize.y) + x] = Color.clear;
            }
        }
    }

    int GetNeighbourCount(int gridX, int gridY) //moore
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
                        if (list[(neighbourY * spriteSize.y) + neighbourX].Equals(theRed))
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



