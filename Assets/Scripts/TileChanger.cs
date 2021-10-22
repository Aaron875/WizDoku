using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnMouseHover()
    {

    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse is over GameObject");
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse has exited GameObject");
        spriteRenderer.color = Color.grey;
    }

}
