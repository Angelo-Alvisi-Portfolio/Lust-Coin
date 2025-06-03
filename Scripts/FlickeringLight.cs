using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour {

    [SerializeField]
    private Light2D dlight;
    [SerializeField]
    private float minIntensity = 1f, maxIntensity = 2f, speed = 0.5f;
    
    private float velocity = 0.0f;

    public void Awake() {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker() {
        bool up = true;
        while (true) {
            if (up) {
                //2Dligh.intensity = Mathf.SmoothDamp(2Dligh.intensity, maxIntensity, ref velocity, speed);
                dlight.intensity += speed * Time.deltaTime;
            } else {
                dlight.intensity -= speed * Time.deltaTime;
                //2Dligh.intensity = Mathf.SmoothDamp(2Dligh.intensity, minIntensity, ref velocity, speed);
            }
            if (dlight.intensity >= maxIntensity) {
                up = false;
            }
            if (dlight.intensity <= minIntensity) {
                up = true;
            }
            yield return null;
            
        }
    }
}
