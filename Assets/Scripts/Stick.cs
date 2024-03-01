using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private SpriteRenderer stick;

    private void Start()
    {
        stick = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (Manager.Instance.showSticks == true)
        {
            stick.color = Color.white;
            return;
        }
        if (stick.color.a > 0.2f)
        {
            stick.color = new Color(stick.color.r, stick.color.g, stick.color.b, stick.color.a - 0.02f);
            return;
        }
    }
}
