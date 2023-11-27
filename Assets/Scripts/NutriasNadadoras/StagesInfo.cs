using UnityEngine;

[CreateAssetMenu(menuName = "Stages", fileName = "StagesInfo", order = 0)]
public class StagesInfo : ScriptableObject
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private bool hasCompleted;
    [SerializeField] private bool isLocked;
    
    public void CompleteStage()
    {
        hasCompleted = true;
    }

    public void ResetStage()
    {
        if (isLocked) return;
        hasCompleted = false;
    }
    public int SceneIndex => sceneIndex;
    public bool HasCompleted => hasCompleted;
    public bool IsStage => !isLocked;
}