using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLifespan {
    public Touch start;
    public Touch current;

    public Vector3 firstPlaneHitPos;
    public Vector3 lastPlaneHitPos;
    public bool hasHitPlane;

    public TouchLifespan(Touch start) {
        this.start = start;
        this.current = start;
    }
}

public class ARInputState : MonoBehaviour
{
    public enum Mode {
        Environment, AddObjects, SelectObjects
    }
    public Mode CurrentMode { get; private set; }
    
    public static ARInputState Instance { get; private set; }

    public List<BaseModeInputHandler> handlers = new List<BaseModeInputHandler>();

    private Dictionary<int, TouchLifespan> storedTouches = new Dictionary<int, TouchLifespan>();

    public LayerMask collisionPlaneMask;
    private void Awake()
    {
        Instance = this;
    }

    private void Start() {
        OnModeChange();
    }


    private void Update() {
        BaseModeInputHandler currentHandler = handlers[(int)CurrentMode];
        Dictionary<int, TouchLifespan> endedTouches = new Dictionary<int, TouchLifespan>();

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase) {
                case TouchPhase.Began:
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) == false) {
                        storedTouches.Add(touch.fingerId, new TouchLifespan(touch));
                    }
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    TouchLifespan storedTouch;
                    if (storedTouches.TryGetValue(touch.fingerId, out storedTouch)) {
                        storedTouch.current = touch;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    TouchLifespan endedTouch;
                    if(storedTouches.TryGetValue(touch.fingerId, out endedTouch)) {
                        endedTouches.Add(touch.fingerId, endedTouch);
                    }
                    break;
            }
        }

        if(storedTouches.Count > 0) {
            print("touch count: " + storedTouches.Count);
        }

        int[] keys = new int[storedTouches.Keys.Count];
        storedTouches.Keys.CopyTo(keys, 0);
        
        if(keys.Length == 1) {
            TouchLifespan touch = storedTouches[keys[0]];
            if(touch.current.phase != TouchPhase.Stationary) {
                Ray ray = Camera.main.ScreenPointToRay(touch.current.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, collisionPlaneMask)) {
                    if (touch.current.phase == TouchPhase.Began) {
                        touch.hasHitPlane = true;
                        touch.firstPlaneHitPos = hit.point;
                        touch.lastPlaneHitPos = hit.point;

                        currentHandler.OnPlaneTouchBegin(touch.firstPlaneHitPos);
                    }
                    else if (touch.current.phase == TouchPhase.Moved && touch.hasHitPlane) {
                        Vector3 delta = hit.point - touch.lastPlaneHitPos;
                        touch.lastPlaneHitPos = hit.point;
                        currentHandler.OnPlaneTouchMove(delta);
                    }
                }
                if ((touch.current.phase == TouchPhase.Ended || touch.current.phase == TouchPhase.Canceled) && touch.hasHitPlane) {
                    currentHandler.OnPlaneTouchEnd(touch.firstPlaneHitPos, touch.lastPlaneHitPos);
                }
            }

            if(touch.current.phase == TouchPhase.Ended || touch.current.phase == TouchPhase.Canceled) {
                Ray ray = Camera.main.ScreenPointToRay(touch.current.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)) {
                    currentHandler.OnScreenPointHitEnd(hit, touch.start.position, touch.current.position);
                }
            }
        }
        else if(keys.Length == 2){
            TouchLifespan touch0 = storedTouches[keys[0]];
            TouchLifespan touch1 = storedTouches[keys[1]];

            if(touch0.current.phase == TouchPhase.Moved || touch1.current.phase == TouchPhase.Moved) {
                Vector2 diff = touch1.current.position - touch0.current.position;
                Vector2 prevDiff =
                    (touch1.current.position - touch1.current.deltaPosition) -
                    (touch0.current.position - touch0.current.deltaPosition);
                //Vector2 beginDiff = touch1.start.position - touch0.start.position;
                
                //rotation
                Vector2 direction = diff.normalized;
                Vector2 prevDirection = prevDiff.normalized;
                
                float angleDelta = Vector2.Angle(Vector2.up, prevDirection) - Vector2.Angle(Vector2.up, direction);
                currentHandler.OnMultiTouchRotate(angleDelta);

                //scaling
                float scaleMultiplier = diff.magnitude / prevDiff.magnitude;
                currentHandler.OnMultiTouchScale(scaleMultiplier);
            }
        }
        else {
            storedTouches.Clear();
        }

        foreach (var touch in endedTouches) {
            storedTouches.Remove(touch.Key);
        }
    }

    private void OnModeChange() {
        for(int i = 0; i < handlers.Count; i++) {
            bool active = i == (int)CurrentMode;
            handlers[i].gameObject.SetActive(active);
        }
        Debug.Log("Current mode: " + CurrentMode.ToString());
    }

    public void SetMode(Mode mode) {
        CurrentMode = mode;
        OnModeChange();
    }

    public void NextMode() {
        Mode mode = CurrentMode;
        if(mode == Mode.SelectObjects) {
            mode = Mode.Environment;
        }
        else {
            mode += 1;
        }
        SetMode(mode);
    }
}
