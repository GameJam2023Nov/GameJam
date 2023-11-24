using UnityEngine;

public class AnimatorControllerCustom : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void Configure()
    {
        
    }
    
    public void SetVelocity(float velocity)
    {
        animator.SetFloat("SwimVelocity", velocity);
    }
    
    public void IsDead(bool isDead)
    {
        animator.SetTrigger("IsDead");
    }
    
    public void SetIntoWater(bool intoWater)
    {
        animator.SetBool("IsInWater", intoWater);
    }
}