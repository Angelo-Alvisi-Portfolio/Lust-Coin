using Sirenix.OdinInspector;
using UnityEngine;

public class RenameAndReorder : MonoBehaviour {
    public GameObject[] objectsToRename;
    [ShowInInspector]
    public string prefix;
    [ShowInInspector]
    private bool numericSuffix = true;
    [ShowInInspector, HideIf("numericSuffix")]
    private string suffix;


    [Button("Rename and Reorder")]
    public void RenameAndReorderObjects() {
        if (objectsToRename == null || objectsToRename.Length == 0) return;

        // Sort objects by x and then y coordinates
        System.Array.Sort(objectsToRename, (a, b) => {
            Vector3 posA = a.transform.position;
            Vector3 posB = b.transform.position;

            // Compare x-coordinates first
            int xComparison = posA.x.CompareTo(posB.x);
            if (xComparison != 0) return xComparison;

            // If x-coordinates are equal, compare y-coordinates
            return posA.y.CompareTo(posB.y);
        });

        // Rename objects based on the sorted order
        for (int i = 0; i < objectsToRename.Length; i++) {
            GameObject obj = objectsToRename[i];
            string newName = prefix;
            if (numericSuffix) {                
                newName += $" {i}";                
            } else {
                newName += $" {suffix}";
            }
            obj.name = newName;
        }
    }
}
