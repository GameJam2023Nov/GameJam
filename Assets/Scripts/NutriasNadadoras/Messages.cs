using System.Collections;
using SL;
using TMPro;
using UnityEngine;

public class Messages : ServiceCustom, IMessages
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    protected override bool Validation()
    {
        return FindObjectsOfType<Messages>().Length > 1;
    }

    protected override void RegisterService()
    {
        panel.SetActive(false);
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

    private IEnumerator ShowMessageC(string message, float time)
    {
        messageText.text = message;
        panel.SetActive(true);
        yield return new WaitForSeconds(time);
        panel.SetActive(false);
    }
}