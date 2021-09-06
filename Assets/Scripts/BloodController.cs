using UnityEngine;

public class BloodController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Texture2D texture = new Texture2D(16, 16);
        texture.filterMode = FilterMode.Point;

        for (int x = 0; x < 16; x++) // todo set pixel data like raw arrray
        {
            for (int y = 0; y < 16; y++)
            {
                if (x == 3 || y == 5)
                {
                    texture.SetPixel(x, y, Color.clear);
                }
                else
                {
                    texture.SetPixel(x, y, Color.red);
                }
                //todo texture.set pixels takes whole array of colors for the pixels
            }
        }
        
        texture.Compress(false);
        
        texture.Apply();
        
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 16, 16), Vector2.one * 0.5f, 16);
        spriteRenderer.sprite = sprite;
    }

    private void GenerateBloodArray()
    {
        //todo make it happen
    }
}
