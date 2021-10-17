using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizDokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float squareOffset = 0.0f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 1.0f;

    private List<GameObject> gridSquares = new List<GameObject>();
    void Start()
    {
        if(gridSquare.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("This Game Object needs to have GridSquare script attached!");
        }

        CreateGrid();
        SetGridNumber();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquarePosition();
    }

    private void SpawnGridSquares()
    {
        // 0, 1, 2, 3, 4, 5, 6,
        // 7, 8, ...
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                gridSquares.Add(Instantiate(gridSquare) as GameObject);
                gridSquares[gridSquares.Count - 1].transform.parent = this.transform; // Instantiate this game object as a child of the object holding this script
                gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
            }
        }
    }

    private void SetSquarePosition()
    {
        var squareRect = gridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = squareRect.rect.width * squareRect.transform.localScale.x + squareOffset;
        offset.y = squareRect.rect.height * squareRect.transform.localScale.y + squareOffset;

        int columnNumber = 0;
        int rowNumber = 0;

        foreach(GameObject square in gridSquares)
        {
            if(columnNumber + 1 > columns)
            {
                rowNumber++;
                columnNumber = 0;
            }

            var posXOffset = offset.x * columnNumber;
            var posYOffset = offset.y * rowNumber;
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + posXOffset, startPosition.y - posYOffset);
            columnNumber++;
        }
    }

    private void SetGridNumber()
    {
        foreach(var square in gridSquares)
        {
            square.GetComponent<GridSquare>().SetNumber(Random.Range(0, 10));
        }
    }
}
