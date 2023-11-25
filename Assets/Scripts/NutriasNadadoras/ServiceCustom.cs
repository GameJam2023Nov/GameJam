using UnityEngine;

public abstract class ServiceCustom : MonoBehaviour
{
    private bool canRegister;

    public virtual void Start()
    {
        if (Validation())
        {
            Destroy(gameObject);
            return;
        }

        RegisterService();
        canRegister = true;
        DontDestroyOnLoad(gameObject);
        CustomStart();
    }

    protected virtual void CustomStart()
    {
        
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