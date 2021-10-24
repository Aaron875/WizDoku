using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;

enum TileColors
{
    Blank = 0,
    Black = 1,
    DarkBlue = 2,
    Green = 3,
    Blue = 4,
    Orange = 5,
    Purple = 6,
    Red = 7,
    White = 8,
    Yellow = 9
}
public class LevelManager : MonoBehaviour
{
    #region Fields

    #region Public Fields
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
    public Sprite Unselected;
    public GameObject selectedTile;
    public TMP_Text description;

    public TMP_Text playerStats;
    public TMP_Text enemyStats;

    public GameObject[] playerMinionTiles;
    public TMP_Text[] playerMinionStats;

    public GameObject[] enemyMinionTiles;
    public TMP_Text[] enemyMinionStats;

    public TMP_Text playerStatus;
    public TMP_Text enemyStatus;

    public TMP_Text turn;
    #endregion

    #region Private Fields
    private const int MAX_NUM_TILES = 81;
    private const int MAX_NUM_MINIONS = 3;

    //Private
    private int[,] tileMap = new int[9, 9];
    private int[] tileVals = new int[MAX_NUM_TILES];

    private int[,] solutionMap = new int[9, 9];
    private int[] solutionVals = new int[MAX_NUM_TILES];

    private Image[] tileImageComponents = new Image[MAX_NUM_TILES];
    private Image selectedTileImage;

    private Image[] playerMinionTileImages = new Image[MAX_NUM_MINIONS];
    private Image[] enemyMinionTileImages = new Image[MAX_NUM_MINIONS];

    private int turnNumber = 1;

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

    private Fighter playerOne;
    private Fighter AI;
    private int triesRemaining = 10;
    private int playerChoice = 0;
    private bool burningOpponent = false;
    #endregion

    #endregion

    private void Start()
    {
        //Setup Initial Board
        SetupTiles(currentLevel);
        SetupSolution(currentLevel);

        //GetComponent calls 
        for (int i = 0; i < MAX_NUM_TILES; i++)
        {
            tileImageComponents[i] = tiles[i].GetComponent<Image>();
        }
        selectedTileImage = selectedTile.GetComponent<Image>();

        for (int i = 0; i < MAX_NUM_MINIONS; i++)
        {
            playerMinionTileImages[i] = playerMinionTiles[i].GetComponent<Image>();
            enemyMinionTileImages[i] = enemyMinionTiles[i].GetComponent<Image>();
        }


        //Setup player and ai player
        playerOne = new Fighter();
        playerOne.EntityTurn = true;
        playerStats.text = "Health: " + playerOne.Health + "\n\nAttack: " + playerOne.Attack;

        AI = new Fighter();
        AI.EntityTurn = false;
        enemyStats.text = "Health: " + AI.Health + "\n\nAttack: " + AI.Attack;

        turn.text = "Turn " + turnNumber;
    }

    //Game Loop
    void Update()
    {
        turn.text = "Turn " + turnNumber;
        //Check player input only on player's turn
        if (playerOne.EntityTurn)
        {
            if (!burningOpponent)
            {
                PlayerTurn();
            }
            else
            {
                BurnOpponent();
            }
            playerStats.text = "Health: " + playerOne.Health + "\n\nAttack: " + playerOne.Attack;
            enemyStats.text = "Health: " + AI.Health + "\n\nAttack: " + AI.Attack;
            AI.CheckMinions(AI.ActiveMinions, enemyMinionTileImages, enemyMinionStats, Unselected);

            //update minion stats
            for (int j = 0; j < MAX_NUM_MINIONS; j++)
            {
                if (playerOne.ActiveMinions[j] != null)
                {
                    playerMinionStats[j].text = "Health: " + playerOne.ActiveMinions[j].Health + "\nAttack: " + playerOne.ActiveMinions[j].Attack;
                    break;
                }
                if (AI.ActiveMinions[j] != null)
                {
                    enemyMinionStats[j].text = "Health: " + AI.ActiveMinions[j].Health + "\nAttack: " + AI.ActiveMinions[j].Attack;
                    break;
                }
            }

        }
        //Else must be enemy Turn
        else if (AI.EntityTurn)
        {
            AITurn();
            playerOne.CheckMinions(playerOne.ActiveMinions, playerMinionTileImages, playerMinionStats, Unselected);

            playerStats.text = "Health: " + playerOne.Health + "\n\nAttack: " + playerOne.Attack;
            enemyStats.text = "Health: " + AI.Health + "\n\nAttack: " + AI.Attack;

            //update minion stats
            for (int j = 0; j < MAX_NUM_MINIONS; j++)
            {
                if (playerOne.ActiveMinions[j] != null)
                {
                    playerMinionStats[j].text = "Health: " + playerOne.ActiveMinions[j].Health + "\nAttack: " + playerOne.ActiveMinions[j].Attack;
                    break;
                }
                if (AI.ActiveMinions[j] != null)
                {
                    enemyMinionStats[j].text = "Health: " + AI.ActiveMinions[j].Health + "\nAttack: " + AI.ActiveMinions[j].Attack;
                    break;
                }
            }
        }
    }

