using UnityEngine;

public abstract class ServiceCustom : MonoBehaviour
{
    private bool canRegister;
    private void Start()
    {
        if (Validation())
        {
            Destroy(gameObject);
            return;
        }

        RegisterService();
        canRegister = true;
    }

    protected abstract bool Validation();

    protected abstract void RegisterService();
    
    protected abstract void RemoveService();

    private void OnDestroy()
    {
        if (canRegister)
        {
            RemoveService();
        }
    }
}