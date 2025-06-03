using UnityEngine;

public class BlockSelector : MonoBehaviour {
    private int x = 9;
    private int y = 2;
    public int Y { get { return y; } }
    public int X { get { return x; } }
    [SerializeField]
    private SpriteRenderer foregroundRenderer;
    [SerializeField]
    private Sprite[] foregroundSprites;
    private int spriteN = 0;


    public void Move(Vector2Int dir, Vector2Int bounds, TileDistances tileDistances) {
        Vector2Int delta = dir;
        Vector2 newPos = transform.position;
        if (dir.x == 1) {
            if (x < bounds.x) {
                newPos.x += tileDistances.BDX;
                x++;
            }
        } else if (dir.x == -1) {
            if (x > 0) {
                newPos.x -= tileDistances.BDX;
                x--;
            }
        }
        if (dir.y == 1)  {
            if (y < bounds.y) {
                newPos.y += tileDistances.DY;
                newPos.x += tileDistances.SkewHeight;
                y++;
            }
        } else if (dir.y == -1) {
            if (y > 0) {
                newPos.y -= tileDistances.DY;
                newPos.x -= tileDistances.SkewHeight;
                y--;
            }
        }
        transform.position = newPos;
    }    

    public void SwitchSprite(int n) {
        if (n > 0) {
            if (spriteN == foregroundSprites.Length-1) {
                return;
            }
        }
        if (n < 0) {
            if (spriteN == 0) {
                return;
            }
        }
        spriteN += n;
        foregroundRenderer.sprite = foregroundSprites[spriteN];
        
    }
}
