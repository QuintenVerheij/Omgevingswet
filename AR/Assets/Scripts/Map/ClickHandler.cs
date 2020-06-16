using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public ClickMarker marker;
    public void OnPointerClick(PointerEventData eventData){
        marker.showPopup();
    }

}
