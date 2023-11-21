using System;
using SL;
using UnityEngine;

public class SelectorOfNutria : MonoBehaviour, ISelectorOfNutria
{
    [SerializeField] private ValidationOfVictory validationOfVictory;
    [SerializeField] private LayerMask layerMaskNutria, layerMaskMap;
    [SerializeField] private Nutria _selectedNutria;
    [SerializeField] private ValidatorOfClick validatorOfClick;
    private IInputCustom _inputCustom;
    private bool _canSelect;
    private Camera _camera;
    private bool _canEvaluate;
    
    public void Configure()
    {
        _inputCustom = ServiceLocator.Instance.GetService<IInputCustom>();
        _camera = Camera.main;
        _canSelect = true;
        _inputCustom.OnActionTouch += SelectNutria;
        _inputCustom.OnStartTouch += OnStartTouch;
        validatorOfClick.Configure(this, _inputCustom);
    }

    private void OnStartTouch()
    {
        
    }

    private void FixedUpdate()
    {
        if(!_canSelect) return;
        var ray = _camera.ScreenPointToRay(_inputCustom.GetTouchPosition());
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMaskNutria) && hit.collider.TryGetComponent<ColliderNutria>(out var colliderNutria))
        {
            if (!colliderNutria.Nutria.CanSelected())
            {
                ServiceLocator.Instance.GetService<ICursorService>().StateOfRelease(true);
                return;
            }
        }
        if(Physics.Raycast(ray, out var hit2, Mathf.Infinity, layerMaskMap))
        {
            ServiceLocator.Instance.GetService<ICursorService>().StateOfCursor(false);
        }
    }

    private void SelectNutria()
    {
        if(!_canSelect) return;
        var ray = _camera.ScreenPointToRay(_inputCustom.GetTouchPosition());
        if (_selectedNutria == null)
        {
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMaskNutria) && hit.collider.TryGetComponent<ColliderNutria>(out var colliderNutria))
            {
                if(!colliderNutria.Nutria.CanSelected())
                {
                    if (validatorOfClick.CanRelease())
                    {
                        validationOfVictory.SendNutriaToDestiny(colliderNutria.Nutria, hit.point, validatorOfClick.GetAnchorPoint());
                    }
                    Debug.Log("No se puede seleccionar y debe ser liberado");
                    return;
                }
                ServiceLocator.Instance.GetService<ICursorService>().StateOfCursor(true);
                validatorOfClick.Evaluate();
                _selectedNutria = colliderNutria.Nutria;
            }
        }
        else if (_selectedNutria != null)
        {
            validatorOfClick.EndEvaluate();
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMaskMap) && validatorOfClick.CanMove())
            {
                validationOfVictory.SendNutriaToDestiny(_selectedNutria, hit.point, validatorOfClick.GetAnchorPoint());
            }
            _selectedNutria = null;
            ServiceLocator.Instance.GetService<ICursorService>().StateOfCursor(false);
        }
        
    }
}