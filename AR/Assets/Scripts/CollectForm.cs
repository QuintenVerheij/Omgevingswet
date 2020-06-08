using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectForm : MonoBehaviour
{
    Dictionary<String, String> Form = new Dictionary<String, String>();
    void InputHandler(int which, String input) {
        Form[which] = input;
    }
}
