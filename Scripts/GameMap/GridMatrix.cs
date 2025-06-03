using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GridMatrix<T> {

    [SerializeField]
    protected T[][] pxpy, pxny, nxpy, nxny;
    [SerializeField]
    protected int quadNX, quadNY;

    public GridMatrix(int horizontalQuadsN, int verticalQuadsN, bool negatives = true) {
        this.quadNX = horizontalQuadsN;
        this.quadNY = verticalQuadsN;
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

    public T this[Vector2Int q] {
        get => this[q.x, q.y];
        set => this[q.x, q.y] = value;
    }
    protected T[][] GetQuad(int cellX, int cellY) {
        if (cellX >= 0 && cellY >= 0) {
            return pxpy;
        } else if (cellX >= 0 && cellY < 0) {
            return pxny;
        } else if (cellX < 0 && cellY >= 0) {
            return nxpy;
        } else {
            return nxny;
        }
    }

    public T this[int cellX, int cellY] {
        get {
            if (cellX >= 0 && cellY >= 0) {
                return pxpy[Mathf.CeilToInt(cellX/quadNX) -1][Mathf.CeilToInt(cellY/quadNY) -1];
            } else if (cellX >= 0 && cellY < 0) {
                return pxny[Mathf.CeilToInt(cellX / quadNX) - 1][Mathf.CeilToInt(cellY / quadNY * -1) - 1];
            } else if (cellX < 0 && cellY >= 0) {
                return nxpy[Mathf.CeilToInt(cellX / quadNX * -1) - 1][Mathf.CeilToInt(cellY / quadNY) - 1];
            } else {
                return nxny[Mathf.CeilToInt(cellX / quadNX * -1) - 1][Mathf.CeilToInt(cellY / quadNY * -1) - 1];
            }
        } 
        set {
            if (cellX >= 0 && cellY >= 0) {
                pxpy[Mathf.CeilToInt(cellX / quadNX) - 1][Mathf.CeilToInt(cellY / quadNY) - 1] = value;
            } else if (cellX >= 0 && cellY < 0) {
                pxny[Mathf.CeilToInt(cellX / quadNX) - 1][Mathf.CeilToInt(cellY / quadNY * -1) - 1] = value;
            } else if (cellX < 0 && cellY >= 0) {
                nxpy[Mathf.CeilToInt(cellX / quadNX * -1) - 1][Mathf.CeilToInt(cellY / quadNY) - 1] = value;
            } else {
                nxny[Mathf.CeilToInt(cellX / quadNX * -1) - 1][Mathf.CeilToInt(cellY / quadNY * -1) - 1] = value;
            }
        }
    }


    

}
