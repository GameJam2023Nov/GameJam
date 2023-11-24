using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

internal class Characters : MonoBehaviour
{
    public Action onCanRun;
    [SerializeField] private GameObject nutriaPrefab;
    [SerializeField] private GameObject pointToSpawn, finalEarth;
    [SerializeField] private int countOfNutrias;
    [SerializeField] private SelectorOfNutria selectorOfNutrias;
    [SerializeField] private float radiusOfPosition;
    [SerializeField] private ValidationOfVictory validationOfVictory;
    [SerializeField] private ValidatorOfClick validatorOfClick;
    
    private List<Nutria> nutrias = new();
    public void Configure()
    {
        for (var i = 0; i < countOfNutrias; i++)
        {
            //get a random position around the pointToSpawn
            var position = pointToSpawn.transform.position + Random.insideUnitSphere * radiusOfPosition;
            position.y = pointToSpawn.transform.position.y;
            var nutria = Instantiate(nutriaPrefab, position, Quaternion.identity).GetComponent<Nutria>();
            nutria.Configure();
            nutria.onTouchFinalEarth += tierraFirme =>
            {
                //Can select the final earth
                Debug.Log("Can select the final earth");
                onCanRun?.Invoke();
                validationOfVictory.AddFinalDestiny(finalEarth);
                validatorOfClick.CanSelectFinalEarth();
            };
            nutrias.Add(nutria);
        }
        selectorOfNutrias.Configure();
    }

    public void StartGame()
    {
        foreach (var nutria in nutrias)
        {
            nutria.StartIdle();
        }
    }

    public List<Nutria> GetNutrias()
    {
        return nutrias;
    }
}