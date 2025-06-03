using UnityEngine;

public class FloatGridMatrix<T> {
    [SerializeField]
    protected T[][] pxpy, pxny, nxpy, nxny;
    [SerializeField]
    protected int quadNX, quadNY;
    [SerializeField]
    protected float quadWidth, quadHeight;

    public FloatGridMatrix(int horizontalQuadsN, int verticalQuadsN, float quadWidth, float quadHeight, bool negatives = true) {
        this.quadNX = horizontalQuadsN;
        this.quadNY = verticalQuadsN;
        this.quadWidth = quadWidth;
        this.quadHeight = quadHeight;
        pxpy = new T[horizontalQuadsN][];
        if (negatives) {
            pxny = new T[horizontalQuadsN][];
            nxpy = new T[horizontalQuadsN][];
            nxny = new T[horizontalQuadsN][];
            for (int x = 0; x < horizontalQuadsN; x++) {
                pxpy[x] = new T[verticalQuadsN];
                pxny[x] = new T[verticalQuadsN];
                nxpy[x] = new T[verticalQuadsN];
                nxny[x] = new T[verticalQuadsN];
            }
        } else {
            for (int x = 0; x < horizontalQuadsN; x++) {
                pxpy[x] = new T[verticalQuadsN];
            }
        }



    }


    protected T[][] GetQuadAtPosition(Vector2 pos) {
        if (pos.x >= 0 && pos.y >= 0) {
            return pxpy;
        } else if (pos.x >= 0 && pos.y < 0) {
            return pxny;
        } else if (pos.x < 0 && pos.y >= 0) {
            return nxpy;
        } else {
            return nxny;
        }
    }

    public T this[Vector2 pos] {
        get => GetQuadAtPosition(pos)[Mathf.Abs(Mathf.FloorToInt(pos.x / quadWidth))][Mathf.Abs(Mathf.FloorToInt(pos.y / quadHeight))];                    
    }
}
