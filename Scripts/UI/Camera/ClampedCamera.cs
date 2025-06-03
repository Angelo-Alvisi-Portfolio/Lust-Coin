using UnityEngine;

public class ClampedCamera : MonoBehaviour {
    [SerializeField]
    private float minX, maxX, minY, maxY;
    
    public void MoveCameraTo(Vector3 pos, bool sexPos = false) {
        float newX = pos.x;
        float newY = Mathf.Clamp(pos.y, minY, maxY);
        if (sexPos) {
            newX-=2f;
        } else {
            newX = Mathf.Clamp(newX, minX, maxX);
        }
        transform.position = new Vector3(newX, newY, -10);                 
    }

    public void ChangeClamps(CameraClamp clamp) {
        minX = clamp.MinX;
        maxX = clamp.MaxX;
        minY = clamp.MinY;
        maxY = clamp.MaxY;
    }
}
