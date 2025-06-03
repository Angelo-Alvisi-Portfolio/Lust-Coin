using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightLightTransition : MonoBehaviour {
    [SerializeField]
    private Light2D light;
    [SerializeField]
    private float dayDuration, dayTwTrDuration, twilightDuration, twNiTrDuration, nightDuration, niDawnTrDuration, dawnDuration, dawnDayTrDuration, speed, lerpFactor, transitionSpeed;
    [SerializeField]
    private Colors colors;

    

    private int status = 0;


    private void Awake() {
        StartCoroutine(Cycle());
        StartCoroutine(ChangeColor());
    }

    private void OnDisable() {
        StopCoroutine(Cycle());
        StopCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor() {
        while (true) {
            switch (status) {
                case 1:
                    light.color = Color.Lerp(light.color, colors.TwilightColor, lerpFactor);
                    yield return new WaitForSeconds(transitionSpeed);
                    break;
                case 3:
                    light.color = Color.Lerp(light.color, colors.NightLightColor, lerpFactor);
                    yield return new WaitForSeconds(transitionSpeed);
                    break;
                case 5:
                    light.color = Color.Lerp(light.color, colors.DawnLightColor, lerpFactor);
                    yield return new WaitForSeconds(transitionSpeed);
                    break;
                case 7:
                    light.color = Color.Lerp(light.color, colors.DayLightColor, lerpFactor);
                    yield return new WaitForSeconds(transitionSpeed);
                    break;
                default:
                    yield return null;
                    break;
            }
        }
    }

    private IEnumerator Cycle() {
        while (true) {
            switch (status) {
                case 0:
                    yield return new WaitForSeconds(dayDuration * speed);
                    status++;
                    break;
                case 1:
                    yield return new WaitForSeconds(dayTwTrDuration * speed);
                    status++;
                    break;
                case 2:
                    yield return new WaitForSeconds(twilightDuration * speed);
                    status++;
                    break;
                case 3:
                    yield return new WaitForSeconds(twNiTrDuration * speed);
                    status++;
                    break;
                case 4:
                    yield return new WaitForSeconds(nightDuration * speed);
                    status++;
                    break;
                    case 5:
                    yield return new WaitForSeconds(niDawnTrDuration * speed);
                    status++;
                    break;
                case 6:
                    yield return new WaitForSeconds(dawnDuration * speed);
                    status++;
                    break;
                case 7:
                    yield return new WaitForSeconds(dawnDayTrDuration * speed);
                    status = 0;
                    break;
                default:
                    yield return null;
                    break;                    
            }          
        }
    }

}
