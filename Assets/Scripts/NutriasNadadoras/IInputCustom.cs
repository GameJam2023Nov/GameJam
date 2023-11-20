using System;
using UnityEngine;

public interface IInputCustom
{
    Vector2 GetTouchPosition();
    Action OnActionTouch { get; set; }
    Action OnStartTouch { get; set; }
}