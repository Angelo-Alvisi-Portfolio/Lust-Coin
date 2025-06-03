using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonClickHandler : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData) {
        Vector3 v = eventData.pointerCurrentRaycast.worldPosition;
        Debug.Log("click dung");
        StaticManager.DungeonInput.ClickOnDungeon(v);        
    }

}
