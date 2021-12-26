using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Singleton
    public static BattleManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    int player1DiceCount = 3;
    int player2DiceCount = 3;

    [SerializeField]
    [Tooltip("The whole UI that contains the battle elements")]
    GameObject battleUI;

    [SerializeField]
    [Tooltip("Player 1 Dice Material")]
    Material player1DiceMaterial;

    [SerializeField]
    [Tooltip("Player 2 Dice Material")]
    Material player2DiceMaterial;

    [SerializeField]
    [Tooltip("Stores Player 1 dice rolls")]
    GameObject player1DiceContainer;

    [SerializeField]
    [Tooltip("Stores Player 2 dice rolls")]
    GameObject player2DiceContainer;

    [SerializeField]
    [Tooltip("Generic prefab to store a dice roll")]
    GameObject diceNumberIndicatorPrefab;

    [SerializeField]
    [Tooltip("Shows who wins the battle")]
    TMP_Text announcementLabel;

    [SerializeField]
    [Tooltip("The dice")]
    Dice dice;

    // Stores the players scores
    int player1Score = 0;
    int player2Score = 0;

    // Stores the player turn to roll the dices, player 1 is always the first
    bool player1Turn = true;

    // Stores if the battle is in progress
    bool battleInProgress = false;

    // Starts the battle phase
    public void StartBattlePhase(int player1DiceCountParam, int player2DiceCountParam) {
        // Resets the battle phase data
        announcementLabel.text = "";
        player1Turn = true;
        foreach (Transform child in player1DiceContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in player2DiceContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }
        battleUI.SetActive(true);
        battleInProgress = true;
        player1DiceCount = player1DiceCountParam;
        player2DiceCount = player2DiceCountParam;
        player1Score = 0;
        player2Score = 0;
        // Starts the battle
        StartCoroutine("BattleCoroutine");
    }

    // Checks if the battle has ended
    public bool IsBattleInProgress() {
        return battleInProgress;
    }

    // Do the dice roll and register the necessary data of the players
    IEnumerator BattleCoroutine()
    {
        // Changes the dice material according the active player
        if (player1Turn) {
            dice.ChangeMaterial(player1DiceMaterial);
        } else {
            dice.ChangeMaterial(player2DiceMaterial);
        }
        // Do a dice roll
        dice.Launch();
        // Waits until the dice stops
        yield return new WaitUntil(() => dice.GetValue() > 0);
        GameObject go = Instantiate(diceNumberIndicatorPrefab);
        go.GetComponent<DiceNumber>().SetValue(dice.GetValue());
        // Updates the number of dices the player can roll and register the value
        if (player1Turn) {
            player1Score += dice.GetValue();
            player1DiceCount--;
            go.transform.SetParent(player1DiceContainer.transform);
        } else {
            player2Score += dice.GetValue();
            player2DiceCount--;
            go.transform.SetParent(player2DiceContainer.transform);
        }
        // Swaps between player when one runs out of dice rolls
        if (player1DiceCount == 0) {
            player1Turn = false;
        }
        // Continue to roll until all players have remaining rolls, if not it ends the battle coroutine
        if (player1DiceCount > 0 || player2DiceCount > 0) {
            StartCoroutine("BattleCoroutine");
        } else {
            StartCoroutine("EndBattleCoroutine");
        }
    }

    // Finish the battle phase
    IEnumerator EndBattleCoroutine() {
        int attack = 0;
        // Applies the attack of the winner player to the other, and updates the announcement
        if (player1Score > player2Score) {
            attack = GameController.Instance.player1.attributes.GetAttack();
            GameController.Instance.player2.attributes.GetHit(attack);
            announcementLabel.text = "Player 1 wins!";
        } else if (player1Score < player2Score) {
            attack = GameController.Instance.player2.attributes.GetAttack();
            GameController.Instance.player1.attributes.GetHit(attack);
            announcementLabel.text = "Player 2 wins!";
        } else {
            announcementLabel.text = "Draw!";
        }
        // Waits for 2 seconds, so the players have time to read, then finish the battle phase
        yield return new WaitForSeconds(2);
        battleUI.SetActive(false);
        battleInProgress = false;
    }
}
