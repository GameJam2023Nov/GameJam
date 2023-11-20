using System;
using SL;
using UnityEngine;

public class ValidatorOfClick : MonoBehaviour
{
    [SerializeField] private float radius;
    //evaluate if the position is avalibe
    //Is close to earth
    //Is close to other nutria
    //Is into the water
    //Is close to the border
    [SerializeField] private bool canMove;
    private bool _canEvaluate;
    private ISelectorOfNutria _selectorOfNutria;
    private IInputCustom _inputCustom;
    private Vector3 _anchorPoint;

    public void Configure(ISelectorOfNutria selectorOfNutria, IInputCustom inputCustom)
    {
        _inputCustom = inputCustom;
        _selectorOfNutria = selectorOfNutria;
    }

    public void Evaluate()
    {
        _canEvaluate = true;
    }

    public void EndEvaluate()
    {
        _canEvaluate = false;
    }

    private void Update()
    {
        if(!_canEvaluate) return;
        var ray = Camera.main.ScreenPointToRay(_inputCustom.GetTouchPosition());
        if (Physics.Raycast(ray, out var hit))
        {
            SphereCastAroundPoint(hit.point, radius);
        }
    }

    public bool CanMove()
    {
        return canMove;
    }
    
    private void OnDrawGizmos()
    {
        if(!_canEvaluate) return;
        var ray = Camera.main.ScreenPointToRay(_inputCustom.GetTouchPosition());
        if (Physics.Raycast(ray, out var hit))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hit.point, radius);
        }
    }

    void SphereCastAroundPoint(Vector3 center, float radius)
    {
        RaycastHit[] hits = Physics.SphereCastAll(center, radius, Vector3.up, 0f);

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Tierra"))
                {
                    Debug.Log("Tierra");
                    CanMoveP();
                    _anchorPoint = hit.point;
                    return;
                }
                
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Nutria"))
                {
                    Debug.Log("Nutria");
                    CanMoveP();
                    _anchorPoint = hit.point;
                    return;
                }
            }
        }
        canMove = false;
        ServiceLocator.Instance.GetService<ICursorService>().StateOfCursorEvaluator(canMove);
    }

    private void CanMoveP()
    {
        canMove = true;
        ServiceLocator.Instance.GetService<ICursorService>().StateOfCursorEvaluator(canMove);
    }

    public Vector3 GetAnchorPoint()
    {
        return _anchorPoint;
    }
}