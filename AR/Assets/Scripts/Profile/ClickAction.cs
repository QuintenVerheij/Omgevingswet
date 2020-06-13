using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAction : MonoBehaviour, IPointerClickHandler
{
    public ReadProfile readProfile;
    public void OnPointerClick(PointerEventData eventData){
        readProfile.isProfilePicTapped = true;
    }

}
