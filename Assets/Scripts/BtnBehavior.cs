using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnBehavior : MonoBehaviour
{
    public string BtnName;

    private void OnMouseDown()
    {
        switch (BtnName)
        {
            //If the Start button is clicked, load the tutorial scene
            case "StartBtn":
                SwitchScene("Tutorial");
                break;
            case "BeginGame":
                SwitchScene("Level 1B");
                break;
            //If the quit button is clicked, close the game
            case "QuitBtn":
                Application.Quit();
                break;
        }
    }

    public void SetSelectedTile(GameObject hoverTile, GameObject targetTile)
    {
        targetTile.GetComponent<Image>().sprite = hoverTile.GetComponent<Image>().sprite;
    }

    public void SwitchScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

}
