using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TracablePixel : Tracable
{
    public SpriteRenderer spriteRenderer;
    


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        traced = false;
    }

    public override void ResetTrace()
    {
        base.ResetTrace();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnMouseOver()
    {
        if (Mouse.current.leftButton.isPressed == true)
        {
            traced = true;
            spriteRenderer.color = Color.black;
        }
    }
}
