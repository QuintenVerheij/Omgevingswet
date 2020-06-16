using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all input handlers
public class BaseModeInputHandler : MonoBehaviour{
    public virtual void OnPlaneTouchBegin(Vector3 position) { //happens when a touch hits the plane for the first time (only ONE finger)

    }
    //happens when a touch moves on the screen and hits the plane (only ONE finger)
    //delta is the difference between the current plane hit and the plane hit of the previous frame
    public virtual void OnPlaneTouchMove(Vector3 delta) { 

    }

    //happens when the user lifts up his finger and the touch has hit the plane at least once during its lifetime (only ONE finger)
    //startposition is the first position of the touch, currentPosition is the most recent position
    //startposition & currentposition are world space positions from plane hits
    public virtual void OnPlaneTouchEnd(Vector3 startPosition, Vector3 currentPosition) { 

    }

    //happens when a user lifts up his finger and the last touch has hit any transform. (only ONE finger)
    //Hit contains the result of the raycast from screen point
    //startposition is the first recorded position of a touch
    //currentposition is the latest recorded position of a touch
    //both are world space positions
    public virtual void OnScreenPointHitEnd(RaycastHit hit, Vector3 startPosition, Vector3 currentPosition) { 

    }

    //delta = difference between the current and previous position in pixel coordinates
    public virtual void OnScreenPointMove(Vector3 delta) {

    }

    //happens when two fingers are present on the screen
    //angleDelta is the angle difference between the current and last frame that has been calculated with the two finger positions
    public virtual void OnMultiTouchRotate(float angleDelta) {

    }

    //happens when two fingers are present on the screen
    //scaleDelta is the scale difference between the current and last frame that has been calculated with the two finger positions
    //if the distance between the fingers decreases it becomes less than 1.0
    //if the distance between the fingers increases it becomes more than 1.0

    public virtual void OnMultiTouchScale(float scaleDelta) {

    }
}