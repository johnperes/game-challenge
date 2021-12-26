using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Event to be executed when the player moves
    public event Action<int> ActionMove;

    [SerializeField]
    [Tooltip("Controls all players attributes")]
    public PlayerAttributes attributes;

    [SerializeField]
    [Tooltip("Controls all players animations")]
    PlayerAnimation _animation;

    // Current player square
    Square currentSquare;

    // Player walk speed
    float speed = 3f;

    // Player rotation speed
    float rotationSpeed = 35f;

    // Control of player movement
    bool isMoving = false;

    // Control the attack of the player
    bool isAttacking = false;

    // Player walk target position
    Vector3 walkTargetPosition;

    // Player rotate target position
    Vector3 rotateTargetPosition;

    // Player index, identifies if its the player 1 or 2
    GameController.PlayerIndex playerIndex;

    // Move the player to a square in the board
    public void Spawn(Square target, GameController.PlayerIndex index) {
        // Stores the player index (1, 2)
        playerIndex = index;
        // Stores the player current position
        currentSquare = target;
        // Just puts the player in the square, without animation
        transform.SetParent(currentSquare.gameObject.transform);
        transform.localPosition = Vector3.zero;
        currentSquare.AddContent(gameObject, true);
    }
    
    public void StartTurn() {
        // Reset the player attributes
        attributes.ResetTurn();
        ActionMove.Invoke(attributes.GetMoveCount());
        // Highlight the new nearby squares
        Board.Instance.HighlightSquares(currentSquare.GetCoordinates(), true);
    }

    // Continues the turn of the player
    void ContinueTurn() {
        // Decrease the number of steps
        attributes.DecreaseMoveCountForTurn();
        ActionMove.Invoke(attributes.GetMoveCount());
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
        rotateTargetPosition = walkTargetPosition;
        // Stores the player current position
        currentSquare = target;
        // Starts walk animation
        _animation.Walk();
    }

    // Attacks the other player
    void Attack() {
        // Starts attack animation
        _animation.Attack();
        // Set attack flag
        isAttacking = true;
    }

    // Ends the movement of the player
    void MoveEnd() {
        // Finish the movement
        isMoving = false;
        // Sets the player new parent and resets its local position
        transform.SetParent(currentSquare.gameObject.transform);
        transform.localPosition = Vector3.zero;
        // Pick and destroys the contents of a square
        currentSquare.DestroyContent(attributes);
        // Moves player to square
        currentSquare.AddContent(gameObject, true);
        // Updates move count
        ActionMove.Invoke(attributes.GetMoveCount());
        // Starts idle animation
        _animation.Idle();
        // Check if nearby another player and attack
        Vector3 attackPosition = Board.Instance.AttackPosition(currentSquare.GetCoordinates());
        if (attackPosition != Vector3.zero) {
            // Starts attack animation
            Attack();
            // Rotates towards target
            rotateTargetPosition = attackPosition;
        } else
        {
            StartCoroutine("CheckTurnEnd");
        }
    }

    // Attack cancel
    public void AttackCancel() {
        isAttacking = false;
        if (playerIndex == GameController.PlayerIndex.Player1) {
            BattleManager.Instance.StartBattlePhase(attributes.GetDiceCount(), 3, playerIndex);
        } else {
            BattleManager.Instance.StartBattlePhase(3, attributes.GetDiceCount(), playerIndex);
        }
        StartCoroutine("CheckTurnEnd");
    }

    // Checks the end of turn
    IEnumerator CheckTurnEnd() {
        // Waits until the battle ends
        yield return new WaitUntil(() => !BattleManager.Instance.IsBattleInProgress());
        if (attributes.GetMoveCount() > 1) {
            // Continue turn
            ContinueTurn();
        } else {
            // End the turn
            Board.Instance.HighlightSquares(currentSquare.GetCoordinates(), false);
            GameController.Instance.ChangeTurn();
        }
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
            if (Vector3.Distance(transform.position, walkTargetPosition) < .1f) {
                // Moves player to the square
                MoveEnd();
            }
        }
        if (isMoving || isAttacking) {
            RotateTowardsTarget(rotateTargetPosition);
        }
        #endregion
    }
}
