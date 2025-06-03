using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour {

    [SerializeField]
    private TMP_Text fpsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("GetFPS", 1f, 1f);
    }

    private void GetFPS() {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
    }
}
