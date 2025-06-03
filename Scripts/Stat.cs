using System;
using UnityEngine;

[Serializable]
public class Stat {
    public int _Value;
    public Stat(int value) { _Value = value; }
    public static Stat operator +(Stat a, Stat b) => new Stat(a._Value + b._Value);
    public static Stat operator -(Stat a, Stat b) => new Stat(a._Value - b._Value);
    public static Stat operator +(Stat a, int b) => new Stat(a._Value + b);
    public static Stat operator -(Stat a, int b) => new Stat(a._Value - b);
    /// <summary>
    /// Ceils to largest integer.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Stat operator *(Stat a, int b) => new Stat(Mathf.CeilToInt(a._Value * b));
    public static bool operator ==(Stat a, Stat b) => a._Value == b._Value;
    public static bool operator !=(Stat a, Stat b) => a._Value != b._Value;
    public static bool operator >(Stat a, Stat b) => a._Value > b._Value;
    public static bool operator <(Stat a, Stat b) => a._Value < b._Value;
    public static bool operator ==(Stat a, int b) => a._Value == b;
    public static bool operator !=(Stat a, int b) => a._Value != b;
    public static bool operator >(Stat a, int b) => a._Value > b;
    public static bool operator <(Stat a, int b) => a._Value < b;

}
