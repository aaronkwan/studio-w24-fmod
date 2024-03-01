using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject lamp_off;
    [SerializeField] private GameObject lamp_on;

    #region Input

    public enum INPUT
    {
        UP, DOWN, LEFT, RIGHT,
        CAST, SPRINT, PAUSE,
        INC_LIFE, DEC_LIFE
    }
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode castKey = KeyCode.Space;
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode pauseKey = KeyCode.Escape;
    private KeyCode incKey = KeyCode.KeypadPlus;
    private KeyCode decKey = KeyCode.KeypadMinus;


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

        // Rotation:
        if (direction != Vector2.zero)
        {
            float targetAngle = (direction == Vector2.up) ? 0 : (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            float currentAngle = transform.rotation.eulerAngles.z;
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, 0.2f);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        // Casting:
        if (Input.GetKey(castKey) && !isCasting)
        {
            isCasting = true;
            if (flashLightCoroutine == null && !hasStick)
            {
                flashLightCoroutine = StartCoroutine(FlashLightCoroutine());
                Manager.Instance.FlashLight();
            }
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

        // Timer cheats:
        if (Input.GetKey(incKey))
        {
            Manager.Instance.life += 0.5f;
        }
        if (Input.GetKey(decKey))
        {
            Manager.Instance.life -= 0.5f;
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
            return;
        }
        if (collision.gameObject.name == "Stick(Clone)")
        {
            PickUpStick();
            Destroy(collision.gameObject);
            Manager.Instance.SpawnNewStick();
            return;
        }
        if (collision.gameObject.name == "Campfire" && hasStick)
        {
            DropStick();
            Manager.Instance.life += 10f;
            return;
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
            return;
        }
    }

    private Coroutine flashLightCoroutine;
    private bool hasStick = false;
    IEnumerator FlashLightCoroutine()
    {
        // Flash Lamp, idle for 4 seconds, then reset.
        lamp_on.SetActive(true);
        lamp_off.SetActive(false);
        Manager.Instance.showSticks = true;
        yield return new WaitForSeconds(0.5f);
        lamp_on.SetActive(false);
        lamp_off.SetActive(false);
        Manager.Instance.showSticks = false;
        yield return new WaitForSeconds(4f);
        lamp_off.SetActive(true);
        flashLightCoroutine = null;
    }
    private void PickUpStick()
    {
        hasStick = true;
        if (flashLightCoroutine != null)
        {
            StopCoroutine(flashLightCoroutine);
            flashLightCoroutine = null;
            Manager.Instance.showSticks = false;
        }
        lamp_on.SetActive(false);
        lamp_off.SetActive(false);
        stick.SetActive(true);
    }
    private void DropStick()
    {
        hasStick = false;
        stick.SetActive(false);
        lamp_on.SetActive(false);
        lamp_off.SetActive(true);
    }



    #endregion
}
