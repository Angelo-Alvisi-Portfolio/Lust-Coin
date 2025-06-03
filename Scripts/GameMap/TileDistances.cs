using UnityEngine;

[CreateAssetMenu(fileName = "TileDistances", menuName = "Scriptable Objects/TileDistances")]
public class TileDistances : ScriptableObject {



    //Start x, Base Delta x, Full Deltax, Vertical Skew Line Height, Start e (bot left corner), Delta e, size from the center in increments
    [SerializeField]
    float sx = -21.1875f, bdx = 1.8125f, fdx = 2f, skewHeight = 0.25f, sy = -5.625f, dy = 1f, boxSizeFromCenter = 16f;
    [SerializeField]
    private Vector2 lbib, rtib;
    /// <summary>
    /// x Center of the leftmost cell
    /// </summary>
    public float SX => sx;
    /// <summary>
    /// x Distance between two cells
    /// </summary>
    public float BDX => bdx;
    /// <summary>
    /// y size of a cell from leftmost to rightmost point
    /// </summary>
    public float FDX => fdx;
    /// <summary>
    /// Height of a segment of the skew line
    /// </summary>
    public float SkewHeight => skewHeight;
    /// <summary>
    /// y Center of the leftmost cell
    /// </summary>
    public float SY => sy;
    /// <summary>
    /// Height of a cell
    /// </summary>
    public float DY => dy;
    /// <summary>
    /// Leftbottommost inside bound
    /// </summary>
    public Vector2 LBIB => lbib;
    /// <summary>
    /// Righttopmost inside bound
    /// </summary>
    public Vector2 RTIB => rtib;

    /// <summary>
    /// Leftmost point of the grid
    /// </summary>
    public float SXLC => sx - (boxSizeFromCenter * 0.0625f);
    /// <summary>
    /// Number of segments in a skew
    /// </summary>
    public float SkewPercentOfHeight => skewHeight / dy;

    public float height() {
        return rtib.y - lbib.y;
    }
}
