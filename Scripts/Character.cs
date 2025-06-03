using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField]
    protected CharacterStats stats;

    private int maxHpBonus = 0;
    [HideInInspector]
    public int CurrentMaxHp => stats.MaxHP + maxHpBonus; 

    private int dexBonus = 0;
    [HideInInspector]
    public int CurrentDex => stats.Dexterity + dexBonus;
    protected int atkBonus = 0;
    [HideInInspector]
    public int CurrentAtk => stats.Attack + atkBonus;
    protected int defBonus = 0;
    [HideInInspector]
    public int CurrentDefense => stats.Defense + defBonus;
    protected int critChBonus = 0;
    [HideInInspector]
    public int CurrentCritChance => stats.CritChance + critChBonus;
    protected int critMultBonus = 0;
    [HideInInspector]
    public int CurrentCritMult => stats.CritMult + critMultBonus;
    protected int luckBonus = 0;
    [HideInInspector]
    public int CurrentLuck => stats.Luck + luckBonus;
    protected int hpRegenBonus = 0;
    [HideInInspector]
    public int CurrentHpRegen => stats.HPRegen + hpRegenBonus;
    protected int currentHp;
    [HideInInspector]
    public int CurrentHp => currentHp;

    [SerializeField]
    protected bool isPlayer;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected TMP_Text damageTextFieldPrefab;
    [SerializeField]
    protected Canvas damageCanvas;
    [SerializeField]
    protected Colors colors;

    protected CombatSceneController combatSceneController;
    public CombatSceneController CombatSceneController { get { return combatSceneController; } }

    private void Start() {
        currentHp = CurrentMaxHp;
    }

    void Awake() {
        GameObject mb = GameObject.Find("CombatManager");
        if (mb != null) {
            combatSceneController = mb.GetComponent<CombatSceneController>();
            if (!isPlayer) {
                EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
                StartCoroutine(enemyMovement.AttackCoroutine());
            }
        }
    }    

    public void Attack(Character target, bool crit) {        
        int damage = CurrentAtk - target.CurrentDefense;
        if (crit) {
            float f = damage * CurrentCritMult / 100f;
            damage = Mathf.CeilToInt(damage * CurrentCritMult / 100f);
            
        }
        TMP_Text field = Instantiate(damageTextFieldPrefab, damageCanvas.transform);
        field.SetText(damage.ToString());
        if (crit) {
            field.color = colors.CritColor;
        }
        target.ModifyHp(-damage);
    }

    public void ModifyHp(int amount) {
        currentHp += amount;
        if (currentHp <= 0) {
            if (isPlayer) {
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            } else {
                animator.SetTrigger("Dead");
            }
            return;
        }
        if (currentHp > CurrentMaxHp) {
            currentHp = CurrentMaxHp;
        }
    }

    public void SetAttacking(bool b) {
        animator.SetBool("Attacking", b);
    }
}
