using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.TimeZoneInfo;

public class Manager : MonoBehaviour
{
    #region Singleton
    
    public static Manager Instance { get; private set; }

    public View m_view;
    public Sound m_sound;
    public Display m_display;
    public Player m_player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    #region Controls

    public void RingBell()
    {
        UnityEngine.Debug.Log("ringing bell");
    }
    
    public void TogglePause()
    {
        m_display.gameObject.SetActive(!m_display.gameObject.activeSelf);
    }
    #endregion

    #region Transition

    [SerializeField] private SpriteRenderer caveCover;
    private Coroutine fadeCoroutine;
    private void Start()
    {
        caveCover.gameObject.SetActive(true);
    }
    public void EnterCave()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCover(0.3f, 250));
    }
    public void ExitCave()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCover(1f, 100));
    }
    private IEnumerator FadeCover(float targetAlpha, int ticks)
    {
        float startAlpha = caveCover.color.a;
        float alphaStep = (targetAlpha - startAlpha) / ticks;
        for (int i = 0; i < ticks; i++)
        {
            float newAlpha = startAlpha + alphaStep * i;
            caveCover.color = new Color(caveCover.color.r, caveCover.color.g, caveCover.color.b, newAlpha);
            yield return null;
        }
        caveCover.color = new Color(caveCover.color.r, caveCover.color.g, caveCover.color.b, targetAlpha);
    }
    #endregion

    #region Footstep

    public enum GROUND
    {
        GRASS, GRAVEL, STONE
    }

    public GROUND currentGround;
    public float currentSpeed;

    #endregion
}
