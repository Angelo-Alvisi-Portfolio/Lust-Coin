using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPointerPositionController : MonoBehaviour, IPointerEnterHandler {

    [SerializeField]
    private FurnitureUI ui;

    public void OnPointerEnter(PointerEventData eventData) {
        ui.UIVisibilityToggle(true);
    }

    //public void OnPointerEnter(PointerEventData eventData) {
    //    foreach (Image image in toTransparency) {
    //        Color color = image.color;
    //        color.a = 1f;
    //        image.color = color;
    //    }
    //    foreach (GameObject gameObject in toDisable) {
    //        gameObject.SetActive(true);
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData) {        
    //    foreach (Image image in toTransparency) {
    //        Color color = image.color;
    //        color.a = 0f;
    //        image.color = color;
    //    }
    //    foreach (GameObject gameObject in toDisable) {
    //        gameObject.SetActive(false);
    //    }
    //}
}
