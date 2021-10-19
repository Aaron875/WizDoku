using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LelevStart : MonoBehaviour
{
    private int [,] numbers = new int[9,9];
    private bool[,] selected = new bool [9,9];
    private bool[,] boxExists = new bool[9, 9];
    private SpriteRenderer[,] boxes = new SpriteRenderer[9, 9];

    [SerializeField]
    SpriteRenderer box;


    // Start is called before the first frame update
    void Start()
    {
        //sets some of the 2D arrays to fill with values
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                selected[i, j] = false;
                boxExists[i, j] = true;
                boxes[i, j] = null;
            }
        }

        //sets the first box as selected
        selected[0, 0] = true;

        //Set all boxExist values to false where there is not an inital box present
        boxExists[0, 1] = false;
        boxExists[0, 2] = false;
        boxExists[0, 5] = false;
        boxExists[0, 6] = false;
        boxExists[1, 2] = false;
        boxExists[1, 3] = false;
        boxExists[1, 4] = false;
        boxExists[1, 6] = false;
        boxExists[2, 1] = false;
        boxExists[2, 5] = false;
        boxExists[2, 7] = false;
        boxExists[2, 8] = false;
        boxExists[3, 0] = false;
        boxExists[3, 1] = false;
        boxExists[3, 5] = false;
        boxExists[4, 3] = false;
        boxExists[4, 4] = false;
        boxExists[4, 6] = false;
        boxExists[4, 7] = false;
        boxExists[5, 1] = false;
        boxExists[5, 2] = false;
        boxExists[5, 4] = false;
        boxExists[5, 8] = false;
        boxExists[6, 1] = false;
        boxExists[6, 2] = false;
        boxExists[6, 4] = false;
        boxExists[6, 7] = false;
        boxExists[7, 0] = false;
        boxExists[7, 2] = false;
        boxExists[7, 6] = false;
        boxExists[8, 3] = false;
        boxExists[8, 5] = false;
        boxExists[8, 7] = false;
        boxExists[8, 8] = false;


        //Sets up all of the boxes within their spots on the screen and the 2D array
        boxes[0, 0] = Instantiate(box, new Vector2(-10.097f, 4.498f), Quaternion.identity);
        boxes[0, 3] = Instantiate(box, new Vector2(-7.097f, 4.498f), Quaternion.identity);
        boxes[0, 4] = Instantiate(box, new Vector2(-6.086f, 4.498f), Quaternion.identity);
        boxes[0, 7] = Instantiate(box, new Vector2(-3.083f, 4.498f), Quaternion.identity);
        boxes[0, 8] = Instantiate(box, new Vector2(-2.056f, 4.498f), Quaternion.identity);
        boxes[1, 0] = Instantiate(box, new Vector2(-10.092f, 3.498f), Quaternion.identity);
        boxes[1, 1] = Instantiate(box, new Vector2(-9.095f, 3.498f), Quaternion.identity);
        boxes[1, 5] = Instantiate(box, new Vector2(-5.103f, 3.498f), Quaternion.identity);
        boxes[1, 7] = Instantiate(box, new Vector2(-3.063f, 3.498f), Quaternion.identity);
        boxes[1, 8] = Instantiate(box, new Vector2(-2.059f, 3.498f), Quaternion.identity);
        boxes[2, 0] = Instantiate(box, new Vector2(-10.098f, 2.498f), Quaternion.identity);
        boxes[2, 2] = Instantiate(box, new Vector2(-8.097f, 2.498f), Quaternion.identity);
        boxes[2, 3] = Instantiate(box, new Vector2(-7.098f, 2.498f), Quaternion.identity);
        boxes[2, 4] = Instantiate(box, new Vector2(-6.093f, 2.498f), Quaternion.identity);
        boxes[2, 6] = Instantiate(box, new Vector2(-4.083f, 2.498f), Quaternion.identity);
        boxes[3, 2] = Instantiate(box, new Vector2(-8.101f, 1.501f), Quaternion.identity);
        boxes[3, 3] = Instantiate(box, new Vector2(-7.085f, 1.501f), Quaternion.identity);
        boxes[3, 4] = Instantiate(box, new Vector2(-6.069f, 1.501f), Quaternion.identity);
        boxes[3, 6] = Instantiate(box, new Vector2(-4.08f, 1.501f), Quaternion.identity);
        boxes[3, 7] = Instantiate(box, new Vector2(-3.096f, 1.501f), Quaternion.identity);
        boxes[3, 8] = Instantiate(box, new Vector2(-2.058f, 1.501f), Quaternion.identity);
        boxes[4, 0] = Instantiate(box, new Vector2(-10.089f, 0.506f), Quaternion.identity);
        boxes[4, 1] = Instantiate(box, new Vector2(-9.094f, 0.506f), Quaternion.identity);
        boxes[4, 2] = Instantiate(box, new Vector2(-8.08f, 0.506f), Quaternion.identity);
        boxes[4, 5] = Instantiate(box, new Vector2(-5.102f, 0.506f), Quaternion.identity);
        boxes[4, 8] = Instantiate(box, new Vector2(-2.059f, 0.506f), Quaternion.identity);
        boxes[5, 0] = Instantiate(box, new Vector2(-10.086f, -0.504f), Quaternion.identity);
        boxes[5, 4] = Instantiate(box, new Vector2(-6.096f, -0.504f), Quaternion.identity);
        boxes[5, 5] = Instantiate(box, new Vector2(-5.104f, -0.504f), Quaternion.identity);
        boxes[5, 6] = Instantiate(box, new Vector2(-4.086f, -0.504f), Quaternion.identity);
        boxes[5, 7] = Instantiate(box, new Vector2(-3.064f, -0.504f), Quaternion.identity);
        boxes[6, 0] = Instantiate(box, new Vector2(-10.093f, -1.495f), Quaternion.identity);
        boxes[6, 3] = Instantiate(box, new Vector2(-7.104f, -1.495f), Quaternion.identity);
        boxes[6, 5] = Instantiate(box, new Vector2(-5.085f, -1.495f), Quaternion.identity);
        boxes[6, 6] = Instantiate(box, new Vector2(-4.086f, -1.495f), Quaternion.identity);
        boxes[6, 8] = Instantiate(box, new Vector2(-2.059f, -1.495f), Quaternion.identity);
        boxes[7, 1] = Instantiate(box, new Vector2(-9.08f, -2.486f), Quaternion.identity);
        boxes[7, 3] = Instantiate(box, new Vector2(-7.091f, -2.486f), Quaternion.identity);
        boxes[7, 4] = Instantiate(box, new Vector2(-6.101f, -2.486f), Quaternion.identity);
        boxes[7, 5] = Instantiate(box, new Vector2(-5.099f, -2.486f), Quaternion.identity);
        boxes[7, 7] = Instantiate(box, new Vector2(-3.097f, -2.486f), Quaternion.identity);
        boxes[7, 8] = Instantiate(box, new Vector2(-2.053f, -2.486f), Quaternion.identity);
        boxes[8, 0] = Instantiate(box, new Vector2(-10.109f, -3.484f), Quaternion.identity);
        boxes[8, 1] = Instantiate(box, new Vector2(-9.076f, -3.484f), Quaternion.identity);
        boxes[8, 2] = Instantiate(box, new Vector2(-8.092f, -3.484f), Quaternion.identity);
        boxes[8, 4] = Instantiate(box, new Vector2(-6.1f, -3.484f), Quaternion.identity);
        boxes[8, 6] = Instantiate(box, new Vector2(-4.101f, -3.484f), Quaternion.identity);


        //Sets up the 2D array with all of the numbers for the sudoku board
        numbers[0, 0] = 6;
        numbers[0, 1] = 4;
        numbers[0, 2] = 2;
        numbers[0, 3] = 7;
        numbers[0, 4] = 5;
        numbers[0, 5] = 9;
        numbers[0, 6] = 8;
        numbers[0, 7] = 3;
        numbers[0, 8] = 1;
        numbers[1, 0] = 7;
        numbers[1, 1] = 3;
        numbers[1, 2] = 8;
        numbers[1, 3] = 2;
        numbers[1, 4] = 6;
        numbers[1, 5] = 1;
        numbers[1, 6] = 9;
        numbers[1, 7] = 5;
        numbers[1, 8] = 4;
        numbers[2, 0] = 9;
        numbers[2, 1] = 1;
        numbers[2, 2] = 5;
        numbers[2, 3] = 8;
        numbers[2, 4] = 3;
        numbers[2, 5] = 4;
        numbers[2, 6] = 2;
        numbers[2, 7] = 6;
        numbers[2, 8] = 7;
        numbers[3, 0] = 4;
        numbers[3, 1] = 5;
        numbers[3, 2] = 7;
        numbers[3, 3] = 6;
        numbers[3, 4] = 9;
        numbers[3, 5] = 3;
        numbers[3, 6] = 1;
        numbers[3, 7] = 2;
        numbers[3, 8] = 8;
        numbers[4, 0] = 3;
        numbers[4, 1] = 2;
        numbers[4, 2] = 6;
        numbers[4, 3] = 1;
        numbers[4, 4] = 7;
        numbers[4, 5] = 8;
        numbers[4, 6] = 5;
        numbers[4, 7] = 4;
        numbers[4, 8] = 9;
        numbers[5, 0] = 1;
        numbers[5, 1] = 8;
        numbers[5, 2] = 9;
        numbers[5, 3] = 5;
        numbers[5, 4] = 4;
        numbers[5, 5] = 2;
        numbers[5, 6] = 6;
        numbers[5, 7] = 7;
        numbers[5, 8] = 3;
        numbers[6, 0] = 2;
        numbers[6, 1] = 9; 
        numbers[6, 2] = 3;
        numbers[6, 3] = 4;
        numbers[6, 4] = 1;
        numbers[6, 5] = 5;
        numbers[6, 6] = 7;
        numbers[6, 7] = 8;
        numbers[6, 8] = 6;
        numbers[7, 0] = 8;
        numbers[7, 1] = 6;
        numbers[7, 2] = 1;
        numbers[7, 3] = 3;
        numbers[7, 4] = 2;
        numbers[7, 5] = 7;
        numbers[7, 6] = 4;
        numbers[7, 7] = 9;
        numbers[7, 8] = 5;
        numbers[8, 0] = 5;
        numbers[8, 1] = 7;
        numbers[8, 2] = 4;
        numbers[8, 3] = 9;
        numbers[8, 4] = 8;
        numbers[8, 5] = 6;
        numbers[8, 6] = 3;
        numbers[8, 7] = 1;
        numbers[8, 8] = 2;


        //sets the inital selected box to red
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (selected[i, j] == true)
                {
                    boxes[i, j].color = Color.red;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Get key input for moving the selected box left right up and down
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (selected[i, j] == true)
                    {
                        //if all other boxes in front of selected are gone go to the first available box
                        for (int q = j + 1; q < 9; q++)
                        {
                            if (boxExists[i, q] == true)
                            {
                                break;
                            }
                            else if (boxExists[i, q] == false && q == 8)
                            {
                                for (int l = 0; l < 9; l++)
                                {
                                    for (int p = 0; p < 9; p++)
                                    {
                                        if (boxExists[l, p] == true)
                                        {
                                            selected[i, j] = false;
                                            boxes[i, j].color = Color.white;
                                            selected[l, p] = true;
                                            boxes[l, p].color = Color.red;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        //if you get to the end of the row go to the first available space
                        if (j == 8)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                for (int p = 0; p < 9; p++)
                                {
                                    if (boxExists[l, p] == true)
                                    {
                                        selected[i, j] = false;
                                        boxes[i, j].color = Color.white;
                                        selected[l, p] = true;
                                        boxes[l, p].color = Color.red;
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int l = j + 1; l < 9; l++)
                            {
                                if (boxes[i, l] != null)
                                {
                                    selected[i, j] = false;
                                    boxes[i, j].color = Color.white;
                                    selected[i, l] = true;
                                    boxes[i, l].color = Color.red;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (selected[i, j] == true)
                    {
                        //if all other boxes in front of selected are gone go to the first available box
                        for (int q = i + 1; q < 9; q++)
                        {
                            if (boxExists[q, j] == true)
                            {
                                break;
                            }
                            else if (boxExists[q, j] == false && q == 8)
                            {
                                for (int l = 0; l < 9; l++)
                                {
                                    for (int p = 0; p < 9; p++)
                                    {
                                        if (boxExists[l, p] == true)
                                        {
                                            selected[i, j] = false;
                                            boxes[i, j].color = Color.white;
                                            selected[l, p] = true;
                                            boxes[l, p].color = Color.red;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        //if you get to the end of the column go to the first available space
                        if (i == 8)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                for (int p = 0; p < 9; p++)
                                {
                                    if (boxExists[l, p] == true)
                                    {
                                        selected[i, j] = false;
                                        boxes[i, j].color = Color.white;
                                        selected[l, p] = true;
                                        boxes[l, p].color = Color.red;
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int l = i + 1; l < 9; l++)
                            {
                                if (boxes[l, j] != null)
                                {
                                    selected[i, j] = false;
                                    boxes[i, j].color = Color.white;
                                    selected[l, j] = true;
                                    boxes[l, j].color = Color.red;
                                    return;
                                }
                            }
                        }

                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (selected[i, j] == true)
                    {
                        //if all other boxes in front of selected are gone go to the first available box
                        for (int q = i - 1; q > -1; q--)
                        {
                            if (boxExists[q, j] == true)
                            {
                                break;
                            }
                            else if (boxExists[q, j] == false && q == 0)
                            {
                                for (int l = 0; l < 9; l++)
                                {
                                    for (int p = 0; p < 9; p++)
                                    {
                                        if (boxExists[l, p] == true)
                                        {
                                            selected[i, j] = false;
                                            boxes[i, j].color = Color.white;
                                            selected[l, p] = true;
                                            boxes[l, p].color = Color.red;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        //if you get to the start of the column go to the first available space
                        if (i == 0)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                for (int p = 0; p < 9; p++)
                                {
                                    if (boxExists[l, p] == true)
                                    {
                                        selected[i, j] = false;
                                        boxes[i, j].color = Color.white;
                                        selected[l, p] = true;
                                        boxes[l, p].color = Color.red;
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int l = i - 1; l > -1; l--)
                            {
                                if (boxes[l, j] != null)
                                {
                                    selected[i, j] = false;
                                    boxes[i, j].color = Color.white;
                                    selected[l, j] = true;
                                    boxes[l, j].color = Color.red;
                                    return;
                                }
                            }
                        }

                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (selected[i, j] == true)
                    {
                        //if all other boxes in front of selected are gone go to the first available box
                        for (int q = j - 1; q > -1; q--)
                        {
                            if (boxExists[i, q] == true)
                            {
                                break;
                            }
                            else if (boxExists[i, q] == false && q == 0)
                            {
                                for (int l = 0; l < 9; l++)
                                {
                                    for (int p = 0; p < 9; p++)
                                    {
                                        if (boxExists[l, p] == true)
                                        {
                                            selected[i, j] = false;
                                            boxes[i, j].color = Color.white;
                                            selected[l, p] = true;
                                            boxes[l, p].color = Color.red;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        //if you get to the start of the row go to the first available space
                        if (j == 0)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                for (int p = 0; p < 9; p++)
                                {
                                    if (boxExists[l, p] == true)
                                    {
                                        selected[i, j] = false;
                                        boxes[i, j].color = Color.white;
                                        selected[l, p] = true;
                                        boxes[l, p].color = Color.red;
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int l = j - 1; l > -1; l--)
                            {
                                if (boxes[i, l] != null)
                                {
                                    selected[i, j] = false;
                                    boxes[i, j].color = Color.white;
                                    selected[i, l] = true;
                                    boxes[i, l].color = Color.red;
                                    return;
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        //if the player gets the correct guess delete the box and reveal the symbol
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (selected[i, j] == true && numbers[i, j] == 1 && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l,p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 2 && Input.GetKeyDown(KeyCode.Alpha2))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 3 && Input.GetKeyDown(KeyCode.Alpha3))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 4 && Input.GetKeyDown(KeyCode.Alpha4))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 5 && Input.GetKeyDown(KeyCode.Alpha5))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 6 && Input.GetKeyDown(KeyCode.Alpha6))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 7 && Input.GetKeyDown(KeyCode.Alpha7))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 8 && Input.GetKeyDown(KeyCode.Alpha8))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
                else if (selected[i, j] == true && numbers[i, j] == 9 && Input.GetKeyDown(KeyCode.Alpha9))
                {
                    selected[i, j] = false;
                    Destroy(boxes[i, j]);
                    boxExists[i, j] = false;
                    for (int l = 0; l < 9; l++)
                    {
                        for (int p = 0; p < 9; p++)
                        {
                            if (boxExists[l, p] == true)
                            {
                                selected[l, p] = true;
                                boxes[l, p].color = Color.red;
                                return;
                            }
                        }
                    }
                    return;
                }
            }
        }
    }
}
