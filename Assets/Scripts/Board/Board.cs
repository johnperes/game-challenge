using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public static Board Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    [Tooltip("Square Prefab")]
    GameObject squarePrefab;

    [SerializeField]
    [Tooltip("Number of squares in the board")]
    int cellSize = 1;

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
        return boardCache[Random.Range(0, cellSize)][Random.Range(0, cellSize)];
    }

    // Get a specific square by a coordinate
    public GameObject GetSquare(Vector2 squarePosition) {
        return boardCache[(int) squarePosition.x][(int) squarePosition.y];
    }

    // Get the square neighborhood
    GameObject[] GetNeighborhoods(Vector2 squarePosition) {
        List<GameObject> neighborhoods = new List<GameObject>();
        if (squarePosition.x > 0) {
            neighborhoods.Add(GetSquare(new Vector2(squarePosition.x - 1, squarePosition.y)));
        }
        if (squarePosition.x < cellSize - 1) {
            neighborhoods.Add(GetSquare(new Vector2(squarePosition.x + 1, squarePosition.y)));
        }
        if (squarePosition.y > 0) {
            neighborhoods.Add(GetSquare(new Vector2(squarePosition.x, squarePosition.y - 1)));
        }
        if (squarePosition.y < cellSize - 1) {
            neighborhoods.Add(GetSquare(new Vector2(squarePosition.x, squarePosition.y + 1)));
        }
        return neighborhoods.ToArray();
    }
    
    // Updates the neighborhoods squares click feedback
    public void HighlightSquares(Vector2 position, bool enable) {
        GameObject[] neighborhoods = GetNeighborhoods(position);
        for (int x = 0; x < neighborhoods.Length; x++) {
            if (enable) {
                neighborhoods[x].GetComponent<Square>().EnableClick();
            } else {
                neighborhoods[x].GetComponent<Square>().DisableClick();
            }
        }
    }
}
