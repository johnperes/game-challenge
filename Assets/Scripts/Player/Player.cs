using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerAnimation _animation;

    // Current player square
    Square currentSquare;

    // Player walk speed
    float speed = 3f;

    // Player rotation speed
    float rotationSpeed = 20f;

    // Control of player movement
    bool isMoving = false;

    // Player walk target position
    Vector3 walkTargetPosition;


    // Move the player to a square in the board
    public void Spawn(Square target) {
        // Stores the player current position
        currentSquare = target;
        // Just puts the player in the square, without animation
        MoveEnd();
    }

    public void StartTurn() {
        // Highlight the new nearby squares
        Board.Instance.HighlightSquares(currentSquare.GetCoordinates(), true);
    }

    // Move the player to a square in the board
    public void Move(Square target) {
        // Disable the current highlight of the squares
        if (currentSquare != null) {
            Board.Instance.HighlightSquares(currentSquare.GetCoordinates(), false);
            currentSquare.RemoveContent();
        }
        // Prepare to move
        isMoving = true;
        transform.SetParent(null);
        walkTargetPosition = target.gameObject.transform.position;
        // Stores the player current position
        currentSquare = target;
        // Starts walk animation
        _animation.Walk();
    }

    // Ends the movement of the player
    void MoveEnd() {
        // Finish the movement
        isMoving = false;
        // Sets the player new parent and resets its local position
        transform.SetParent(currentSquare.gameObject.transform);
        transform.localPosition = Vector3.zero;
        currentSquare.AddContent(gameObject, true);
        // Starts idle animation
        _animation.Idle();
    }

    // Rotates the player to the square direction
    void RotateTowardsTarget(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
    }

    void Update() {
        #region Player movement
        if (isMoving) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, walkTargetPosition, step);
            RotateTowardsTarget(walkTargetPosition);
            if (Vector3.Distance(transform.position, walkTargetPosition) < .1f) {
                // Moves player to the square
                MoveEnd();
                // Continue turn
                StartTurn();
            }
        }
        #endregion
    }
}
