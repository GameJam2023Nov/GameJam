using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Nutria : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float toleranceDistance;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private AnimatorControllerCustom animatorControllerCustom;
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
        CanMove(true);
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
        CanMove(_canMove);
        if (Vector3.Distance(transform.position, _targetPosition) < toleranceDistance)
        {
            _canMove = false;
            _moveToTarget = false;
            CanMove(_canMove);
            animatorControllerCustom.SetVelocity(0);
            return;
        }
        var direction = (_targetPosition - transform.position).normalized;
        animatorControllerCustom.SetVelocity(direction.magnitude);
        rigidbody.MovePosition(transform.position + direction * (speed * Time.deltaTime));
        //set rotation to direction
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        Debug.Log("Move");
    }

    private void CanMove(bool canMove)
    {
        if (canMove)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void StartIdle()
    {
        //_canMove = true;
        CanMove(false);
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
            animatorControllerCustom.SetIntoWater(true);
        }
        if (other.gameObject.CompareTag("TierraFinal"))
        {
            onTouchFinalEarth?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water") && other.gameObject.CompareTag("Agua"))
        {
            speed /= 1.5f;
            animatorControllerCustom.SetIntoWater(false);
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

    public void DeadNutria()
    {
        animatorControllerCustom.IsDead(true);
    }
}