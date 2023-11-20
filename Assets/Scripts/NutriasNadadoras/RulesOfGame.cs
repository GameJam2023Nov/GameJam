using SL;
using UnityEngine;

public class RulesOfGame : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField] private Characters characters;
    private TeaTime _beforeToStart, _start, _game, _end;
    private bool _endGame;
    private bool _isWin;
    private bool _seleccionoAlgo;
    private int _seleccionDeFinDeJuego;
    
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

    void Start()
    {
        Debug.Log("RulesOfGame");
        _beforeToStart = this.tt().Pause().Add(() =>
        {
            Debug.Log("beforeToStart");
            //Configurar todos los componentes para empezar
            map.Configure();
            characters.Configure();
        }).Add(() =>
        {
            _start.Play();
        });
        
        _start = this.tt().Pause().Add(() =>
        {
            Debug.Log("start");
            //Hacer el FadeOut de la pantalla de inicio
            ServiceLocator.Instance.GetService<IFade>().Out();
            //Mostrar mensaje de que empieza el juego
            ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Empieza el juego", 2f);
        }).Add(2f).Add(() =>
        {
            _game.Play();
        });
        _game = this.tt().Pause().Add(() =>
        {
            Debug.Log("game");
            //darle la orden a todos los componentes configurados a que empiecen
            map.StartGame();
            characters.StartGame();
        }).Wait(()=>_endGame).Add(() =>
        {
            _end.Play();
        });
        _end = this.tt().Pause().Add(() =>
        {
            Debug.Log("end");
            //evaluar si gano o perdio
            if (_isWin)
            {
                //Mostrar mensaje de que gano
                ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Ganaste", 2f);
            }
            else
            {
                //Mostrar mensaje de que perdio
                ServiceLocator.Instance.GetService<IMessages>().ShowMessage("Perdiste", 2f);
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
    }
}