    #region Helper Methods
    //Helper Methods
    #region Small Helper Methods
    //Setup initial tiles and corresponding images
    public void SetupTiles(int level)
    {
        ResetTiles(); //initialize all values to 0
        //Hardcode each game's starting values
        switch (level)
        {
            case 1:
                #region LEVEL 1
                //Row 1
                tileMap[1, 0] = (int)TileColors.Blue;
                tileMap[2, 0] = (int)TileColors.DarkBlue;
                tileMap[5, 0] = (int)TileColors.Yellow;
                tileMap[6, 0] = (int)TileColors.White;
                //Row 2
                tileMap[2, 1] = (int)TileColors.White;
                tileMap[3, 1] = (int)TileColors.DarkBlue;
                tileMap[4, 1] = (int)TileColors.Purple;
                tileMap[6, 1] = (int)TileColors.Yellow;
                //Row 3
                tileMap[1, 2] = (int)TileColors.Black;
                tileMap[5, 2] = (int)TileColors.Blue;
                tileMap[7, 2] = (int)TileColors.Purple;
                tileMap[8, 2] = (int)TileColors.Red;
                //Row 4
                tileMap[0, 3] = (int)TileColors.Blue;
                tileMap[1, 3] = (int)TileColors.Orange;
                tileMap[5, 3] = (int)TileColors.Green;
                //Row 5
                tileMap[3, 4] = (int)TileColors.Black;
                tileMap[4, 4] = (int)TileColors.Red;
                tileMap[6, 4] = (int)TileColors.Orange;
                tileMap[7, 4] = (int)TileColors.Blue;
                //Row 6
                tileMap[1, 5] = (int)TileColors.White;
                tileMap[2, 5] = (int)TileColors.Yellow;
                tileMap[3, 5] = (int)TileColors.Orange;
                tileMap[8, 5] = (int)TileColors.Green;
                //Row 7
                tileMap[1, 6] = (int)TileColors.Yellow;
                tileMap[2, 6] = (int)TileColors.Green;
                tileMap[4, 6] = (int)TileColors.Black;
                tileMap[7, 6] = (int)TileColors.White;
                //Row 8
                tileMap[0, 7] = (int)TileColors.White;
                tileMap[2, 7] = (int)TileColors.Black;
                tileMap[6, 7] = (int)TileColors.Blue;
                //Row 9
                tileMap[3, 8] = (int)TileColors.Yellow;
                tileMap[5, 8] = (int)TileColors.Purple;
                tileMap[7, 8] = (int)TileColors.Black;
                tileMap[8, 8] = (int)TileColors.DarkBlue;
                #endregion
                break;
        }
        //Change tiles as needed
        tileVals = ConvertTileList(tileMap);
        for (int i = 0; i < MAX_NUM_TILES; i++)
        {
            switch (tileVals[i])
            {
                case 1:
                    tiles[i].GetComponent<Image>().sprite = Tile1;
                    break;
                case 2:
                    tiles[i].GetComponent<Image>().sprite = Tile2;
                    break;
                case 3:
                    tiles[i].GetComponent<Image>().sprite = Tile3;
                    break;
                case 4:
                    tiles[i].GetComponent<Image>().sprite = Tile4;
                    break;
                case 5:
                    tiles[i].GetComponent<Image>().sprite = Tile5;
                    break;
                case 6:
                    tiles[i].GetComponent<Image>().sprite = Tile6;
                    break;
                case 7:
                    tiles[i].GetComponent<Image>().sprite = Tile7;
                    break;
                case 8:
                    tiles[i].GetComponent<Image>().sprite = Tile8;
                    break;
                case 9:
                    tiles[i].GetComponent<Image>().sprite = Tile9;
                    break;
                default:
                    break;
            }
        }
    }

