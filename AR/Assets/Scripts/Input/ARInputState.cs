using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This class is used to store touch related data for multiple frames
public class TouchLifespan {
    public Touch start; //start of the finger touch
    public Touch current; //newest finger touch state

    public Vector3 firstPlaneHitPos; //first time hitting planning
    public Vector3 lastPlaneHitPos; //latest plane hit
    public bool hasHitPlane; //has the user with this touch touched the plane at least once?

    public TouchLifespan(Touch start) {
        this.start = start;
        this.current = start;
    }
}

//This class manages all finger touches in the ar scene and execute input events of the BaseModeInputHandler instance
//A BaseModeInputHandler instance is an object that has input event functions that can be executed by the ARInputState.
//There can be only 1 active BaseModeInputHandler instance at any time and that is determined by the current mode.
//When a mode switch happens, all but the BaseModeInputHandler of the selected mode will be turned off.
public class ARInputState : MonoBehaviour
{
    /*
    Modes:
    -Environment: place, move, rotate and scale the entire environment
    -AddObjects: add new objects in the environment with the selected prefab
    -SelectObjects: select/deselect and transform selected objects (move, rotate, scale)
    */
    public enum Mode {
        Environment, AddObjects, SelectObjects
    }
    public Mode CurrentMode { get; private set; } //The mode that is currently active
    
    public static ARInputState Instance { get; private set; }

    public List<BaseModeInputHandler> handlers = new List<BaseModeInputHandler>(); //Instances of BaseModeInputHandler need to be in the right order in the list (See the enum 'Mode')

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
        //Is used to store touches that needs to be removed from storedTouches at the end of the Update function
        Dictionary<int, TouchLifespan> endedTouches = new Dictionary<int, TouchLifespan>(); 

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase) {
                case TouchPhase.Began: //when a finger starts touching the screen
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) == false) { //do not keep track of the finger touch if it touches an UI object (ex: Button)
                        storedTouches.Add(touch.fingerId, new TouchLifespan(touch));
                    }
                    break;
                case TouchPhase.Moved: //when the user moves his finger on the screen
                case TouchPhase.Stationary: //when the user keeps pressing on the screen without moving his finger
                    TouchLifespan storedTouch;
                    if (storedTouches.TryGetValue(touch.fingerId, out storedTouch)) {
                        storedTouch.current = touch; //update value stored in the dictionary of storedTouches
                    }
                    break;
                case TouchPhase.Ended: //when the user lifts his finger from the screen
                case TouchPhase.Canceled: //cancelled by the operating system
                    TouchLifespan endedTouch;
                    if(storedTouches.TryGetValue(touch.fingerId, out endedTouch)) {
                        endedTouch.current = touch;
                        endedTouches.Add(touch.fingerId, endedTouch);
                    }
                    break;
            }
        }

        int[] keys = new int[storedTouches.Keys.Count];
        storedTouches.Keys.CopyTo(keys, 0);
        
        if(keys.Length == 1) { //one finger press
            TouchLifespan touch = storedTouches[keys[0]];
            if(touch.current.phase == TouchPhase.Began || touch.current.phase == TouchPhase.Moved) {
                Ray ray = Camera.main.ScreenPointToRay(touch.current.position); //convert the touch position on the screen to a ray
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, collisionPlaneMask)) {
                    if (touch.current.phase == TouchPhase.Began) {
                        //update touch data
                        touch.hasHitPlane = true;
                        touch.firstPlaneHitPos = hit.point;
                        touch.lastPlaneHitPos = hit.point;

                        currentHandler.OnPlaneTouchBegin(touch.firstPlaneHitPos); //execute event
                    }
                    else if (touch.current.phase == TouchPhase.Moved && touch.hasHitPlane) {
                        Vector3 delta = hit.point - touch.lastPlaneHitPos;
                        touch.lastPlaneHitPos = hit.point;
                        currentHandler.OnPlaneTouchMove(delta); //execute event
                    }
                }
            }

            if(touch.current.phase == TouchPhase.Ended || touch.current.phase == TouchPhase.Canceled) {
                if (touch.hasHitPlane) {
                    currentHandler.OnPlaneTouchEnd(touch.firstPlaneHitPos, touch.lastPlaneHitPos); //execute event
                }
                Ray ray = Camera.main.ScreenPointToRay(touch.current.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)) {
                    currentHandler.OnScreenPointHitEnd(hit, touch.start.position, touch.current.position); //execute event
                }
            }
        }
        else if(keys.Length == 2){ //two finger presses
            TouchLifespan touch0 = storedTouches[keys[0]];
            TouchLifespan touch1 = storedTouches[keys[1]];

            if(touch0.current.phase == TouchPhase.Moved || touch1.current.phase == TouchPhase.Moved) {
                Vector2 diff = touch1.current.position - touch0.current.position;
                Vector2 prevDiff =
                    (touch1.current.position - touch1.current.deltaPosition) -
                    (touch0.current.position - touch0.current.deltaPosition);
                
                //rotation
                Vector2 direction = diff.normalized;
                Vector2 prevDirection = prevDiff.normalized;
                
                float angleDelta = -Vector2.SignedAngle(prevDirection, direction);
                currentHandler.OnMultiTouchRotate(angleDelta); //execute event

                //scaling
                float scaleMultiplier = diff.magnitude / prevDiff.magnitude;
                currentHandler.OnMultiTouchScale(scaleMultiplier); //execute event
            }
        }
        else { //more than 2 is not supported, clear the dictionary of stored touches
            storedTouches.Clear();
        }

        foreach (var touch in endedTouches) { //remove all touches that needs to be removed
            storedTouches.Remove(touch.Key);
        }
    }

    private void OnModeChange() { //happens once in the beginning and everytime when the mode changes
        for(int i = 0; i < handlers.Count; i++) {
            bool active = i == (int)CurrentMode; //only activate the newly selected mode, deactivate the rest
            handlers[i].gameObject.SetActive(active);
        }
        Debug.Log("Current mode: " + CurrentMode.ToString());
    }

    public void SetMode(Mode mode) {
        CurrentMode = mode;
        OnModeChange();
    }

    public void NextMode() { //should happen when the user presses on the change mode button
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
