using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all input handlers
public class BaseModeInputHandler : MonoBehaviour{
    public virtual void OnPlaneTouchBegin(Vector3 position) {

    }
    public virtual void OnPlaneTouchMove(Vector3 delta) {

    }
    public virtual void OnPlaneTouchEnd(Vector3 startPosition, Vector3 currentPosition) {

    }

    public virtual void OnScreenPointHitEnd(RaycastHit hit, Vector3 startPosition, Vector3 currentPosition) {

    }

    public virtual void OnMultiTouchRotate(float angleDelta) {

    }

    public virtual void OnMultiTouchScale(float scaleDelta) {

    }
}