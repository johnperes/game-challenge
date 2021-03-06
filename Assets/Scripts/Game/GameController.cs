using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum PlayerIndex { Player1, Player2 };

    public static GameController Instance { get; private set; }
    void Awake() {
        Instance = this;
    }

    [SerializeField]
    UIController uiController;

    [SerializeField]
    [Tooltip("Script to the camera to follow the players")]
    CameraFollow cameraFollow;

    // Players reference
    Player activePlayer;

    [HideInInspector]
    public Player player1;

    [HideInInspector]
    public Player player2;

    void Start() {
        Board.Instance.CreateBoard();
        SpawnPlayers();
        SpawnCollectables();
    }

    void Update() {
        uiController.UpdatePlayerPanel(player1.GetAttributes(), player2.GetAttributes());
        #region Check Player Click
        if ( Input.GetMouseButtonDown(0) ) {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if( Physics.Raycast( ray, out hit, 100 ) ) {
                Square square = hit.transform.gameObject.GetComponent<Square>();
                if (square != null) {
                    if (square.CanClick()) {
                        activePlayer.Move(square);
                    }
                }
            }
        }
        #endregion
    }

    // Spawn the players in the board
    void SpawnPlayers() {
        // Spawn player 1
        GameObject go1 = Instantiate(Resources.Load<GameObject>(StaticData.Player1Prefab));
        player1 = go1.GetComponent<Player>();
        player1.Spawn(Board.Instance.GetRandomSquare().GetComponent<Square>(), PlayerIndex.Player1);
        player1.ActionMove += uiController.UpdateMovesLeft;
        // Spawn player 2
        GameObject go2 = Instantiate(Resources.Load<GameObject>(StaticData.Player2Prefab));
        player2 = go2.GetComponent<Player>();
        player2.Spawn(Board.Instance.GetRandomSquare().GetComponent<Square>(), PlayerIndex.Player2);
        player2.ActionMove += uiController.UpdateMovesLeft;
        // Start the turn
        ChangeTurn();
    }

    // Spawn the collectables in the board
    void SpawnCollectables() {
        Board.Instance.SpawnCollectables();
    }

    // Turn end
    public void ChangeTurn() {
        // Swaps players
        if (activePlayer == player1) {
            activePlayer = player2;
            uiController.EnablePlayer2();
        } else {
            activePlayer = player1;
            uiController.EnablePlayer1();
        }
        // Start turn of active player
        activePlayer.StartTurn();
        // Sets the new camera target
        cameraFollow.SetTarget(activePlayer.gameObject);
    }

    public void FinishGame(PlayerIndex winner) {
        uiController.DisplayEndGameScreen(winner);
    }
}
