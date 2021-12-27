using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    // Singleton
    public static Board Instance { get; private set; }
    void Awake() {
        Instance = this;
    }

    [SerializeField]
    [Tooltip("Square Prefab")]
    GameObject squarePrefab;

    [SerializeField]
    [Tooltip("Number of squares in the board")]
    int cellSize = 1;

    int totalCollectables = 0;
    int currentCollectables = 0;

    // Stores the board squares to easily access the coordinates
    GameObject[][] boardCache;

    // Creates the board according the parameters, with all the squares
    public void CreateBoard() {
        boardCache = new GameObject[cellSize][];
        for (int x = 0; x < cellSize; x++) {
            boardCache[x] = new GameObject[cellSize];
            for (int y = 0; y < cellSize; y++) {
                // Creates the square
                GameObject temp = Instantiate(squarePrefab, new Vector3(x * squarePrefab.transform.localScale.x, 0, y * squarePrefab.transform.localScale.z), Quaternion.identity);
                temp.transform.SetParent(transform);
                boardCache[x][y] = temp;
                // Set the square coordinates and material
                Square square = temp.GetComponent<Square>();
                square.SetCoordinates(new Vector2(x, y));
                if ((x + y) % 2 == 0) {
                    square.SetMaterial(Square.MaterialType.Black);
                } else {
                    square.SetMaterial(Square.MaterialType.White);
                }
            }
        }
    }

    // Get a random square, to spawn the player
    public GameObject GetRandomSquare() {
        GameObject go = boardCache[UnityEngine.Random.Range(0, cellSize)][UnityEngine.Random.Range(0, cellSize)];
        while (go.GetComponent<Square>().HasContent()) {
            go = boardCache[UnityEngine.Random.Range(0, cellSize)][UnityEngine.Random.Range(0, cellSize)];
        }
        return go;
    }

    // Get a specific square by a coordinate
    public GameObject GetSquare(Vector2 squarePosition) {
        return boardCache[(int) squarePosition.x][(int) squarePosition.y];
    }

    // Highlighted a specific square
    void HighlightSquare(Vector2 squarePosition, bool enable) {
        GameObject go = boardCache[(int)squarePosition.x][(int)squarePosition.y];
        if (enable && !go.GetComponent<Square>().HasPlayer()) {
            go.GetComponent<Square>().EnableMoveClick();
        } else {
            go.GetComponent<Square>().DisableMoveClick();
        }
    }

    // Get the square neighborhoods and check if they can be highlighted
    void HighlightNeighborhoodsSquares(Vector2 squarePosition, bool enable) {
        if (squarePosition.x > 0) {
            HighlightSquare(new Vector2(squarePosition.x - 1, squarePosition.y), enable);
        }
        if (squarePosition.x < cellSize - 1) {
            HighlightSquare(new Vector2(squarePosition.x + 1, squarePosition.y), enable);
        }
        if (squarePosition.y > 0) {
            HighlightSquare(new Vector2(squarePosition.x, squarePosition.y - 1), enable);
        }
        if (squarePosition.y < cellSize - 1) {
            HighlightSquare(new Vector2(squarePosition.x, squarePosition.y + 1), enable);
        }
    }

    // Updates the neighborhoods squares click feedback
    public void HighlightSquares(Vector2 position, bool enable) {
        HighlightNeighborhoodsSquares(position, enable);
    }

    // Check if an attack action is about to happen
    public Vector3 AttackPosition(Vector2 squarePosition) {
        List<GameObject> nearbySquares = new List<GameObject>();
        // Get all adjacent squares
        if (squarePosition.x > 0 && squarePosition.y < cellSize - 1) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x - 1, squarePosition.y + 1)));
        }
        if (squarePosition.y < cellSize - 1) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x, squarePosition.y + 1)));
        }
        if (squarePosition.x < cellSize - 1 && squarePosition.y < cellSize - 1) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x + 1, squarePosition.y + 1)));
        }
        if (squarePosition.x > 0) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x - 1, squarePosition.y)));
        }
        if (squarePosition.x < cellSize - 1) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x + 1, squarePosition.y)));
        }
        if (squarePosition.x > 0 && squarePosition.y > 0) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x - 1, squarePosition.y - 1)));
        }
        if (squarePosition.y > 0) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x, squarePosition.y - 1)));
        }
        if (squarePosition.x < cellSize - 1 && squarePosition.y > 0) {
            nearbySquares.Add(GetSquare(new Vector2(squarePosition.x + 1, squarePosition.y - 1)));
        }
        // Check if there is a player in one of them
        for (int x = 0; x < nearbySquares.Count; x++) {
            if (nearbySquares[x].GetComponent<Square>().HasPlayer()) {
                return nearbySquares[x].transform.position;
            }
        }
        return new Vector3(-100, -100, -100);
    }

    // Spawn collectables in all free spaces
    public void SpawnCollectables() {
        currentCollectables = (cellSize * cellSize) - 2;
        totalCollectables = currentCollectables;
        for (int x = 0; x < cellSize; x++) {
            for (int y = 0; y < cellSize; y++) {
                Square square = boardCache[x][y].GetComponent<Square>();
                if (!square.HasContent()) {
                    GameObject collectable;
                    Collectable.CollectableType randomCollectable = (Collectable.CollectableType) UnityEngine.Random.Range(0, (int) Collectable.CollectableType.COUNT);
                    switch (randomCollectable) {
                        case Collectable.CollectableType.AttackPower:
                            collectable = Instantiate(Resources.Load<GameObject>("Prefabs/Collectables/CollectableExtraAttack"));
                            collectable.GetComponent<Collectable>().SetType(Collectable.CollectableType.AttackPower);
                            break;
                        case Collectable.CollectableType.ExtraDice:
                            collectable = Instantiate(Resources.Load<GameObject>("Prefabs/Collectables/CollectableExtraDice"));
                            collectable.GetComponent<Collectable>().SetType(Collectable.CollectableType.ExtraDice);
                            break;
                        case Collectable.CollectableType.ExtraMove:
                            collectable = Instantiate(Resources.Load<GameObject>("Prefabs/Collectables/CollectableExtraMove"));
                            collectable.GetComponent<Collectable>().SetType(Collectable.CollectableType.ExtraMove);
                            break;
                        case Collectable.CollectableType.Heal:
                        default:
                            collectable = Instantiate(Resources.Load<GameObject>("Prefabs/Collectables/CollectableHeal"));
                            collectable.GetComponent<Collectable>().SetType(Collectable.CollectableType.Heal);
                            break;
                    }
                    square.AddContent(collectable, false);
                    collectable.transform.SetParent(square.transform);
                    collectable.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    // Decrease the number of collectables
    public void DecreaseCollectableNumber() {
        currentCollectables--;
    }

    private void Update() {
        // Spawn collectables when less than 10%
        if (currentCollectables < totalCollectables * .1f) {
            SpawnCollectables();
        }
    }
}
