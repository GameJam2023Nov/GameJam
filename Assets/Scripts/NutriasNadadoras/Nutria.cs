using UnityEngine;

public abstract class Nutria : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float toleranceDistance;
    private Vector3 _pointToAnchor;
    private bool _canMove;
    private Vector3 _targetPosition;
    private bool _moveToTarget;
    public void Configure()
    {
        _canMove = false;
        _targetPosition = transform.position;
    }
    
    public void GoTo(Vector3 position, Vector3 pointToAnchor)
    {
        if(_moveToTarget) return;
        _pointToAnchor = pointToAnchor;
        _targetPosition = position;
        _canMove = true;
        _moveToTarget = true;
    }

    private void Update()
    {
        if (!_canMove) return;
        if (Vector3.Distance(transform.position, _targetPosition) < toleranceDistance)
        {
            _canMove = false;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

    public void StartIdle()
    {
        _canMove = true;
    }

    public bool CanSelected()
    {
        return !_moveToTarget;
    }

    public void AddForce(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime;
    }
}