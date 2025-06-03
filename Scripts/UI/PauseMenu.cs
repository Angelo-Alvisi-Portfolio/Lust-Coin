using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private BaseInput input;

    private void OnEnable() {
        Time.timeScale = 0f; // Pause the game
        BaseInput.ChangeInputStatus(InputStatus.PauseMenu);
    }

    private void OnDisable() {
        Time.timeScale = 1f; // Resume the game
        BaseInput.ChangeInputStatus(BaseInput.PreviousInputStatus);
        //input.ResetEscPresses();
    }
}
