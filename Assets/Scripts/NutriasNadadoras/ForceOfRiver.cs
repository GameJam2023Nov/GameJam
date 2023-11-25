using System;
using System.Collections.Generic;
using UnityEngine;

public class ForceOfRiver : MonoBehaviour
{
    [SerializeField] private float force;
    private List<Nutria> _nutrias;

    private void Awake()
    {
        _nutrias = new List<Nutria>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nutria") && other.gameObject.CompareTag("Nutria"))
        {
            _nutrias.Add(other.gameObject.GetComponent<ColliderNutria>().Nutria);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nutria") && other.gameObject.CompareTag("Nutria"))
        {
            _nutrias.Remove(other.gameObject.GetComponent<ColliderNutria>().Nutria);
        }
    }

    private void FixedUpdate()
    {
        foreach (var nutria in _nutrias)
        {
            nutria.AddForce(Vector3.forward * (force * Time.fixedDeltaTime));
        }
    }
}
