using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Puente puente;
    private Vector2 _mousePosition;
    private Vector3 _pointToStartRaycast, _pointToEndRaycast;
    private Puente puenteActual;

    public void MousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }
    public void Click(InputAction.CallbackContext context)
    {
        Debug.Log($"canceled {context.canceled}" );
        Debug.Log($"performed {context.performed}" );
        Debug.Log($"started {context.started}" );
        Debug.Log($"phase {context.phase}" );
        Debug.Log($"time {context.time}" );
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        if (context.started)
        {
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
            {
                _pointToStartRaycast = hit.point;
                Instantiate(prefab, hit.point, Quaternion.identity);
                puenteActual = Instantiate(puente, hit.point, Quaternion.identity);
            }
        } 
        else if (context.canceled)
        {
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
            {
                _pointToEndRaycast = hit.point;
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
            //get Rotation of the bridge
            Vector3 direction = _pointToEndRaycast - _pointToStartRaycast;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            puenteActual.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            puenteActual.GoTo(Vector3.Distance(_pointToStartRaycast, _pointToEndRaycast));
        }
        
    }
}