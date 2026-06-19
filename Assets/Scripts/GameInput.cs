using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    Vector3 inputVector = new Vector3(0, 0, 0);
    private PlayerInputActions inputActions;
    public event EventHandler onInteractAction;
    public event EventHandler onAltInterAction;
    public event EventHandler onPauseAction;
    public event EventHandler onBindingRebind;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        InteractAlternate,
        Pause,
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Awake()
    {
        Instance= this;
        inputActions= new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed +=Interact_performed;
        inputActions.Player.AltInterAct.performed +=AltInterAct_performed;
        inputActions.Player.Pause.performed+=Pause_performed;
        
    }
    private void OnDestroy()
    {
        inputActions.Player.Interact.performed -=Interact_performed;
        inputActions.Player.AltInterAct.performed -=AltInterAct_performed;
        inputActions.Player.Pause.performed -= Pause_performed;
        inputActions.Dispose();
    }

    private void AltInterAct_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onAltInterAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onInteractAction?.Invoke(this, EventArgs.Empty);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetMovementVector()
    {
        inputVector = inputActions.Player.Move.ReadValue<Vector3>();
        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVector.z +=1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.z-=1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x+=1;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x-=1;
        //}
        
        return inputVector;
    }
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return inputActions.Player.Move.bindings[5].ToDisplayString();
            case Binding.Move_Down:
                return inputActions.Player.Move.bindings[6].ToDisplayString();
            case Binding.Move_Right:
                return inputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Left:
                return inputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Interact :
                return inputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return inputActions.Player.AltInterAct.bindings[0].ToDisplayString();
            case Binding.Pause:
                return inputActions.Player.Pause.bindings[0].ToDisplayString();
           

        }
        
    }

    public void ReBindBinding(Binding binding, Action onActionRebound)
    {
        inputActions.Player.Disable();
        switch (binding) {
            case Binding.Move_Up :
                inputActions.Player.Move.PerformInteractiveRebinding(5)
                .OnComplete(callback =>
                 {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
            break;
            case Binding.Move_Down:
                inputActions.Player.Move.PerformInteractiveRebinding(6)
                .OnComplete(callback =>
                {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
                break;
            case Binding.Move_Left:
                inputActions.Player.Move.PerformInteractiveRebinding(3)
                .OnComplete(callback =>
                {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
                break;
            case Binding.Move_Right:
                inputActions.Player.Move.PerformInteractiveRebinding(4)
                .OnComplete(callback =>
                {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
                break;
            case Binding.Pause:
                inputActions.Player.Pause.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
                break;
            case Binding.Interact:
                inputActions.Player.Interact.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
                break;
            case Binding.InteractAlternate:
                inputActions.Player.AltInterAct.PerformInteractiveRebinding(0)
                .OnComplete(callback =>
                {

                    callback.Dispose();
                    inputActions.Player.Enable();
                    onActionRebound();
                }).Start();
                break;
        }
        onBindingRebind?.Invoke(this, EventArgs.Empty);
        inputActions.Player.Enable();
    }
}
