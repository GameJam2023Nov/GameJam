using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Nutria : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float toleranceDistance;
    [SerializeField] private Rigidbody rigidbody;
    public Action<GameObject> onTouchFinalEarth;
    private Vector3 _pointToAnchor;
    private bool _canMove;
    private Vector3 _targetPosition;
    private bool _moveToTarget;
    private bool _wasSelected;
    private bool _wasDeleted;

    public void Configure()
    {
        _canMove = false;
        _targetPosition = transform.position;
    }
    
    public void GoTo(Vector3 position, Vector3 pointToAnchor)
    {
        //Debug.Log("Go to: " + position);
        if(_moveToTarget) return;
        _pointToAnchor = pointToAnchor;
        _targetPosition = position;
        _canMove = true;
        _moveToTarget = true;
        _wasSelected = true;
    }

    private void Update()
    {
        if (!_canMove) return;
        if (Vector3.Distance(transform.position, _targetPosition) < toleranceDistance)
        {
            _canMove = false;
            _moveToTarget = false;
            return;
        }
        //transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        //change how to move to the target with rigidbody
        //Debug.Log($"Speed {speed}");
        var direction = (_targetPosition - transform.position).normalized;
        rigidbody.MovePosition(transform.position + direction * (speed * Time.deltaTime));
        //rigidbody.AddForce(direction * (speed * Time.deltaTime));
        //rigidbody.velocity = direction * (speed * Time.deltaTime);
        //rigidbody.AddForce(direction * (speed * Time.deltaTime));
        //rigidbody.AddForce(direction * (speed * Time.deltaTime), ForceMode.VelocityChange);
        //rigidbody.AddForce(direction * speed * Time.deltaTime, ForceMode.Acceleration);
        //rigidbody.AddForce(direction * speed * Time.deltaTime, ForceMode.Force);
        //rigidbody.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
    }

    public void StartIdle()
    {
        _canMove = true;
    }

    public bool CanSelected()
    {
        return !_wasSelected;
    }

    public void AddForce(Vector3 direction)
    {
        rigidbody.AddForce(direction * speed);
    }

    public void Release()
    {
        Debug.Log("Release");
    }

    public bool ArriveToDestiny()
    {
        return !_canMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water") && other.gameObject.CompareTag("Agua"))
        {
            speed *= 1.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water") && other.gameObject.CompareTag("Agua"))
        {
            speed /= 1.5f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("TierraFinal"))
        {
            onTouchFinalEarth?.Invoke(other.gameObject);
        }
    }

    public bool WasDeleted()
    {
        return _wasDeleted;
    }

    public void Deleted()
    {
        _wasDeleted = true;
    }
}