using System;
using System.Collections;
using SL;
using UnityEngine;
using UnityEngine.UI;

public class Fade : ServiceCustom, IFade
{
    [SerializeField] private Image panel;
    [SerializeField] private float delayFade;
    
    public void Out()
    {
        StartFade(1, 0, delayFade);
    }

    public void Out(Action action)
    {
        StartFade(1, 0, delayFade, action);
    }

    public void In()
    {
        StartFade(0, 1, delayFade);
    }

    public void In(Action action)
    {
        StartFade(0, 1, delayFade, action);
    }

    private void StartFade(int start, int end, float delay, Action callback = null)
    {
        StartCoroutine(FadeImage(start, end, delay, callback));
    }

    private IEnumerator FadeImage(int start, int end, float delay, Action callback = null)
    {
        panel.raycastTarget = true;
        var color = panel.color;
        float time = 0;

        color.a = Mathf.Lerp(start, end, time);

        while (Math.Abs(color.a - end) > 0.1f)
        {
            time += Time.deltaTime / delay;
            color.a = Mathf.Lerp(start, end, time);
            panel.color = color;
            yield return null;
        }
        color.a = end;
        panel.color = color;
        panel.raycastTarget = false;
        callback?.Invoke();
    }

    protected override bool Validation()
    {
        return FindObjectsOfType<Fade>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<IFade>(this);
        panel.raycastTarget = false;
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IFade>();
    }
}