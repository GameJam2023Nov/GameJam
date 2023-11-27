public interface IRulesOfGameService
{
    void LoadScene(StagesInfo stagesInfo);
    void CompleteStage(StagesInfo stagesInfo);
    bool HasCompletedAllLevels();
}