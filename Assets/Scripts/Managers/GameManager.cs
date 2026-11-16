using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private InputManager inputManager;
    private GameState gameState;
    private float gameStartDelay;

    void Start()
    {
        gameStartDelay = 3f;
        gameState = GameState.Start;
        Invoke("StartGame", gameStartDelay);
    }

    #region ----- Functions -----

    void StartGame()
    {
        gameState = GameState.Playing;
    }

    /*-------------------------*/

    enum GameState
    {
        Start,
        Playing,
        Stop,
        Finish
    }

    #endregion
}
