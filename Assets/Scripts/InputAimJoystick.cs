using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAimJoystick : FixedJoystick
{
    public static InputAimJoystick instance;

    void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }
    }
}
