using System.Collections;
using SL;
using UnityEngine;

public class LoadingScenes : MonoBehaviour
{
    [SerializeField] private StageButton[] stageButtons;
    [SerializeField] private StagesInfo creditScene;
    [SerializeField] private Sprite stageCompleted;

    private void Start()
    {
        foreach (var stageButton in stageButtons)
        {
            stageButton.Button.onClick.AddListener(() => ServiceLocator.Instance.GetService<IRulesOfGameService>().LoadScene(stageButton.StagesInfo));
        }
        if (ServiceLocator.Instance.GetService<IRulesOfGameService>().HasCompletedAllLevels())
        {
            //script to all changes when the game is completed
            ServiceLocator.Instance.GetService<IMessages>().ShowGameCompleted("Felicidades Completaste el juego!", () =>
            {
                //Go to Credits
                ServiceLocator.Instance.GetService<IFade>().In(() =>
                {
                    ServiceLocator.Instance.GetService<IRulesOfGameService>().LoadScene(creditScene);
                });
            });
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        ServiceLocator.Instance.GetService<IFade>().Out();
    }
}
