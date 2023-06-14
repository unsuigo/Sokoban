using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Action<Vector2> OnInput;
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = Vector2.zero;

        if (horizontal == 0 && vertical == 0) return;
       
        
        if (horizontal != 0f && vertical == 0f)
        {
            movement = new Vector2(horizontal,  0f).normalized ;
        }
        else if (horizontal == 0f && vertical != 0f)
        {
            movement = new Vector2(0f, vertical).normalized;
        }
        
        OnInput?.Invoke(movement);
    }
}
