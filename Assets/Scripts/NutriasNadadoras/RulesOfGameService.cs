using SL;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesOfGameService : ServiceCustom, IRulesOfGameService
{
    [SerializeField] private StagesInfo[] _stagesInfos;
    private int _currentStageIndex;
    public void LoadScene(StagesInfo stagesInfo)
    {
        _currentStageIndex = 1;
        //get last stage completed
        foreach (var stageInfo in _stagesInfos)
        {
            if(stageInfo.HasCompleted)
            {
                _currentStageIndex++;
            }
        }
        if (!stagesInfo.IsStage)
        {
            ServiceLocator.Instance.GetService<IFade>().In(() =>
            {
                SceneManager.LoadScene(stagesInfo.SceneIndex);
            });
            return;
        }
        if(stagesInfo.HasCompleted)
        {
            ServiceLocator.Instance.GetService<IMessages>().ShowMessage("You have already completed this stage", 2f);
            return;
        }
        if(_currentStageIndex == stagesInfo.SceneIndex)
        {
            ServiceLocator.Instance.GetService<IFade>().In(() =>
            {
                SceneManager.LoadScene(stagesInfo.SceneIndex);
            });
            return;
        }
        ServiceLocator.Instance.GetService<IMessages>().ShowMessage("You have not completed the previous stage", 2f);
    }
    
    public void CompleteStage(StagesInfo stagesInfo)
    {
        stagesInfo.CompleteStage();
    }

    public bool HasCompletedAllLevels()
    {
        foreach (var stagesInfo in _stagesInfos)
        {
            if (!stagesInfo.HasCompleted && stagesInfo.IsStage)
            {
                return false;
            }
        }
        return true;
    }

    protected override bool Validation()
    {
        return FindObjectsOfType<RulesOfGameService>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<IRulesOfGameService>(this);
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IRulesOfGameService>();
        foreach (var stagesInfo in _stagesInfos)
        {
            stagesInfo.ResetStage();
        }
    }
}