using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script to the camera to follow the players")]
    CameraFollow cameraFollow;

    [SerializeField]
    [Tooltip("Player prefab")]
    GameObject playerPrefab;

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
                        player1.Move(square);
                    }
                }
            }
        }
        #endregion
    }

    // Spawn the players in the board
    void SpawnPlayers() {
        GameObject go = Instantiate(playerPrefab);
        player1 = go.GetComponent<Player>();
        player1.Move(Board.Instance.GetRandomSquare().GetComponent<Square>());
        cameraFollow.SetTarget(go);
    }
}
