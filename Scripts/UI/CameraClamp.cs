using UnityEngine;

[CreateAssetMenu(fileName = "CameraClamp", menuName = "Scriptable Objects/CameraClamp")]
public class CameraClamp : ScriptableObject {
    [SerializeField]
    private float minX, maxX, minY, maxY;    
    public float MinX => minX;
    public float MinY => minY;
    public float MaxX => maxX;
    public float MaxY => maxY;

}
