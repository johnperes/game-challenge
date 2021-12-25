using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Color used to indicate the player 1 turn")]
    Color player1EnabledColor;

    [SerializeField]
    [Tooltip("Color used to indicate the player 2 turn")]
    Color player2EnabledColor;

    [SerializeField]
    [Tooltip("Color used to indicate that its not the player's turn")]
    Color disabledColor;

    [SerializeField]
    [Tooltip("Player 1 UI Panel")]
    Image player1Panel;

    [SerializeField]
    [Tooltip("Player 2 UI Panel")]
    Image player2Panel;

    [SerializeField]
    [Tooltip("Indicates the moves left for player 1")]
    TMP_Text player1MoveCounterLabel;

    [SerializeField]
    [Tooltip("Indicates the moves left for player 2")]
    TMP_Text player2MoveCounterLabel;

    // Enables player 1 turn in GUI, then disables the player 2 indicators
    public void EnablePlayer1() {
        player1Panel.color = player1EnabledColor;
        player2Panel.color = disabledColor;
        player1MoveCounterLabel.transform.parent.gameObject.SetActive(true);
        player2MoveCounterLabel.transform.parent.gameObject.SetActive(false);
    }

    // Enables player 2 turn in GUI, then disables the player 1 indicators
    public void EnablePlayer2() {
        player2Panel.color = player2EnabledColor;
        player1Panel.color = disabledColor;
        player1MoveCounterLabel.transform.parent.gameObject.SetActive(false);
        player2MoveCounterLabel.transform.parent.gameObject.SetActive(true);
    }

    // Updates the labels with the moves left for the players
    public void UpdateMovesLeft(int value) {
        player1MoveCounterLabel.text = value.ToString();
        player2MoveCounterLabel.text = value.ToString();
    }
}
