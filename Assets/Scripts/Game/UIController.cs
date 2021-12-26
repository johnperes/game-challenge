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
    [Tooltip("Indicates the moves left for player 1")]
    TMP_Text player1MoveCounterLabel;

    [SerializeField]
    [Tooltip("Indicates the moves left for player 2")]
    TMP_Text player2MoveCounterLabel;

    [SerializeField]
    [Tooltip("End game screen")]
    GameObject endGameScreen;

    [SerializeField]
    [Tooltip("End game screen announcement")]
    TMP_Text endGameScreenAnnouncementLabel;

    [Header("Player's Panel")]

    [SerializeField]
    [Tooltip("Player 1 UI Panel")]
    Image player1Panel;

    [SerializeField]
    [Tooltip("Player 2 UI Panel")]
    Image player2Panel;

    [Header("Player's Panel Labels")]

    [SerializeField]
    [Tooltip("Player 1 Health")]
    TMP_Text player1HealthLabel;

    [SerializeField]
    [Tooltip("Player 2 Health")]
    TMP_Text player2HealthLabel;

    [SerializeField]
    [Tooltip("Player 1 Attack")]
    TMP_Text player1AttackLabel;

    [SerializeField]
    [Tooltip("Player 2 Attack")]
    TMP_Text player2AttackLabel;

    [SerializeField]
    [Tooltip("Player 1 Dice")]
    TMP_Text player1DiceLabel;

    [SerializeField]
    [Tooltip("Player 2 Dice")]
    TMP_Text player2DiceLabel;

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

    public void UpdatePlayerPanel(PlayerAttributes p1Attr, PlayerAttributes p2Attr) {
        player1HealthLabel.text = p1Attr.GetHealth().ToString();
        player1AttackLabel.text = p1Attr.GetAttack().ToString();
        player1DiceLabel.text = p1Attr.GetDiceCount().ToString();
        player2HealthLabel.text = p2Attr.GetHealth().ToString();
        player2AttackLabel.text = p2Attr.GetAttack().ToString();
        player2DiceLabel.text = p2Attr.GetDiceCount().ToString();
    }

    // Displays the end game screen and result
    public void DisplayEndGameScreen(GameController.PlayerIndex winner) {
        if (winner == GameController.PlayerIndex.Player1) {
            endGameScreenAnnouncementLabel.text = "Player 1 wins the game!";
        } else {
            endGameScreenAnnouncementLabel.text = "Player 2 wins the game!";
        }
        endGameScreen.SetActive(true);
    }
}
