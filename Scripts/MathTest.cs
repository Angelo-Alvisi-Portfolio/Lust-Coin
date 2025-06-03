using Sirenix.OdinInspector;
using UnityEngine;

public class MathTest : MonoBehaviour {

    [SerializeField]
    private int hSegs = 1, vSegs = 1, maxWidth = 25, maxHeight = 25, x, y;

    public int MinX => Mathf.CeilToInt(((float)hSegs - 1) / 2) * maxWidth * -1;
    public int MaxX => Mathf.FloorToInt(((float)hSegs - 1) / 2) * maxWidth - ((maxWidth % 2 == 0) ? 1 : 0) + maxWidth;
    public int MinY => Mathf.CeilToInt(((float)vSegs - 1) / 2) * maxHeight * -1;
    public int MaxY => Mathf.FloorToInt(((float)vSegs - 1) / 2) * maxHeight - ((maxHeight % 2 == 0) ? 1 : 0) + maxHeight;

    [Button]
    public void TestMinMax() {
        Debug.Log(MinX + " " + MaxX + " " + MinY + " " + MaxY);
    }
    [Button]
    public void TestFindMapKey() {
        float fH = (float)x / maxWidth;
        float fV = (float)y / maxHeight;
        int hS = (x >= 0) ? Mathf.FloorToInt(fH) : Mathf.CeilToInt(fH);
        int vS = (y >= 0) ? Mathf.FloorToInt(fV) : Mathf.CeilToInt(fV);
        Debug.Log(hS + " " + vS);
    }
}
