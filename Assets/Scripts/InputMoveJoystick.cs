using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMoveJoystick : FixedJoystick
{
    public static InputMoveJoystick instance;

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
