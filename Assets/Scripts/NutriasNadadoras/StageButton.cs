using System;
using SL;
using UnityEngine;
using UnityEngine.UI;

internal class StageButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private StagesInfo stagesInfo;
    [SerializeField] private Image imageToComplete;
    public Button Button => button;
    public StagesInfo StagesInfo => stagesInfo;

    private void Start()
    {
        imageToComplete.enabled = false;
        if(stagesInfo.HasCompleted)
        {
            Completed();
        }
    }

    private void Reset()
    {
        button = GetComponent<Button>();
    }

    private void Completed()
    {
        imageToComplete.enabled = true;
    }
}