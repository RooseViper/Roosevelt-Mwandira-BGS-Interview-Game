using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the State and the general functions of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    public RectTransform continueButtonRect;
    public RectTransform continueDefaultPosition;
    public RectTransform popUpDialogBoxRect;
    private Canvas popUpDialogCanvas;
    public static GameManager Instance => _instance;

    private static GameManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpDialogCanvas = continueButtonRect.GetComponentInParent<Canvas>();
        LeanTween.moveLocal(continueButtonRect.gameObject, continueDefaultPosition.localPosition, 0F);
        LeanTween.moveLocalX(popUpDialogBoxRect.gameObject, -1200F, 0F);
    }
    
}
