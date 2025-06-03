using System;
using UnityEngine;

[Serializable]
public struct MoneyAccount {    
    private int CurrentMoney { get; set; }
    private int CurrentDebt { get; set; }

    public MoneyAccount(int currentMoney, int currentDebt) {
        CurrentMoney = currentMoney;
        CurrentDebt = currentDebt;
    }
}
