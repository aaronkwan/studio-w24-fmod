using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float sizePercent = Manager.Instance.life / 30f;
        if (sizePercent < 0)
        {
            gameObject.SetActive(false);
            Manager.Instance.EndGame();
        }
        transform.localScale = Vector2.one * sizePercent * 1.5f;
    }
}
