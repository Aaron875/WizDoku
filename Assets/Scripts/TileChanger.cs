using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite blankSprite;
    Sprite[] sprites;
    Sprite blackSprite;
    Sprite darkBlueSprite;
    Sprite greenSprite;
    Sprite lightBlueSprite;
    Sprite orangeSprite;
    Sprite purpleSprite;
    Sprite redSprite;
    Sprite whiteSprite;
    Sprite yellowSprite;
    // Variabe for the new sprite to change a tile to, null when no key is pressed
    Sprite newSprite;
    void Start()
    {
        // Load blank sprite and other sprites from spritesheet
        blankSprite = Resources.Load<Sprite>("BlankTile");
        sprites = Resources.LoadAll<Sprite>("TilesTogether");
        // Variables for each sprite in the array so its easier to tell what sprite it is
        redSprite = sprites[0];
        lightBlueSprite = sprites[1];
        greenSprite = sprites[2];
        yellowSprite = sprites[3];
        orangeSprite = sprites[4];
        purpleSprite = sprites[5];
        darkBlueSprite = sprites[6];
        whiteSprite = sprites[7];
        blackSprite = sprites[8];
    }

    void Update()
    {
        // Check for keyboard input and change newSprite accordingly
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            newSprite = redSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            newSprite = lightBlueSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            newSprite = greenSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            newSprite = yellowSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            newSprite = orangeSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            newSprite = purpleSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            newSprite = darkBlueSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            newSprite = whiteSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            newSprite = blackSprite;
        }
        
        // No key is currently being pressed so set newSprite to null
        if(!Input.anyKey)
        {
            newSprite = null;
        }
    }

    private void OnMouseOver()
    {
        // Get the sprite renderer and check if the tile being hovered over is blank
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == blankSprite)
        {
            // Tile is blank so highlight it and change to the new sprite if newSprite is not null
            Debug.Log("Mouse is over GameObject");
            spriteRenderer.color = Color.yellow;
            if(newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    void OnMouseExit()
    {
        // Undo highlighting when no longer hovering over a tile
        Debug.Log("Mouse has exited GameObject");
        spriteRenderer.color = new Color(255, 255, 255);
    }
}
