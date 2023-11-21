using System;
using SL;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCustom : ServiceCustom, IInputCustom
{
    private Vector2 _touchPosition;
    protected override bool Validation()
    {
        return FindObjectsOfType<InputCustom>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<IInputCustom>(this);
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IInputCustom>();
    }
    
    public void SetTouch(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OnStartTouch?.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnActionTouch?.Invoke();
                break;
        }
    }
    
    public void TouchPosition(InputAction.CallbackContext context)
    {
        _touchPosition = context.ReadValue<Vector2>();
    }

    public Vector2 GetTouchPosition()
    {
        return _touchPosition;
    }

    public Action OnActionTouch { get; set; }
    public Action OnStartTouch { get; set; }
}