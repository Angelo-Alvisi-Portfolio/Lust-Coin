using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VectorSortedSet : SortedSet<Vector3Int> {

    public VectorSortedSet() : this(Comparer<Vector3Int>.Create((a, b) => { 
                                if (a.x != b.x) {
                                    return a.x.CompareTo(b.x);
                                }
                                    return a.y.CompareTo(b.y);
                                })) 
    { }

    public VectorSortedSet(bool sortByY) : this(Comparer<Vector3Int>.Create((a, b) => {
        if (a.y != b.y) {
            return a.y.CompareTo(b.y);
        }
        return a.x.CompareTo(b.x);
    })) { }


    private VectorSortedSet(IComparer<Vector3Int> comparer) : base(comparer) {

    }
}
