using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    void Awake() {
        Instance = this;
    }

    [SerializeField]
    UIController uiController;

    [SerializeField]
    [Tooltip("Script to the camera to follow the players")]
    CameraFollow cameraFollow;

    [SerializeField]
    [Tooltip("Player prefab")]
    GameObject playerPrefab;

    // Players reference
    Player activePlayer;
    Player player1;
    Player player2;

    void Start() {
        Board.Instance.CreateBoard();
        SpawnPlayers();
    }

    void Update() {
        #region Check Player Click
        if( Input.GetMouseButtonDown(0) ) {
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
        GameObject go1 = Instantiate(playerPrefab);
        player1 = go1.GetComponent<Player>();
        player1.Spawn(Board.Instance.GetRandomSquare().GetComponent<Square>());
        player1.ActionMove += uiController.UpdateMovesLeft;
        // Spawn player 2
        GameObject go2 = Instantiate(playerPrefab);
        player2 = go2.GetComponent<Player>();
        player2.Spawn(Board.Instance.GetRandomSquare().GetComponent<Square>());
        player2.ActionMove += uiController.UpdateMovesLeft;
        // Start the turn
        ChangeTurn();
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
}
