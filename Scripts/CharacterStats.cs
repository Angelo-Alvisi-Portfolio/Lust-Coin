using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject {
    [SerializeField]
    private int maxHP = 10;
    public int MaxHP { get { return maxHP; } }
    [SerializeField]
    private int dexterity = 1;
    public int Dexterity { get { return dexterity; } }
    [SerializeField]
    private int attack = 1;
    public int Attack { get { return attack; } }
    [SerializeField]
    private int defense = 1;
    public int Defense { get { return defense; } }
    [SerializeField]
    private int critChance = 15;
    public int CritChance { get { return critChance; } }
    [SerializeField]
    private int critMult = 150;
    public int CritMult { get { return critMult; } }
    [SerializeField]
    private int luck = 0;
    public int Luck { get { return luck; } }
    [SerializeField]
    private int hpRegen = 0;
    public int HPRegen { get { return hpRegen; } }
}
