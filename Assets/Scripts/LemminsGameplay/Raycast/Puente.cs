using UnityEngine;

internal class Puente : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    public void GoTo(float distance)
    {
        cube.transform.localScale = new Vector3(distance, 1, 1);
        cube.transform.localPosition = new Vector3(distance / 2, 0, 0);
    }
}