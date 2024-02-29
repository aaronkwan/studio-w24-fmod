using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Input

    public enum INPUT
    {
        UP, DOWN, LEFT, RIGHT,
        CAST, SPRINT, PAUSE
    }
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode castKey = KeyCode.Space;
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode pauseKey = KeyCode.Escape;

    #endregion

    #region Movement

    [SerializeField] private Rigidbody2D body;
    public float baseSpeed = 0.1f;
    public float currentSpeed = 0.1f;
    private bool isCasting = false;
    private bool isPausing = false;

    private void FixedUpdate()
    {
        // Movement:
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(upKey))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(downKey))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(leftKey))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(rightKey))
        {
            direction += Vector2.right;
        }
        body.AddForce(direction.normalized * currentSpeed);
        Manager.Instance.currentSpeed = (direction == Vector2.zero) ? 0 : 
            direction.normalized.magnitude * currentSpeed;

        // Casting:
        if (Input.GetKey(castKey) && !isCasting)
        {
            Manager.Instance.RingBell();
            isCasting = true;
        }
        else if (!Input.GetKey(castKey))
        {
            isCasting = false;
        }

        // Sprinting:
        if (Input.GetKey(sprintKey))
        {
            currentSpeed = Mathf.Min(currentSpeed + 0.001f, baseSpeed * 1.5f);
        }
        else
        {
            currentSpeed = Mathf.Max(baseSpeed, currentSpeed - 0.002f);
        }

        // Pausing:
        if (Input.GetKey(pauseKey) && !isPausing)
        {
            Manager.Instance.TogglePause();
            isPausing = true;
        }
        else if (!Input.GetKey(pauseKey))
        {
            isPausing = false;
        }
    }
    #endregion

    #region Interaction

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ExitCave")
        {
            Manager.Instance.ExitCave();
            return;
        }
        if (collision.gameObject.name == "EnterCave")
        {
            Manager.Instance.EnterCave();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Grass")
        {
            Manager.Instance.currentGround = Manager.GROUND.GRASS;
            return;
        }
        if (collision.gameObject.name == "Gravel")
        {
            Manager.Instance.currentGround = Manager.GROUND.GRAVEL;
            return;
        }
        if (collision.gameObject.name == "Stone")
        {
            Manager.Instance.currentGround = Manager.GROUND.STONE;
        }
    }


    #endregion
}
