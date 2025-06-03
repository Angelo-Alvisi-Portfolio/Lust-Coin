using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text slot1Keyb, slot2Keyb, slot3Keyb, slot4Keyb;
    public TMP_Text Slot1Keyb => slot1Keyb;
    public TMP_Text Slot2Keyb => slot2Keyb;
    public TMP_Text Slot3Keyb => slot3Keyb;
    public TMP_Text Slot4Keyb => slot4Keyb;
    [SerializeField]
    private Image slot1Bar, slot2Bar, slot3Bar, slot4Bar;
    public Image Slot1Bar => slot1Bar;
    public Image Slot2Bar => slot2Bar;
    public Image Slot3Bar => slot3Bar;
    public Image Slot4Bar => slot4Bar;
    [SerializeField]
    private Character[] enemyPrefabs;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private CombatBackground combatBackground;
    public CombatBackground CombatBackground => combatBackground;

    private Character currentEnemy;
    public Character CurrentEnemy => currentEnemy;

    private void Awake() {
        StaticManager.CombatSceneController = this;        
    }

    private void OnEnable() {
        SpawnEnemy();
    }

    public void SpawnEnemy() {
        currentEnemy = Instantiate(enemyPrefabs[0], spawnPoint.transform);
    }


}
