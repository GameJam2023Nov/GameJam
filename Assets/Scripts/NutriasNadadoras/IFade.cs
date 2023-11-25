using System;

internal interface IFade
{
    void Out();
    void Out(Action action);
    void In();
    void In(Action action);
}