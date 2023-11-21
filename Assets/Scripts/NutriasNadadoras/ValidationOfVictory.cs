using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SL;
using UnityEngine;

public class ValidationOfVictory : MonoBehaviour
{
    private readonly List<GameObject> _nutrias = new();
    private bool _canGoTo;
    private int state = 0;

    public void SendNutriaToDestiny(Nutria nutria, Vector3 destiny, Vector3 anchorPoint)
    {
        StartCoroutine(SendBetweenPoints(nutria, destiny, anchorPoint));
    }

    private IEnumerator SendBetweenPoints(Nutria nutria, Vector3 destiny, Vector3 anchorPoint)
    {
        foreach (var nutria1 in _nutrias)
        {
            nutria.GoTo(nutria1.transform.position, anchorPoint);
            yield return new WaitForSeconds(0.1f);
            while (!nutria.ArriveToDestiny())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (!_canGoTo)
        {
            nutria.GoTo(destiny, anchorPoint);
            _nutrias.Add(nutria.gameObject);
        }
        else
        {
            //remove nutrua to list
            //_nutrias.Remove(nutria.gameObject);
            nutria.Deleted();
            if(_nutrias.Where(o => o.GetComponent<Nutria>().WasDeleted()).ToList().Count <= 0)
            {
                state++;
                ServiceLocator.Instance.GetService<IRulesOfGame>().Win();
            }
        }
    }

    public void AddFinalDestiny(GameObject tierraFirme)
    {
        if(_canGoTo) return;
        _nutrias.Add(tierraFirme);
        _canGoTo = true;
        state++;
    }
}