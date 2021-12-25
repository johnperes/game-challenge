using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Current health
    int health;

    [SerializeField]
    [Tooltip("Player maximum health")]
    int maxHealth;

    [SerializeField]
    [Tooltip("Player attack damage")]
    int attack;
    int attackBuff = 0;

    [SerializeField]
    [Tooltip("Player dice count")]
    int diceCount = 3;
    int diceCountBuff = 0;
    int dicesUsed = 0;

    [SerializeField]
    [Tooltip("Player move count")]
    int moveCount = 3;
    int moveCountBuff = 0;
    int movesUsed = 0;

    // Gets current health
    public int GetHealth()
    {
        return health;
    }

    // Gets current health
    public int GetAttack()
    {
        return attack + attackBuff;
    }

    // Gets current dice count
    public int GetDiceCount()
    {
        return diceCount + diceCountBuff - dicesUsed;
    }

    // Gets current move count
    public int GetMoveCount()
    {
        return moveCount + moveCountBuff - movesUsed;
    }

    // Damages the player
    public void GetHit(int value)
    {
        health -= value;
        if (health < 0)
        {
            health = 0;
        }
    }

    // Decreases current turn dice count
    public void DecreaseDiceCountForTurn()
    {
        dicesUsed++;
    }

    // Decreases current turn move count
    public void DecreaseMoveCountForTurn()
    {
        movesUsed++;
    }

    // Adds attack for the rest of the turn
    public void AttackBuff(int value)
    {
        attackBuff += value;
    }

    // Adds extra dices for the rest of the turn
    public void DiceBuff(int value)
    {
        diceCountBuff += value;
    }

    // Adds extra movement for the rest of the turn
    public void MovementBuff(int value)
    {
        moveCountBuff += value;
    }

    // Heals the player
    public void Heal(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            maxHealth = health;
        }
    }

    // Resets counters for the turn end
    public void ResetTurn()
    {
        attackBuff = 0;
        diceCountBuff = 0;
        moveCountBuff = 0;
        dicesUsed = 0;
        movesUsed = 0;
    }
}
