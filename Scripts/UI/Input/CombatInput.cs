using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CombatInput : MonoBehaviour {

    
    [SerializeField]
    private Material activeFont, inactiveFont;

    private int slot1Cd = 0;
    public int Slot1Cd => slot1Cd;
    private int slot2Cd = -1;
    public int Slot2Cd => slot2Cd;
    private int slot3Cd = -1;
    public int Slot3Cd => slot3Cd;
    private int slot4Cd = -1;
    public int Slot4Cd => slot4Cd;

    public void OnPotion1() {
        if (slot1Cd == 0) {
            StaticManager.Axel.Character.ModifyHp(5);
            slot1Cd = 10;
            StaticManager.CombatSceneController.Slot1Keyb.fontMaterial = inactiveFont;
            StaticManager.CombatSceneController.Slot1Bar.fillAmount = 0;
            StartCoroutine(Slot1Cooldown());
        }
    }
    public void OnPotion2() {
        if (slot1Cd == 0) {
            StaticManager.Axel.Character.ModifyHp(5);
            slot1Cd = 10;
            StaticManager.CombatSceneController.Slot1Keyb.fontMaterial = inactiveFont;
            StaticManager.CombatSceneController.Slot1Bar.fillAmount = 0;
            StartCoroutine(Slot2Cooldown());
        }
    }
    public void OnPotion3() {
        if (slot1Cd == 0) {
            StaticManager.Axel.Character.ModifyHp(5);
            slot1Cd = 10;
            StaticManager.CombatSceneController.Slot1Keyb.fontMaterial = inactiveFont;
            StaticManager.CombatSceneController.Slot1Bar.fillAmount = 0;
            StartCoroutine(Slot3Cooldown());
        }
    }
    public void OnPotion4() {
        if (slot1Cd == 0) {
            StaticManager.Axel.Character.ModifyHp(5);
            slot1Cd = 10;
            StaticManager.CombatSceneController.Slot1Keyb.fontMaterial = inactiveFont;
            StaticManager.CombatSceneController.Slot1Bar.fillAmount = 0;
            StartCoroutine(Slot4Cooldown());
        }
    }

    private IEnumerator Slot1Cooldown() {
        while (slot1Cd > 0) {
            slot1Cd--;
            float f = 10f - slot1Cd;
            StaticManager.CombatSceneController.Slot1Bar.fillAmount = f * 0.1f;

            yield return new WaitForSeconds(1);
        }
        StaticManager.CombatSceneController.Slot1Keyb.fontMaterial = activeFont;
    }
    private IEnumerator Slot2Cooldown() {
        while (slot2Cd > 0) {
            yield return new WaitForSeconds(1);
            slot2Cd--;
        }
    }
    private IEnumerator Slot3Cooldown() {
        while (slot3Cd > 0) {
            yield return new WaitForSeconds(1);
            slot3Cd--;
        }
    }
    private IEnumerator Slot4Cooldown() {
        while (slot4Cd > 0) {
            yield return new WaitForSeconds(1);
            slot4Cd--;
        }
    }
#if UNITY_EDITOR
    public void OnLowerHP() {
        StaticManager.Axel.Character.ModifyHp(-10);
    }
#endif
}
