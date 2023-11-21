using System.Collections.Generic;
using UnityEngine;

internal class Characters : MonoBehaviour
{
    [SerializeField] private GameObject nutriaPrefab;
    [SerializeField] private GameObject pointToSpawn;
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
                validationOfVictory.AddFinalDestiny(tierraFirme);
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
}