    //Setup solution for particular level
    public void SetupSolution(int level)
    {
        switch (level)
        {
            #region Level 1 Solution
            case 1:
                //Row 1
                solutionMap[0, 0] = 6;
                solutionMap[1, 0] = 4;
                solutionMap[2, 0] = 2;
                solutionMap[3, 0] = 7;
                solutionMap[4, 0] = 5;
                solutionMap[5, 0] = 9;
                solutionMap[6, 0] = 8;
                solutionMap[7, 0] = 3;
                solutionMap[8, 0] = 1;
                //Row 2
                solutionMap[0, 1] = 7;
                solutionMap[1, 1] = 3;
                solutionMap[2, 1] = 8;
                solutionMap[3, 1] = 2;
                solutionMap[4, 1] = 6;
                solutionMap[5, 1] = 1;
                solutionMap[6, 1] = 9;
                solutionMap[7, 1] = 5;
                solutionMap[8, 1] = 4;
                //Row 3
                solutionMap[0, 2] = 9;
                solutionMap[1, 2] = 1;
                solutionMap[2, 2] = 5;
                solutionMap[3, 2] = 8;
                solutionMap[4, 2] = 3;
                solutionMap[5, 2] = 4;
                solutionMap[6, 2] = 2;
                solutionMap[7, 2] = 6;
                solutionMap[8, 2] = 7;
                //Row 4
                solutionMap[0, 3] = 4;
                solutionMap[1, 3] = 5;
                solutionMap[2, 3] = 7;
                solutionMap[3, 3] = 6;
                solutionMap[4, 3] = 9;
                solutionMap[5, 3] = 3;
                solutionMap[6, 3] = 1;
                solutionMap[7, 3] = 2;
                solutionMap[8, 3] = 8;
                //Row 5
                solutionMap[0, 4] = 3;
                solutionMap[1, 4] = 2;
                solutionMap[2, 4] = 6;
                solutionMap[3, 4] = 1;
                solutionMap[4, 4] = 7;
                solutionMap[5, 4] = 8;
                solutionMap[6, 4] = 5;
                solutionMap[7, 4] = 4;
                solutionMap[8, 4] = 9;
                //Row 6
                solutionMap[0, 5] = 1;
                solutionMap[1, 5] = 8;
                solutionMap[2, 5] = 9;
                solutionMap[3, 5] = 5;
                solutionMap[4, 5] = 4;
                solutionMap[5, 5] = 2;
                solutionMap[6, 5] = 6;
                solutionMap[7, 5] = 7;
                solutionMap[8, 5] = 3;
                //Row 7
                solutionMap[0, 6] = 2;
                solutionMap[1, 6] = 9;
                solutionMap[2, 6] = 3;
                solutionMap[3, 6] = 4;
                solutionMap[4, 6] = 1;
                solutionMap[5, 6] = 5;
                solutionMap[6, 6] = 7;
                solutionMap[7, 6] = 8;
                solutionMap[8, 6] = 6;
                //Row 8
                solutionMap[0, 7] = 8;
                solutionMap[1, 7] = 6;
                solutionMap[2, 7] = 1;
                solutionMap[3, 7] = 3;
                solutionMap[4, 7] = 2;
                solutionMap[5, 7] = 7;
                solutionMap[6, 7] = 4;
                solutionMap[7, 7] = 9;
                solutionMap[8, 7] = 5;
                //Row 9
                solutionMap[0, 8] = 5;
                solutionMap[1, 8] = 7;
                solutionMap[2, 8] = 4;
                solutionMap[3, 8] = 9;
                solutionMap[4, 8] = 8;
                solutionMap[5, 8] = 6;
                solutionMap[6, 8] = 3;
                solutionMap[7, 8] = 1;
                solutionMap[8, 8] = 2;
                break;
                #endregion
        }
        solutionVals = ConvertTileList(solutionMap);
    }

    //going to next level only resets the board, not the players' stats themselves
    void NextLevel()
    {
        currentLevel++;
        ResetTiles();
        SetupTiles(currentLevel);
        SetupSolution(currentLevel);
    }

