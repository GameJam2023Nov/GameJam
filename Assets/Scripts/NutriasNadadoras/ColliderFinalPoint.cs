using UnityEngine;

public class ColliderFinalPoint : MonoBehaviour
{
    private int _countOfNutriasIntoCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _countOfNutriasIntoCollider++;
        }
    }
    
    public int CountOfNutriasIntoCollider => _countOfNutriasIntoCollider;
}
