using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public enum MaterialType { White, Black, Red };

    Vector2 coordinates;
    
    [SerializeField]
    [Tooltip("Neutral square material")]
    Material SquareWhite;

    [SerializeField]
    [Tooltip("Contrast square material")]
    Material SquareBlack;

    [SerializeField]
    [Tooltip("Game Object with the movement indicator")]
    GameObject MovementIndicator;

    bool canClick = false;
    GameObject content;
    bool isOccupied = false;
    bool hasPlayer = false;

    // Set the square coordinates
    public void SetCoordinates(Vector2 coords) {
        coordinates = coords;
    }

    // Get the square coordinates
    public Vector2 GetCoordinates() {
        return coordinates;
    }

    // Set the square material
    public void SetMaterial(MaterialType material) {
        switch (material) {
            case MaterialType.Black:
                GetComponent<MeshRenderer>().material = SquareBlack;
            break;
            case MaterialType.White:
                GetComponent<MeshRenderer>().material = SquareWhite;
            break;
        }
    }

    // Adds the square content
    public void AddContent(GameObject go, bool player) {
        content = go;
        isOccupied = true;
        hasPlayer = player;
    }

    // Removes the square content
    public void RemoveContent() {
        content = null;
        isOccupied = false;
        hasPlayer = false;
    }

    // Removes the square content
    public void DestroyContent(PlayerAttributes attributes) {
        if (content != null)
        {
            Collectable collectable = content.GetComponentInChildren<Collectable>();
            if (collectable != null)
            {
                // Pick the collectable
                collectable.Pick(attributes);
                // Decreases the number of collectables available
                Board.Instance.DecreaseCollectableNumber();
            }
            Destroy(content);
        }
        RemoveContent();
    }

    // Check if has content
    public bool HasContent() {
        return isOccupied;
    }

    // Check if has player
    public bool HasPlayer() {
        return hasPlayer;
    }
    
    // Makes the square clickable
    public void EnableMoveClick() {
        canClick = true;
        MovementIndicator.SetActive(true);
    }

    // Disables the square click
    public void DisableMoveClick() {
        canClick = false;
        MovementIndicator.SetActive(false);
    }

    // Check if the square is clickable
    public bool CanClick() {
        return canClick;
    }
}