    //Check to see if input was correct (returns true if input is correct)
    bool CorrectChoice(int tileNumber, int playerChoice)
    {
        if (solutionVals[tileNumber] == playerChoice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //updates the TileMap
    void ChangeTile(int index, int newVal)
    {
        tileVals[index] = newVal;
    }

    //Check to see if level is finished (returns true if level is finished)
    bool LevelFinished()
    {
        bool levelFinished = true;
        //Check each value of current board against solution board
        for (int i = 0; i < MAX_NUM_TILES; i++)
        {
            //If there is a tile that does not match, there are still moves to make and the game is not over
            if (tileVals[i] != solutionVals[i])
            {
                levelFinished = false;
                break;
            }
        }
        return levelFinished;
    }

    //Parses data from 2d array into 1d array. Takes in a specific 2d array
    int[] ConvertTileList(int[,] tileMapX)
    {
        int[] convertedList = new int[MAX_NUM_TILES];
        int n = 0;

        for (int y = 0, yLength = tileMapX.GetLength(1); y < yLength; y++)
        {
            for (int x = 0, xLength = tileMapX.GetLength(0); x < xLength; x++)
            {
                convertedList[n] = tileMapX[x, y];
                n++;
            }
        }
        return convertedList;
    }

    //Returns all values in all arrays to 0
    public void ResetTiles()
    {
        //(xLength and yLength variables used to prevent loop from calling GetLength function repeatedly)
        for (int y = 0, yLength = tileMap.GetLength(1); y < yLength; y++)
        {
            for (int x = 0, xLength = tileMap.GetLength(0); x < xLength; x++)
            {
                tileMap[x, y] = 0;
                solutionMap[x, y] = 0;
            }
        }
        //Reset values of 1d arrays
        for (int i = 0; i < MAX_NUM_TILES; i++)
        {
            tileVals[i] = 0;
            solutionVals[i] = 0;
        }
    }
    #endregion

    #region Major Helper Methods
    //Check for input on player's turn
    void PlayerTurn()
    {
        playerOne.EntityEffectTurns++;
        if (playerOne.EntityEffectTurns > 1)
        {
            playerOne.EntityEffect = ActiveEffect.None;
        }
        for (int i = 0; i < MAX_NUM_TILES; i++) //check which tile is selected
        {
            //Change selected tile based on 1-9 key inputs
            if (EventSystem.current.currentSelectedGameObject == tiles[i])
            {
                #region Input keys 1-9
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    tileImageComponents[i].sprite = Tile1;
                    selectedTileImage.sprite = Tile1;
                    description.text = "Shield\nEffect: Resist all damage on next turn.";
                    playerChoice = 1;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    tileImageComponents[i].sprite = Tile2;
                    selectedTileImage.sprite = Tile2;
                    description.text = "Ice\nEffect: Prevent an opponent or minion from attacking on next turn";
                    playerChoice = 2;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    tileImageComponents[i].sprite = Tile3;
                    selectedTileImage.sprite = Tile3;
                    description.text = "Sorcery\nEffect: Restore +3 health and permanently give +3 attack to all friendly minions in play.";
                    playerChoice = 3;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    tileImageComponents[i].sprite = Tile4;
                    selectedTileImage.sprite = Tile4;
                    description.text = "Wizardry\nEffect: Destroy all of opponent's currently summoned minions.";
                    playerChoice = 4;
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    tileImageComponents[i].sprite = Tile5;
                    selectedTileImage.sprite = Tile5;
                    description.text = "Fire\nEffect: Prevent opponent from placing a specific tile type until they use a potion.";
                    playerChoice = 5;
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    tileImageComponents[i].sprite = Tile6;
                    selectedTileImage.sprite = Tile6;
                    description.text = "Potion\nEffect: Restore 15 health to self, removes all negative effects.";
                    playerChoice = 6;
                }
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    tileImageComponents[i].sprite = Tile7;
                    selectedTileImage.sprite = Tile7;
                    description.text = "Demonology\nEffect: Summon a demon minion with 40 health and 5 attack.";
                    playerChoice = 7;
                }
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    tileImageComponents[i].sprite = Tile8;
                    selectedTileImage.sprite = Tile8;
                    description.text = "Sword\nEffect: Permanently raise attack by 5.";
                    playerChoice = 8;
                }
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    tileImageComponents[i].sprite = Tile9;
                    selectedTileImage.sprite = Tile9;
                    description.text = "Theurgy\nEffect: Summon a god minion with 30 health and 7 attack.";
                    playerChoice = 9;
                }
                #endregion
                #region Cancel/Change Key
                if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.W) 
                    || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) 
                    || Input.GetKeyDown(KeyCode.D)) //Will cancel on right mouse click or WASD
                {
                    selectedTileImage.sprite = Unselected;
                    tileImageComponents[i].sprite = Unselected;
                    description.text = "";
                    EventSystem.current.SetSelectedGameObject(null);
                }
                #endregion
                #region Submit Input
                if (Input.GetKeyDown(KeyCode.Return)) //Submit choice
                {
                    //Tile Phase
                    int attackModifier = 10 - triesRemaining;
                    triesRemaining--;

                    //Deselect items on GUI
                    selectedTileImage.sprite = Unselected;
                    description.text = "";
                    EventSystem.current.SetSelectedGameObject(null);

                    //Process turn outcome, reset values as needed
                    #region Correct Guess with tries left
                    if (CorrectChoice(i, playerChoice) && playerChoice != playerOne.DisabledTileType) //Player chooses correctly, has tries left and isnt blocked
                    {
                        ChangeTile(i, playerChoice); //update current tiles
                        if (LevelFinished())
                        {
                            NextLevel();
                        }

                        switch (playerChoice)
                        {
                            case 1: //Become invulnerable on next turn
                                playerOne.EntityEffect = ActiveEffect.Invulnerable;
                                playerStatus.text = "Success! Invulnerable to damage on next turn!";
                                break;

                            case 2: //Specific enemy becomes frozen on next turn
                                if (AI.NumMinions > 0) //if enemy has minions, freeze the first one possible
                                {
                                    for (int j = 0; j < MAX_NUM_MINIONS; j++)
                                    {
                                        if (AI.ActiveMinions[j] != null)
                                        {
                                            AI.ActiveMinions[j].EntityEffect = ActiveEffect.Frozen;
                                            playerStatus.text = "Success! Froze an enemy minion!";
                                            break;
                                        }
                                    }
                                }
                                else //otherwise freeze the enemy
                                {
                                    AI.EntityEffect = ActiveEffect.Frozen;
                                    playerStatus.text = "Success! Froze the enemy!";
                                }
                                break;

                            case 3: //Buff all active minions
                                if (playerOne.NumMinions > 0)
                                {
                                    int minionsBuffed = 0;
                                    for (int j = 0; j < MAX_NUM_MINIONS; j++)
                                    {
                                        if (playerOne.ActiveMinions[j] != null)
                                        {
                                            playerOne.ActiveMinions[j].AddHealth(3);
                                            playerOne.ActiveMinions[j].AddAttack(3);
                                            minionsBuffed++;
                                        }
                                    }
                                    playerStatus.text = "Success! Buffed " + minionsBuffed + " minion(s)!";
                                }
                                else
                                {
                                    playerStatus.text = "Successful placement! But you don't currently have any minions to buff!";
                                }
                                break;

                            case 4: //Kill all enemy minions (Can only be applied to enemy fighter)
                                if (AI.NumMinions > 0)
                                {
                                    for (int j = 0; j < MAX_NUM_MINIONS; j++)
                                    {
                                        if (AI.ActiveMinions[j] != null)
                                        {
                                            AI.ActiveMinions[j].Health = 0;
                                        }
                                    }
                                    playerStatus.text = "Success! All opposing minions destroyed!";
                                }
                                else
                                {
                                    playerStatus.text = "Successful Placement! But your opponent didn't have any minions to destroy!";
                                }
                                break;

                            case 5: //Prevent enemy from placing a specific tile type (can only be applied to enemy fighter)
                                playerStatus.text = "Correct Placement! Choose tile type to block:";
                                burningOpponent = true;
                                break;

                            case 6: //Restore 15 health to self, get rid of negative effects
                                playerOne.AddHealth(15);
                                if (playerOne.EntityEffect == ActiveEffect.Burned || playerOne.EntityEffect == ActiveEffect.Frozen)
                                {
                                    playerOne.EntityEffect = ActiveEffect.None;
                                    playerOne.DisabledTileType = 0;
                                }
                                playerStatus.text = "Success! Healed 5 points and cleansed all negative effects!";
                                break;

                            case 7: //Summmon demon minion if there is room in inventory
                                if (playerOne.NumMinions < 3)
                                {
                                    for (int j = 0; j < MAX_NUM_MINIONS; j++)
                                    {
                                        if (playerOne.ActiveMinions[j] == null)
                                        {
                                            playerOne.AttemptSummonMinion(MinionType.Demon);
                                            playerStatus.text = "Success! Summoned a Demon minion!";
                                            playerMinionTileImages[j].sprite = Tile7;
                                            playerMinionStats[j].text = "Health: " + playerOne.ActiveMinions[j].Health + "\nAttack: " + playerOne.ActiveMinions[j].Attack;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    playerStatus.text = "Correct Placement! But you already have at least 3 minions in play!";
                                }
                                break;

                            case 8: //Permanently raise attack
                                playerOne.AddAttack(5);
                                playerStatus.text = "Success! You raised your attack!";
                                break;

                            case 9: //Summon god minion if there is room in inventory
                                if (playerOne.NumMinions < 3)
                                {
                                    for (int j = 0; j < MAX_NUM_MINIONS; j++)
                                    {
                                        if (playerOne.ActiveMinions[j] == null)
                                        {
                                            playerOne.AttemptSummonMinion(MinionType.God);
                                            playerStatus.text = "Success! Summoned a God minion!";
                                            playerMinionTileImages[j].sprite = Tile9;
                                            playerMinionStats[j].text = "Health: " + playerOne.ActiveMinions[j].Health + "\nAttack: " + playerOne.ActiveMinions[j].Attack;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    playerStatus.text = "Correct Placement! But you already have at least 3 minions in play!";
                                }
                                break;
                        }
                        triesRemaining = 10;
                        playerChoice = 0;

                        #region Player Attack Phase
                        //Attack Phase (can only be reached if they guess correctly before they run out of tries)

                        playerOne.AddAttack(-attackModifier);//adjust damage for any incorrect tries

                        if (AI.NumMinions > 0) //if enemy has minions, attack the first one possible
                        {
                            for (int j = 0; j < MAX_NUM_MINIONS; j++)
                            {
                                if(AI.ActiveMinions[j] != null)
                                {
                                    playerOne.AttackTarget(AI.ActiveMinions[j]);
                                }
                            }
                        }
                        else //otherwise attack the enemy
                        {
                            playerOne.AttackTarget(AI);
                        }
                        playerOne.AddAttack(attackModifier);
                        #endregion
                        playerOne.EntityTurn = false;
                        AI.EntityTurn = true;
                    }
                    #endregion

                    #region Incorrect Guess with tries left
                    else if (!CorrectChoice(i, playerChoice) && triesRemaining - 1 > 0)//player chooses incorrectly, but has tries left
                    {
                        tileImageComponents[i].sprite = Unselected;
                        selectedTileImage.sprite = Unselected;
                        description.text = "Incorrect tile placement.\nTries remaining: " + triesRemaining;
                        playerChoice = 0;
                    }
                    #endregion

                    #region Incorrect Guess with no tries left
                    else //player chooses incorrectly, has no tries left
                    {
                        playerStatus.text = "Incorrect tile placement.\nNo tries Remaining, AI's turn.";
                        triesRemaining = 10;
                        playerChoice = 0;
                        playerOne.EntityTurn = false;
                        AI.EntityTurn = true;
                    }
                    #endregion

                    #region Minion Attack Phase
                    if (playerOne.NumMinions > 0) //Step 1: check if player has any minions
                    {
                        for (int j = 0; j < MAX_NUM_MINIONS; j++)
                        {
                            if (playerOne.ActiveMinions[j] != null)
                            {
                                if (AI.NumMinions > 0) //Step 2: if player has minions, check if enemy has any minions
                                {
                                    for (int k = 0; k < MAX_NUM_MINIONS; k++)
                                    {
                                        if (AI.ActiveMinions[k] != null && AI.ActiveMinions[k].Health > 0) //Step 3 each player minion attacks an enemy minion if they are still alive
                                        {
                                            playerOne.ActiveMinions[j].AttackTarget(AI.ActiveMinions[k]);
                                            break;
                                        }
                                    }
                                }
                                else //Step 4: if the enemy has no minions, all minions attack enemy instead
                                {
                                    playerOne.ActiveMinions[j].AttackTarget(AI);
                                }

                            }
                        }
                    }
                    #endregion

                }
                #endregion
            }
        }
    }

    //Helper method for additional input
    void BurnOpponent()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTileImage.sprite = Tile1;
            description.text = "Shield\nEffect: Resist all damage on next turn.";
            playerChoice = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTileImage.sprite = Tile2;
            description.text = "Ice\nEffect: Prevent an opponent or minion from attacking on next turn";
            playerChoice = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTileImage.sprite = Tile3;
            description.text = "Sorcery\nEffect: Restore +3 health and permanently give +3 attack to all friendly minions in play.";
            playerChoice = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedTileImage.sprite = Tile4;
            description.text = "Wizardry\nEffect: Destroy all of opponent's currently summoned minions.";
            playerChoice = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedTileImage.sprite = Tile5;
            description.text = "Fire\nEffect: Prevent opponent from placing a specific tile type until they use a potion.";
            playerChoice = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedTileImage.sprite = Tile6;
            description.text = "Potion\nEffect: Restore 5 health to self, removes all negative effects.";
            playerChoice = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedTileImage.sprite = Tile7;
            description.text = "Demonology\nEffect: Summon a demon minion with 20 health and 5 attack.";
            playerChoice = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedTileImage.sprite = Tile8;
            description.text = "Sword\nEffect: Permanently raise attack by 5.";
            playerChoice = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedTileImage.sprite = Tile9;
            description.text = "Theurgy\nEffect: Summon a god minion with 10 health and 10 attack.";
            playerChoice = 9;
        }
        if (Input.GetKeyDown(KeyCode.Return)) //submit addtional input
        {
            AI.DisabledTileType = playerChoice;
            description.text = "Success! Opponent can no longer use that tile type!";
            playerChoice = 0;
            burningOpponent = false;
            playerOne.EntityTurn = false;
            AI.EntityTurn = true;
        }
    }

    //Simulate an opposing player's turn
    void AITurn()
    {
        AI.EntityEffectTurns++;
        if (AI.EntityEffectTurns > 1)
        {
            AI.EntityEffect = ActiveEffect.None;
        }
        bool validResult = false;
        int emptyTileIndex = 0;
        int solutionValue = 0;
        int randomTile = Random.Range(1, 10); //Tile that will be blocked in the event of rolling a 5
        while (!validResult)
        {
            emptyTileIndex = Random.Range(0, 81);
            if (tileVals[emptyTileIndex] == 0) //Find a tile that has not been filled yet
            {
                validResult = true;
            }
        }
        solutionValue = solutionVals[emptyTileIndex];
        ChangeTile(emptyTileIndex, solutionValue);
        if (solutionValue == AI.DisabledTileType)
        {
            enemyStatus.text = "Opponent was unable to place anything because that tile type is blocked!";
        }
        else
        {
            switch (solutionValue)
            {
                case 1: //Invulnerability
                    tileImageComponents[emptyTileIndex].sprite = Tile1;
                    AI.EntityEffect = ActiveEffect.Invulnerable;
                    enemyStatus.text = "Opponent is invulnerable next turn!";
                    break;

                case 2: //Frozen
                    tileImageComponents[emptyTileIndex].sprite = Tile2;
                    if (playerOne.NumMinions > 0) //if enemy has minions, freeze the first one possible
                    {
                        for (int j = 0; j < MAX_NUM_MINIONS; j++)
                        {
                            if (playerOne.ActiveMinions[j] != null)
                            {
                                playerOne.ActiveMinions[j].EntityEffect = ActiveEffect.Frozen;
                                enemyStatus.text = "Opponent froze one of your minions!";
                                break;
                            }
                        }
                    }
                    else //otherwise freeze the enemy
                    {
                        AI.EntityEffect = ActiveEffect.Frozen;
                        enemyStatus.text = "Opponent froze you!";
                    }
                    break;

                case 3: //Buff minions
                    tileImageComponents[emptyTileIndex].sprite = Tile3;
                    if (AI.NumMinions > 0)
                    {
                        int minionsBuffed = 0;
                        for (int j = 0; j < MAX_NUM_MINIONS; j++)
                        {
                            if (AI.ActiveMinions[j] != null)
                            {
                                AI.ActiveMinions[j].AddHealth(3);
                                AI.ActiveMinions[j].AddAttack(3);
                                minionsBuffed++;
                            }
                        }
                        enemyStatus.text = "Opponent buffed " + minionsBuffed + " minion(s)!";
                    }
                    else
                    {
                        enemyStatus.text = "Successful placement! But opponent currently doesn't have any minions to buff!";
                    }
                    break;

                case 4: //Destroy all minions
                    tileImageComponents[emptyTileIndex].sprite = Tile4;
                    if (playerOne.NumMinions > 0)
                    {
                        for (int j = 0; j < MAX_NUM_MINIONS; j++)
                        {
                            if (playerOne.ActiveMinions[j] != null)
                            {
                                playerOne.ActiveMinions[j].Health = 0;
                            }
                        }
                        enemyStatus.text = "Your opponent destroyed all of your minions!";
                    }
                    else
                    {
                        enemyStatus.text = "Successful Placement! But you didn't have any minions for your opponent to destroy!";
                    }
                    break;

                case 5: //Burn
                    tileImageComponents[emptyTileIndex].sprite = Tile5;
                    playerOne.DisabledTileType = randomTile;
                    enemyStatus.text = "Your opponent blocked tile type " + randomTile + "!";
                    break;

                case 6: //Potion
                    tileImageComponents[emptyTileIndex].sprite = Tile6;
                    AI.AddHealth(15);
                    if (AI.EntityEffect == ActiveEffect.Burned || AI.EntityEffect == ActiveEffect.Frozen)
                    {
                        AI.EntityEffect = ActiveEffect.None;
                        AI.DisabledTileType = 0;
                    }
                    enemyStatus.text = "Opponent healed 5 health points and removed all negative effects!";
                    break;

                case 7: //Demon Summon
                    tileImageComponents[emptyTileIndex].sprite = Tile7;
                    if (AI.NumMinions < 3)
                    {
                        for (int j = 0; j < MAX_NUM_MINIONS; j++)
                        {
                            if (AI.ActiveMinions[j] == null)
                            {
                                AI.AttemptSummonMinion(MinionType.Demon);
                                enemyStatus.text = "Opponent summoned a Demon minion!";
                                enemyMinionTileImages[j].sprite = Tile7;
                                enemyMinionStats[j].text = "Health: " + AI.ActiveMinions[j].Health + "\nAttack: " + AI.ActiveMinions[j].Attack;
                                break;
                            }
                        }
                    }
                    else
                    {
                        enemyStatus.text = "Correct Placement! But opponent already has at least 3 minions in play!";
                    }
                    break;

                case 8: //Increase Attack
                    tileImageComponents[emptyTileIndex].sprite = Tile8;
                    AI.AddAttack(5);
                    enemyStatus.text = "Opponent raised attack!";
                    break;

                case 9: //God Summon
                    tileImageComponents[emptyTileIndex].sprite = Tile9;
                    if (AI.NumMinions < 3)
                    {
                        for (int j = 0; j < MAX_NUM_MINIONS; j++)
                        {
                            if (AI.ActiveMinions[j] == null)
                            {
                                AI.AttemptSummonMinion(MinionType.God);
                                enemyStatus.text = "Opponent summoned a God minion!";
                                enemyMinionTileImages[j].sprite = Tile9;
                                enemyMinionStats[j].text = "Health: " + AI.ActiveMinions[j].Health + "\nAttack: " + AI.ActiveMinions[j].Attack;
                                break;
                            }
                        }
                    }
                    else
                    {
                        enemyStatus.text = "Correct Placement! But opponent already has at least 3 minions in play!";
                    }
                    break;



            }

            

            #region Enemy attack phase
            //Enemy attack phase
            if (playerOne.NumMinions > 0) //if player has minions, attack the first one possible
            {
                for (int j = 0; j < MAX_NUM_MINIONS; j++)
                {
                    if (playerOne.ActiveMinions[j] != null)
                    {
                        AI.AttackTarget(playerOne.ActiveMinions[j]);
                    }
                }
            }
            else //otherwise attack the player
            {
                AI.AttackTarget(playerOne);
            }

            playerOne.EntityTurn = true;
            AI.EntityTurn = false;
            turnNumber++;

            #endregion

            #region Enemy minion attack phase
            //Enemy minion attack phase
            if (AI.NumMinions > 0) //Step 1: check if enemy has any minions
            {
                for (int j = 0; j < MAX_NUM_MINIONS; j++)
                {
                    if (AI.ActiveMinions[j] != null)
                    {
                        if (playerOne.NumMinions > 0) //Step 2: if enemy has minions, check if player has any minions
                        {
                            for (int k = 0; k < MAX_NUM_MINIONS; k++)
                            {
                                if (playerOne.ActiveMinions[k] != null && playerOne.ActiveMinions[k].Health > 0) //Step 3 each enemy minion attacks a player minion if they are still alive
                                {
                                    AI.ActiveMinions[j].AttackTarget(playerOne.ActiveMinions[k]);
                                    break;
                                }
                            }
                        }
                        else //Step 4: if the player has no minions, all minions attack player instead
                        {
                            AI.ActiveMinions[j].AttackTarget(playerOne);
                        }

                    }
                }
            }
            #endregion

        }

    }
    #endregion

    #endregion
}