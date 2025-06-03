using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class BoundingBoxesContainer : MonoBehaviour {
    [SerializeField]
    private BoundingBox prefab;
    [SerializeField]
    private TileDistances tileDistances;
    [Button]
    private void GenerateBoxes() {
        for (int x = 0; x < 23; x++) {
            for (int y = 0; y < 5; y++) {
                float posx = tileDistances.SX + tileDistances.BDX * x + tileDistances.SkewHeight * y;
                float posy = tileDistances.SY + tileDistances.DY * y;
                BoundingBox b = Instantiate(prefab, transform);
                b.transform.position = new Vector3(posx, posy, 0f);
                b.name = "Bounding Box " + x.ToString() + "," + y.ToString();
                b.SetPos(x, y);
            }
        }

    }
}
