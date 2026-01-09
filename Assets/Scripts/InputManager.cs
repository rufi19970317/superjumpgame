using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<int> OnMoveInput;
    public static event Action OnJumpInput;

    void Update()
    {
        int moveDir = 0;

        if(Input.GetKey(KeyCode.A))
        {
            moveDir = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moveDir = 1;
        }

        if (moveDir != 0)
            OnMoveInput?.Invoke(moveDir);

        if(Input.GetKey(KeyCode.Mouse0))
        {
            OnJumpInput?.Invoke();
        }

        if(Input.GetKey(KeyCode.Mouse1))
        {

        }
    }
}
