﻿using System.Collections;
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

    bool canClick = false;

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

    // Makes the square clickable
    public void EnableClick() {
        canClick = true;
    }

    // Disables the square click
    public void DisableClick() {
        canClick = false;
    }

    // Check if the square is clickable
    public bool CanClick() {
        return canClick;
    }
}
