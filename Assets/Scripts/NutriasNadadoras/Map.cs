using UnityEngine;

internal class Map : MonoBehaviour
{
    [SerializeField] private GameObject petroleo;
    [SerializeField] private float speed;
    
    private bool _canStart;
    public void Configure()
    {
        _canStart = false;
    }

    public void StartGame()
    {
        _canStart = true;
    }

    private void Update()
    {
        if(!_canStart) return;
        petroleo.transform.localPosition += petroleo.transform.forward * (speed * Time.deltaTime);
    }
}