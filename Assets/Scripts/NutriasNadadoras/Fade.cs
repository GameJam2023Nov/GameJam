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
    
    public void In()
    {
        StartFade(0, 1, delayFade);
    }

    private void StartFade(int start, int end, float delay)
    {
        StartCoroutine(FadeImage(start, end, delay));
    }

    private IEnumerator FadeImage(int start, int end, float delay)
    {
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
    }

    protected override bool Validation()
    {
        return FindObjectsOfType<Fade>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<IFade>(this);
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IFade>();
    }
}