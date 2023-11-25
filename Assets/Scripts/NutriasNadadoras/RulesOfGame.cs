using System.Collections;
using SL;
using UnityEngine;

public class RulesOfGame : ServiceCustom, IRulesOfGame
{
    [SerializeField] private Map map;
    [SerializeField] private Characters characters;
    [SerializeField] private ColliderFinalPoint colliderFinalPoint;
    [SerializeField] private Temporizador temporizador;
    [SerializeField] private StagesInfo stagesInfo;
    private TeaTime _beforeToStart, _start, _game, _runNutria, _end;
    private bool _endGame;
    private bool _isWin;
    private bool _seleccionoAlgo;
    private int _seleccionDeFinDeJuego;
    private bool _finishedFade;
    private bool _canRun;
    
    public bool EndGame => _endGame;
    public bool IsWin => _isWin;
    public bool SeleccionoAlgo => _seleccionoAlgo;
    public int SeleccionDeFinDeJuego => _seleccionDeFinDeJuego;
    
    public void SetEndGame(bool endGame)
    {
        _endGame = endGame;
    }
    
    public void SetIsWin(bool isWin)
    {
        _isWin = isWin;
    }
    
    public void SetSeleccionoAlgo(bool seleccionoAlgo)
    {
        _seleccionoAlgo = seleccionoAlgo;
    }
    
    public void SetSeleccionDeFinDeJuego(int seleccionDeFinDeJuego)
    {
        _seleccionDeFinDeJuego = seleccionDeFinDeJuego;
    }

    protected override void CustomStart()
    {
        base.CustomStart();
        Debug.Log("RulesOfGame");
        _beforeToStart = this.tt().Pause().Add(() =>
        {
            Debug.Log("beforeToStart");
            //Configurar todos los componentes para empezar
            map.Configure();
            characters.Configure();
        }).Add(5f).Add(() =>
        {
            _start.Play();
        });
        
        _start = this.tt().Pause().Add(() =>
        {
            Debug.Log("start");
            //Hacer el FadeOut de la pantalla de inicio
            ServiceLocator.Instance.GetService<IFade>().Out(() =>
            {
                _finishedFade = true;
            });
            //Mostrar mensaje de que empieza el juego
            ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Empieza el juego", 2f);
        }).Wait(()=> _finishedFade).Add(() =>
        {
            _game.Play();
        });
        _game = this.tt().Pause().Add(() =>
        {
            Debug.Log("game");
            //darle la orden a todos los componentes configurados a que empiecen
            map.StartGame();
            characters.StartGame();
            temporizador.Configure();
        }).Wait(()=>_canRun || temporizador.IsTimeOut).Add(() =>
        {
            if (temporizador.IsTimeOut)
            {
                _isWin = false;
                _end.Play();
                return;
            }
            _runNutria.Play();
        });
        _runNutria = this.tt().Pause().Add(() =>
        {
            ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Corre con las nutrias al otro lado", 2f);
        }).Wait(()=> colliderFinalPoint.CountOfNutriasIntoCollider >= characters.GetNutrias().Count || temporizador.IsTimeOut, 0.1f).Add(() =>
        {
            _isWin = !temporizador.IsTimeOut;
            _end.Play();
        });
        _end = this.tt().Pause().Add(() =>
        {
            Debug.Log("end");
            //evaluar si gano o perdio
            if (_isWin)
            {
                //Mostrar mensaje de que gano
                ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Ganaste", 20f);
                ServiceLocator.Instance.GetService<IRulesOfGameService>().CompleteStage(stagesInfo);
            }
            else
            {
                //Mostrar mensaje de que perdio
                ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Perdiste", 20f);
            }
        }).Wait(()=>_seleccionoAlgo).Add(()=>
        {
            //mandarlo a la pantalla de inicio
            //reiniciar el nivel
            switch (_seleccionDeFinDeJuego)
            {
                case 0:
                    //reiniciar el nivel
                    break;
                case 1:
                    //mandarlo a la pantalla de inicio
                    break;                
            }
        });
        _beforeToStart.Play();
        characters.onCanRun += () =>
        {
            Debug.Log("onCanRun");
            _canRun = true;
        };
        Debug.Log("RegisterService");
    }

    protected override bool Validation()
    {
        return FindObjectsOfType<RulesOfGame>().Length > 1;
    }

    protected override void RegisterService()
    {
        ServiceLocator.Instance.RegisterService<IRulesOfGame>(this);
    }

    protected override void RemoveService()
    {
        ServiceLocator.Instance.RemoveService<IRulesOfGame>();
    }

    public void Win()
    {
        _endGame = true;
        _isWin = true;
    }
}