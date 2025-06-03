using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ShuffleFunctions {
    public static List<T> ShuffleList<T>(List<T> list) {
        List<T> copyList = list.ToList();
        for (int i = 0; i < list.Count; i++) {
            int n = Random.Range(i, copyList.Count);
            (copyList[i], copyList[n]) = (copyList[n], copyList[i]);
        }
        return copyList;
    }
}
