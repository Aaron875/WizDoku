using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    //Fields
    //Public
    public GameObject[] tiles;
    public Sprite Tile1;
    public Sprite Tile2;
    public Sprite Tile3;
    public Sprite Tile4;
    public Sprite Tile5;
    public Sprite Tile6;
    public Sprite Tile7;
    public Sprite Tile8;
    public Sprite Tile9;

    //Private
    private int[,] tileMap = new int[9, 9];
    // Tile placement follows the following coordinate system:
    // 0 1 2 3 4 5 6 7 8 9 x
    // 1
    // 2
    // 3
    // 4
    // 5
    // 6
    // 7
    // 8
    // 9
    // y
    private int currentLevel = 1;

    private GameObject selectedTile = null;
    private SpriteRenderer selectedRenderer = null;

    private GameObject previousTile = null;
    private SpriteRenderer previousRenderer = null;

    private SpriteRenderer[] renderers;

    private void Start()
    {
        SetupTiles(currentLevel);

        //selectedTile = tiles[0];

        //renderers = new SpriteRenderer[81];
        //for(int i = 0; i < 81; i++)
        //{
        //    renderers[i] = tiles[i].GetComponent<SpriteRenderer>();
        //}

        //selectedRenderer = renderers[0];
    }

    //Game Loop
    void Update()
    {

    }

    //Helper Methods
    public void SetupTiles(int level)
    {
        //Black = 1
        //DarkBlue = 2
        //Green = 3
        //Blue = 4
        //Orange = 5
        //Purple = 6
        //Red = 7
        //White = 8
        //Yellow = 9
        ResetTiles(); //initialize all values to 0
        //Hardcode each game's starting values
        switch (level)
        {
            case 1:
                #region LEVEL 1
                //Row 1
                tileMap[1, 0] = 4;
                tileMap[2, 0] = 2;
                tileMap[5, 0] = 9;
                tileMap[6, 0] = 4;
                //Row 2
                tileMap[2, 1] = 8;
                tileMap[3, 1] = 2;
                tileMap[4, 1] = 6;
                tileMap[6, 1] = 9;
                //Row 3
                tileMap[1, 2] = 1;
                tileMap[5, 2] = 4;
                tileMap[7, 2] = 6;
                tileMap[8, 2] = 7;
                //Row 4
                tileMap[0, 3] = 4;
                tileMap[1, 3] = 5;
                tileMap[5, 3] = 3;
                //Row 5
                tileMap[3, 4] = 1;
                tileMap[4, 4] = 7;
                tileMap[6, 4] = 5;
                tileMap[7, 4] = 4;
                //Row 6
                tileMap[1, 5] = 8;
                tileMap[2, 5] = 9;
                tileMap[3, 5] = 5;
                tileMap[8, 5] = 3;
                //Row 7
                tileMap[1, 6] = 9;
                tileMap[2, 6] = 3;
                tileMap[4, 6] = 1;
                tileMap[7, 6] = 8;
                //Row 8
                tileMap[0, 7] = 8;
                tileMap[2, 7] = 1;
                tileMap[6, 7] = 4;
                //Row 9
                tileMap[3, 8] = 9;
                tileMap[5, 8] = 6;
                tileMap[7, 8] = 1;
                tileMap[8, 8] = 2;
                #endregion
                break;
        }
        //Change tiles as needed
        int[] convertedTiles = ConvertTileList();
        for (int i = 0; i < 81; i++)
        {
            switch (convertedTiles[i])
            {
                case 1:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile1;
                    break;
                case 2:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile2;
                    break;
                case 3:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile3;
                    break;
                case 4:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile4;
                    break;
                case 5:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile5;
                    break;
                case 6:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile6;
                    break;
                case 7:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile7;
                    break;
                case 8:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile8;
                    break;
                case 9:
                    tiles[i].GetComponent<SpriteRenderer>().sprite = Tile9;
                    break;
                default:
                    break;
            }
        }
    }

    void NextLevel()
    {
        currentLevel++;
        ResetTiles();
    }

    bool CheckSquare(int input)
    {
        return true;
    }

    bool CheckRowAndColumn(int input)
    {
        return true;
    }

    //updates the TileMap
    void ChangeTile(int x, int y)
    {

    }

    void AITurn()
    {

    }

    //Parse data from 2d array into 1d array
    int[] ConvertTileList()
    {
        int[] convertedList = new int[81];
        int n = 0;

        for (int y = 0, yLength = tileMap.GetLength(1); y < yLength; y++)
        {
            for (int x = 0, xLength = tileMap.GetLength(0); x < xLength; x++)
            {
                convertedList[n] = tileMap[x, y];
                n++;
            }
        }
        return convertedList;
    }

    //Set all values in the array equal to 0 
    public void ResetTiles()
    {
        //(xLength and yLength variables used to prevent loop from calling GetLength function repeatedly)
        for (int y = 0, yLength = tileMap.GetLength(1); y < yLength; y++)
        {
            for (int x = 0, xLength = tileMap.GetLength(0); x < xLength; x++)
            {
                tileMap[x, y] = 0;
            }
        }
    }

}
