using UnityEngine;

public class IngameMenu : MonoBehaviour {

    [SerializeField]
    private GameObject invMenu, statsMenu, storageMenu, upgMenu;

    void OnEnable() {
        Time.timeScale = 0f;
        OpenStatsMenu();
        BaseInput.ChangeInputStatus(InputStatus.IngameMenu);
    }

    private void OnDisable() {
        Time.timeScale = 1f;
        BaseInput.ChangeInputStatus(BaseInput.PreviousInputStatus);
    }

    public void OpenStatsMenu() {
        statsMenu.SetActive(true);
        storageMenu.SetActive(false);
        upgMenu.SetActive(false);
        invMenu.SetActive(false);
    }

    public void OpenInventory() {
        statsMenu.SetActive(false);
        storageMenu.SetActive(false);
        upgMenu.SetActive(false);
        invMenu.SetActive(true);
    }

    public void OpenStorage() {
        statsMenu.SetActive(false);
        storageMenu.SetActive(true);
        upgMenu.SetActive(false);
        invMenu.SetActive(false);
    }

    public void OpenUpgradeMenu() {
        statsMenu.SetActive(false);
        storageMenu.SetActive(false);
        upgMenu.SetActive(true);
        invMenu.SetActive(false);
    }

}
