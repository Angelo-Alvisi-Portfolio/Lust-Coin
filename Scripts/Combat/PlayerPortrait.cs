using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortrait : MonoBehaviour {
    [SerializeField]
    private Image portrait;
    [SerializeField]
    private Sprite[] portraits;
    [SerializeField]
    private Image hpBar;

    // Update is called once per frame
    void Update()     {
        float hpRatio = (float) StaticManager.Axel.Character.CurrentHp / StaticManager.Axel.Character.CurrentMaxHp;
        if (hpRatio < 0.25f) {
            portrait.sprite = portraits[3];
        } else if (hpRatio < 0.5f) {
            portrait.sprite = portraits[2];
        } else if (hpRatio < 0.75f) {
            portrait.sprite = portraits[1];
        } else {
            portrait.sprite = portraits[0];
        }
        hpBar.fillAmount = hpRatio;

    }
}
