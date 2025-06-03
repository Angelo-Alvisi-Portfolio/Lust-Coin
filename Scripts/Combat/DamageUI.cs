using System.Collections;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour {
    [SerializeField]
    private TMP_Text text;
    private float elapsedTime = 0;
    private float startValue;
    private float position;
    private void Awake() {
        StartCoroutine(StartLogic());
    }

    private IEnumerator StartLogic() {
        yield return DisplayDamage();
    }

    private IEnumerator DisplayDamage() {        
        startValue = text.color.a;
        
        while (elapsedTime < 3) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 1, elapsedTime*0.3f);
            float newPosition = Mathf.Lerp(position, 0.02f, elapsedTime*5);
            text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
            text.transform.Translate(new Vector3(0, newPosition, 0));
            yield return null;
        }
        Destroy(gameObject);
    }
}
