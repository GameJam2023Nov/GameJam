using UnityEngine;

internal class Map : MonoBehaviour
{
    [SerializeField] private GameObject petroleo, pointToStart, pointToEnd;
    [SerializeField] private float timeToComplete;
    [SerializeField] private Material waterMaterial;
    private float _delta;
    
    private bool _canStart;
    public void Configure()
    {
        _canStart = false;
        waterMaterial.SetFloat("_Oil", 0);
    }

    public void StartGame()
    {
        _canStart = true;
    }

    private void Update()
    {
        if(!_canStart) return;
        //Move petroleo pointToStart to pointToEnd in timeToComplete and calc porcentage
        _delta += Time.deltaTime;
        petroleo.transform.position = Vector3.Lerp(pointToStart.transform.position, pointToEnd.transform.position, _delta / timeToComplete);
        //calc porcentage
        var porcentage = _delta / timeToComplete;
        waterMaterial.SetFloat("_Oil", porcentage);
        Debug.Log(porcentage);
        //if petroleo pointToStart is in pointToEnd, end game
        if (_delta >= timeToComplete)
        {
            _canStart = false;
            _delta = 0;
            waterMaterial.SetFloat("_Oil", 1);
        }
    }
}