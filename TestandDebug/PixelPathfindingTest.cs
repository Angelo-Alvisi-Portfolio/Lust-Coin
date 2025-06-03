using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PixelPathfindingTest : MonoBehaviour {
    [SerializeField]
    private PathfindingMap map;
    [SerializeField]
    private Vector2 startPixel, endPixel;
    [SerializeField]
    private AStarPathfinding pathfinding;
    [SerializeField]
    private GameObject singlePixel;

    private List<Vector2> path = new();
    private List<GameObject> pixels = new();

    [Button]
    public void Test() {
        path = pathfinding.StartSearch(startPixel, endPixel, null, 0.0625f);
        for (int i = 0; i < path.Count; i++) {
            GameObject pix = Instantiate(singlePixel);
            pixels.Add(pix);
            pix.transform.position = path[i];
        }
        GameObject sPix = Instantiate(singlePixel);
        pixels.Add(sPix);
        sPix.transform.position = startPixel;
        sPix.GetComponent<SpriteRenderer>().color = Color.red;
    }

    [Button]
    public void Reset() {
        foreach (GameObject pix in pixels) {
            Destroy(pix);
        }
    }
}
