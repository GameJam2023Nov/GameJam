using System;
using SL;
using UnityEngine;
using UnityEngine.UI;

internal class StageButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private StagesInfo stagesInfo;
    public Button Button => button;
    public StagesInfo StagesInfo => stagesInfo;

    private void Reset()
    {
        button = GetComponent<Button>();
    }
}