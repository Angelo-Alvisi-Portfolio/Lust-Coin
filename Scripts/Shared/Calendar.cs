using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {
    [SerializeField, HideInInspector]
    private int currentDay = 1, currentMonth = 1, currentYear = 0;
    [SerializeField, HideInInspector]
    private float elapsedTime = 0;
    [SerializeField]
    /// <summary>
    /// Duration of the day in seconds
    private int gameTimeScale = 384;
    [SerializeField]
    private Image bar1, bar2;
    [SerializeField]
    private TMP_Text dayText, monthText, yearText;
    [SerializeField]
    private Button nextDayButton;
    [SerializeField]
    private Axel axel;
    [SerializeField]            
    private ManagerController managerController;    
    
    private bool firstShopLoad = true;
    [SerializeField]
    private DaySection daySection = DaySection.Morning;
    public DaySection DaySection => daySection;
    bool dayStarted = true;
    [SerializeField]
    private MorningRoutine morningRoutine;
    [SerializeField]
    private MainGameManager mainGameManager;

    private IEnumerator DayTimer() {
        while (dayStarted) {
            yield return new WaitForSeconds(1);
            AddTime(1);
            if (elapsedTime == gameTimeScale / 4 || elapsedTime == gameTimeScale / 4 * 2 || elapsedTime == gameTimeScale /4 * 3) {
                NextDaySection();
            } else if (elapsedTime == gameTimeScale) {
                elapsedTime = 0;
                NextDaySection();
            }            
        }
    }

    private void NextDaySection() {
        switch (daySection) {
            case DaySection.Dawn:
                daySection = DaySection.Morning;
                if (mainGameManager.CurrentScene == MainGameManager.LoadedScene.Shop) {
                    //shopStatusManager = FindFirstObjectByType<ShopStatusManager>();
                    managerController.ShopStatusManager.StartShopDaySegment(axel);
                }
                dayStarted = true;
                StartCoroutine(DayTimer());                
                break;
            case DaySection.Morning:
                daySection = DaySection.Day;
                //mainGameManager.LoadScene("Shop");
                if (mainGameManager.CurrentScene != MainGameManager.LoadedScene.Shop) {
                    mainGameManager.LoadScene("Shop");
                }
                managerController.ShopStatusManager.EndShopDaySegment(axel);
                managerController.ShopStatusManager.StartShopDaySegment(axel);
                break;
            case DaySection.Day:
                daySection = DaySection.Evening;
                //mainGameManager.LoadScene("Shop");                
                managerController.ShopStatusManager.EndShopDaySegment(axel);                
                managerController.ShopStatusManager.StartEveningSegment(firstShopLoad);
                firstShopLoad = false;
                break;
            case DaySection.Evening:
                daySection = DaySection.Night;
                break;
            case DaySection.Night:
                daySection = DaySection.Dawn;
                NextDay();
                break;
        }
    }

    private void AddTime(float time) {
        Vector3 delta = new Vector3(time / (gameTimeScale / 96), 0, 0);
        bar1.transform.localPosition -= delta;
        bar2.transform.localPosition -= delta;
        if (bar1.transform.localPosition.x == -96) {
            bar1.transform.localPosition = new Vector3(96, 2, 0);
        }
        if (bar2.transform.localPosition.x == -96) {
            bar2.transform.localPosition = new Vector3(96, 2, 0);
        }
        elapsedTime += time;
    }

    private void NextDay() {
        currentDay++;
        if (currentDay > 30) {
            currentDay = 1;
            currentMonth++;
            if (currentMonth > 12) {
                currentMonth = 1;
                currentYear++;
            }
        }
        dayText.text = $"{currentDay.ToString()}/{currentMonth.ToString()}/{currentYear.ToString()}";
        elapsedTime = 0;
        bar1.transform.localPosition = new Vector3(0, 2, 0);
        bar2.transform.localPosition = new Vector3(96, 2, 0);
        dayStarted = false;
        morningRoutine.gameObject.SetActive(true);
    }

    public void SkipDay() {
        daySection = DaySection.Night;
        elapsedTime = gameTimeScale - 1;
        managerController.ShopStatusManager.EndShopDaySegment(axel);
    }

    public void SkipSection() {
        switch (daySection) {
            case DaySection.Dawn:
                NextDaySection();
                break;
            case DaySection.Morning:                
                AddTime(gameTimeScale / 4 - 1 - elapsedTime);
                break;
            case DaySection.Day:
                AddTime(gameTimeScale / 4 * 2 - 1 - elapsedTime);
                break;
            case DaySection.Evening:
                AddTime(gameTimeScale / 4 * 3 - 1 - elapsedTime);
                break;
            case DaySection.Night:
                AddTime(gameTimeScale - 1 - elapsedTime);
                break;
        }
    }

}
