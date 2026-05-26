using UnityEngine;
using UnityEngine.InputSystem;

public class ReadInputs : MonoBehaviour
{
    public InputActionReference moveInput;

    public Vector2 move;


    void Update() 
    {
        GetInputs();
    }

    void GetInputs() 
    {
        move = moveInput.action.ReadValue<Vector2>();
    }
}