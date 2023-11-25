using UnityEngine;

internal class Temporizador : MonoBehaviour
{
    [SerializeField] private float timeToFinish;
    private float _currentTime;
    private bool _startTimer, _isTimeOut;
    public bool IsTimeOut => _isTimeOut;

    public void Configure()
    {
        _currentTime = timeToFinish;
        _startTimer = true;
    }

    private void Update()
    {
        if (!_startTimer) return;
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            Debug.Log($"Time is over {_currentTime}");
            _startTimer = false;
            _isTimeOut = true;
        }
    }
}