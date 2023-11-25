using SL;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesOfGameService : ServiceCustom, IRulesOfGameService
{
    private int currentStage = 1;
    public void LoadScene(StagesInfo stagesInfo)
    {
        if(currentStage < stagesInfo.SceneIndex)
        {
            //Message To User "You have not completed the previous stage"
            Debug.Log("You have not completed the previous stage");
            ServiceLocator.Instance.GetService<IMessages>().ShowMessage("You have not completed the previous stage", 2f);
            return;
        }
        if(stagesInfo.HasCompleted)
        {
            //Message To User "You have already completed this stage"
            Debug.Log("You have already completed this stage");
            ServiceLocator.Instance.GetService<IMessages>().ShowMessage("You have already completed this stage", 2f);
            return;
        }
        ServiceLocator.Instance.GetService<IFade>().In(() =>
        {
            SceneManager.LoadScene(stagesInfo.SceneIndex);
        });
    }
    
    public void CompleteStage(StagesInfo stagesInfo)
    {
        stagesInfo.CompleteStage();
        currentStage++;
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
    }
}