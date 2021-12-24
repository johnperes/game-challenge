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

    // Damages the player
    public void GetHit(int damage) {
        
    }
}
