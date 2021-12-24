using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Current player square
    Square currentSquare;

    // Move the player to a square in the board
    public void Move(Square target)
    {
        // Disable the current highlight of the squares
        if (currentSquare != null) {
            Board.Instance.HighlightSquares(currentSquare.GetCoordinates(), false);
        }
        // Sets the player new parent and resets its local position
        transform.SetParent(target.gameObject.transform);
        transform.localPosition = Vector3.zero;
        // Highlight the new nearby squares
        Board.Instance.HighlightSquares(target.GetCoordinates(), true);
        // Stores the player current position
        currentSquare = target;
    }
}
