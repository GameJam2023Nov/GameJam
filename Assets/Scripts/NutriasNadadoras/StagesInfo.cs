using UnityEngine;

[CreateAssetMenu(menuName = "Stages", fileName = "StagesInfo", order = 0)]
public class StagesInfo : ScriptableObject
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private bool hasCompleted;
    
    public void CompleteStage()
    {
        hasCompleted = true;
    }
    public int SceneIndex => sceneIndex;
    public bool HasCompleted => hasCompleted;
}