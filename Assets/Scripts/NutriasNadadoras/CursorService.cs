using SL;
using UnityEngine;
using UnityEngine.UI;

public class CursorService : ServiceCustom, ICursorService
{
    [SerializeField] private Image cursor;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Sprite cursorNormal, cursorMove, cursorRelease;
    [SerializeField] private Material materialNormal, materialMoveGood, materialMoveBad, materialToRelease;
    protected override bool Validation()
    {
        return FindObjectsOfType<CursorService>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<ICursorService>(this);
        Cursor.visible = false;
        
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<ICursorService>();
    }

    public void StateOfCursor(bool canEvaluate)
    {
        if (canEvaluate)
        {
            cursor.sprite = cursorMove;
            cursor.color = Color.white;
        }else
        {
            cursor.sprite = cursorNormal;
            cursor.color = materialNormal.color;
        }
    }

    public void StateOfCursorEvaluator(bool isGood)
    {
        cursor.color = isGood ? materialMoveGood.color : materialMoveBad.color;
    }

    public void StateOfRelease(bool isRelease)
    {
        if (isRelease)
        {
            cursor.sprite = cursorRelease;
            cursor.color = materialToRelease.color;
        }
        else
        {
            cursor.sprite = cursorNormal;
            cursor.color = materialNormal.color;
            
        }
    }

    private void Update()
    {
        cursor.transform.position = ServiceLocator.Instance.GetService<IInputCustom>().GetTouchPosition() + offset;
    }
}