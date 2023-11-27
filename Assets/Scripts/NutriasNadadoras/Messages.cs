using System;
using System.Collections;
using SL;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Messages : ServiceCustom, IMessages
{
    [SerializeField] private GameObject panel, panelToRestartOrGoToHome;
    [SerializeField] private TextMeshProUGUI messageText, messageTextToRestartOrGoToHome;
    [SerializeField] private Button restartButton, goToHomeButton;
    protected override bool Validation()
    {
        return FindObjectsOfType<Messages>().Length > 1;
    }

    protected override void RegisterService()
    {
        panel.SetActive(false);
        panelToRestartOrGoToHome.SetActive(false);
        ServiceLocator.Instance.RegisterService<IMessages>(this);
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IMessages>();
    }

    public void ShowMessage(string message, float delay)
    {
        StartCoroutine(ShowMessageC(message, delay));
    }

    public void ShowRestartOrGoToHome(string title, Action action, Action action1)
    {
        messageTextToRestartOrGoToHome.text = title;
        panelToRestartOrGoToHome.SetActive(true);
        restartButton.onClick.AddListener(() =>
        {
            action?.Invoke();
            panelToRestartOrGoToHome.SetActive(false);
        });
        goToHomeButton.onClick.AddListener(() =>
        {
            action1?.Invoke();
            panelToRestartOrGoToHome.SetActive(false);
        });
    }

    private IEnumerator ShowMessageC(string message, float time)
    {
        messageText.text = message;
        panel.SetActive(true);
        yield return new WaitForSeconds(time);
        panel.SetActive(false);
    }
}