using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : Selectable
{
    public GameObject numberText;
    private int number = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText()
    {
        if(number <= 0)
        {
            numberText.GetComponent<Text>().text = " ";
        }
        else
        {
            numberText.GetComponent<Text>().text = number.ToString();
        }
    }

    public void SetNumber(int number_)
    {
        number = number_;
        DisplayText();
    }
}
