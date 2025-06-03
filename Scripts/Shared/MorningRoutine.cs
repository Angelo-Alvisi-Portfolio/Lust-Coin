using UnityEngine;

public class MorningRoutine : MonoBehaviour {

    [SerializeField]
    private MainGameManager mainGameManager;
    [SerializeField]
    private Calendar calendar;

    private void OnEnable() {
        BaseInput.ChangeInputStatus(InputStatus.MorningRoutine);
    }

    public void GoToShop() {        
        gameObject.SetActive(false);
        BaseInput.ChangeInputStatus(InputStatus.EmptyShop);
        calendar.SkipSection();
    }

    public void GoToCombat() {
        gameObject.SetActive(false);
        mainGameManager.LoadScene("Combat");
        BaseInput.ChangeInputStatus(InputStatus.Idle);
        calendar.SkipSection();
    }

    public void GoToTown() {
        gameObject.SetActive(false);
    }
}
