using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StaticMath {
    private static readonly int[] prime1k = { 0, 1, 2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79,83,89,97,101,
103,107,109,113,127,131,137,139,149,151,157,163,167,173,179,181,191,193,197,
199,211,223,227,229,233,239,241,251,257,263,269,271,277,281,283,293,307,311,
313,317,331,337,347,349,353,359,367,373,379,383,389,397,401,409,419,421,431,
433,439,443,449,457,461,463,467,479,487,491,499,503,509,521,523,541,547,557,
563,569,571,577,587,593,599,601,607,613,617,619,631,641,643,647,653,659,661,
673,677,683,691,701,709,719,727,733,739,743,751,757,761,769,773,787,797,809,
811,821,823,827,829,839,853,857,859,863,877,881,883,887,907,911,919,929,937,
941,947,953,967,971,977,983,991,997 };
    public static bool IsPrime(int i) {
        if (i < 1000) {
            return prime1k.Contains(i);
        } else {
            return false;
        }
    }

    public static int HighestDivisor(int n, int cap) {
        int divisor = cap;
        while (true) {
            if (n % divisor == 0) {
                return divisor;
            } else {
                divisor--;
            }
        }
    }

    public static (int, int) FindResultAndDifference(int n, int div) {
        int res = Mathf.CeilToInt((float) n / div);
        int t = res * div;
        int diff = n - t;
        return (res, diff);
    }

    public static (int, float) FindResultAndDifference(float n, float div) {
        int res = Mathf.CeilToInt((float)n / div);
        float t = res * div;
        float diff = n - t;
        return (res, diff);
    }

    public static Vector2Int ApproachZero(Vector2Int v) {
        Vector2Int newV = Vector2Int.zero;
        if (v.x < 0) {
            newV.x = v.x+1;
        } else if (v.x > 0) {
            newV.x = v.x-1;
        }

        if (v.y < 0) {
            newV.y = v.y+1;
        } else if (v.y > 0) {
            newV.y = v.y-1;
        }
        return newV;
    }

    public static Vector3Int ApproachZero(Vector3Int v, bool zChange = false) {
        Vector3Int newV = Vector3Int.zero;
        if (v.x < 0) {
            newV.x = v.x + 1;
        } else if (v.x > 0) {
            newV.x = v.x - 1;
        }

        if (v.y < 0) {
            newV.y = v.y + 1;
        } else if (v.y > 0) {
            newV.y = v.y - 1;
        }
        if (zChange) {
            if (v.z < 0) {
                newV.z = v.z + 1;
            } else if (v.z > 0) {
                newV.z = v.z - 1;
            }
        }
        return newV;
    }

    public static (int, int) ApproachZero(int a, int b) {
        return (a--, b--);
    }

    public static Vector3Int Abs(Vector3Int v) {
        return new Vector3Int(Mathf.Abs(v.x), Mathf.Abs(v.y) , Mathf.Abs(v.z));
    }

    public static Vector2 Abs(Vector2 v) {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }

    public static float RoundToMult(float n, float d) {
        return Mathf.Round(n / d) * d;
    }

    public static Vector2 RoundToMult(Vector2 v, float d) {
        float x = Mathf.Round(v.x / d) * d;
        float y = Mathf.Round(v.y / d) * d;
        Vector2 r = new(x, y);
        Debug.Log(r);
        return r;
    }

    public static Vector2 RoundToMult(Vector3 v, float d) {
        return RoundToMult((Vector2)v, d);
    }

    public static int FloatMinutesToInt(float minutes) {
        int diff = Mathf.FloorToInt(minutes);
        float dec = minutes - diff;
        float converted = dec * 0.6f;
        return Mathf.RoundToInt(converted);
    }
}
