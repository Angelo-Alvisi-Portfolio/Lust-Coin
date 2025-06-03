using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour {

    private LoadedScene currentScene;
    public LoadedScene CurrentScene => currentScene;
    [SerializeField]
    private Canvas mainUIcanvas;
    [SerializeField]
    private MorningRoutine morningRoutine;
    [SerializeField]
    private ManagerController managerController;
    public ManagerController ManagerController => managerController;
    [SerializeField]
    private Axel axel;
    [SerializeField]
    private CameraClamp shopCameraClamp, dungeonCameraClamp, combatCameraClamp;
    [SerializeField]
    private ShopInput shopInput;
    [SerializeField]
    private DungeonInput dungeonInput;
    [SerializeField]
    private CombatInput combatInput;


    private void OnEnable() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        FirstLoad();
    }

    private void FirstLoad() {
        StaticManager.DungeonInput = dungeonInput;
//#if UNITY_EDITOR
//        currentScene = LoadedScene.Shop;
//        axel.ShopCameraActive(true);    
//        return;
//#endif
        currentScene = LoadedScene.MainMenu;
#if !UNITY_EDITOR
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
#endif
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
    }

    public void NewGame() {
        //UnloadCurrentScene();
        LoadScene("Shop");
        
        morningRoutine.gameObject.SetActive(true);
    }

    public void LoadScene(string sceneName) {
        if ((sceneName == "Main Menu" && currentScene == LoadedScene.MainMenu) || (sceneName == "Combat" && currentScene == LoadedScene.Combat) || (sceneName == "Shop" && currentScene == LoadedScene.Shop) || (sceneName == "Dungeon" && currentScene == LoadedScene.Dungeon)) {
            return;
        }
        UnloadCurrentScene();
        if (sceneName != "Shop") {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        switch (sceneName) {
            case "Main Menu":
                mainUIcanvas.gameObject.SetActive(false);
                currentScene = LoadedScene.MainMenu;
                break;
            case "Combat":
                currentScene = LoadedScene.Combat;
                axel.gameObject.SetActive(true);
                axel.Animator.SetTrigger("Combat");
                Camera.main.GetComponent<ClampedCamera>().ChangeClamps(combatCameraClamp);
                axel.transform.position = new Vector3(-54f, -5.625f, 0);
                axel.SpriteRenderer.flipX = false;
                combatInput.enabled = true;
                break;
            case "Shop":
                currentScene = LoadedScene.Shop;
                Camera.main.GetComponent<ClampedCamera>().ChangeClamps(shopCameraClamp);
                axel.transform.position = new Vector3(0, -5.625f, 0);
                axel.SpriteRenderer.flipX = false;
                shopInput.enabled = true;
                break;
            case "Dungeon":
                currentScene = LoadedScene.Dungeon;
                Camera.main.GetComponent<ClampedCamera>().ChangeClamps(dungeonCameraClamp);
                axel.transform.position = new Vector3(46, 0, 0);
                axel.SpriteRenderer.flipX = false;
                dungeonInput.enabled = true;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }


    private void UnloadCurrentScene() {
        switch (currentScene) {
            case LoadedScene.MainMenu:
                SceneManager.UnloadSceneAsync("Main Menu");
                break;
            case LoadedScene.Combat:
                StaticManager.CombatSceneController.CurrentEnemy.StopAllCoroutines();
                Destroy(StaticManager.CombatSceneController.CurrentEnemy);
                SceneManager.UnloadSceneAsync("Combat");
                axel.Animator.SetTrigger("Combat");
                mainUIcanvas.gameObject.SetActive(true);
                combatInput.enabled = false;
                break;
            case LoadedScene.Shop:
                //SceneManager.UnloadSceneAsync("Shop");
                mainUIcanvas.gameObject.SetActive(true);
                shopInput.enabled = false;
                break;
            case LoadedScene.Dungeon:
                SceneManager.UnloadSceneAsync("Dungeon");
                mainUIcanvas.gameObject.SetActive(true);
                dungeonInput.enabled = false;
                break;
            default:
                throw new System.ArgumentOutOfRangeException();                
        }
    }    

    public enum LoadedScene { MainMenu, Combat, Shop, Dungeon }


    public void ExitGame() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
