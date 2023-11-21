using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private bool isStartRight;

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isStartRight)
        {
            rb.velocity = Vector3.right + rb.velocity * (speed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.left + rb.velocity * (speed * Time.deltaTime);
        }
    }
}
