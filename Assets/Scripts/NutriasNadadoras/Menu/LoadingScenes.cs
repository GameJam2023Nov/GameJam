using SL;
using UnityEngine;

public class LoadingScenes : MonoBehaviour
{
    [SerializeField] private StageButton[] stageButtons;

    private void Start()
    {
        foreach (var stageButton in stageButtons)
        {
            stageButton.Button.onClick.AddListener(() => ServiceLocator.Instance.GetService<IRulesOfGameService>().LoadScene(stageButton.StagesInfo));
        }
    }
}
