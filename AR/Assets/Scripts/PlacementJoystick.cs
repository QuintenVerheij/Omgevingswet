using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementJoystick : MonoBehaviour
{
    public ModelSwitcher switcher;
    private bl_Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        joystick = GetComponent<bl_Joystick>();
    }

    // Update is called once per frame
    void Update(){
        float dt = Time.deltaTime;
        switcher.MoveSelection(new Vector3(joystick.Horizontal * dt, 0, joystick.Vertical * dt));
    }
}
