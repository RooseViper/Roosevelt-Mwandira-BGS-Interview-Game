using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the State and the general functions of the game. Note: This script is using the Singleton Design pattern.
/// </summary>
public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public RectTransform continueButtonRect;
    public RectTransform continueClosePosition;
    public RectTransform continueOpenPosition;
    public RectTransform popUpDialogBoxRect;
    /// <summary>
    /// Stores and contorols NPC Dialogoue
    /// </summary>
    public Dialogue npcDialogue;    
    public Text popUpDialogueText;
    private Canvas _popUpDialogueCanvas;
    private Vector3 _popupDialogBoxPosition;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    /// <summary>
    /// If true it means player cannot move or interact with objects
    /// </summary>
    private bool _playerIsDormant = false;
    public Canvas[] canvases;
    public bool PlayerIsDormant { get => _playerIsDormant; set => _playerIsDormant = value; }
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _popUpDialogueCanvas = continueButtonRect.GetComponentInParent<Canvas>();
        _popupDialogBoxPosition = popUpDialogBoxRect.localPosition;
        LeanTween.moveLocal(continueButtonRect.gameObject, continueClosePosition.localPosition, 0F);
        LeanTween.moveLocalX(popUpDialogBoxRect.gameObject, -1200F, 0F);
    }

    /// <summary>
    /// Opens and shows the Pop up Dialogue box when Players interact with the NPC
    /// </summary>
    public void OpenPopUpDialogueBox()
    {
        _popUpDialogueCanvas.enabled = true;
        _playerIsDormant = true;
        LeanTween.move(continueButtonRect.gameObject, continueOpenPosition, 1F).setEaseInOutSine().setOnComplete(ShowDialogue);
        LeanTween.moveLocal(popUpDialogBoxRect.gameObject, _popupDialogBoxPosition, 1F).setEaseInOutSine();
    }
    /// <summary>
    /// Shows the dialog of the NPC
    /// </summary>
    private void ShowDialogue()
    {
        npcDialogue.Speak(popUpDialogueText);
    }

    public void OpenShop(bool isBuying)
    {
        popUpDialogueText.text = "";
        _popUpDialogueCanvas.enabled = false;
        _playerIsDormant = false;
        LeanTween.moveLocal(continueButtonRect.gameObject, continueClosePosition.localPosition, 0F);
        LeanTween.moveLocalX(popUpDialogBoxRect.gameObject, -1200F, 0F);
        ShopManager.OnBuyingPanel = isBuying;
        OpenCloseCanvas("CanvasGeneral", false);
        OpenCloseCanvas(isBuying ? "CanvasShopBuying" : "CanvasShopSelling", true);
        ShopManager.Instance.ShopSectionDisplaySelector.SwitchPreviewInformation(isBuying);
    }
    
    /// <summary>
    /// Opens ont the main Canvases
    /// </summary>
    public void OpenCloseCanvas(string tagName, bool open)
    {
        Canvas canvas = Array.Find(canvases, c => c.CompareTag(tagName));
        canvas.enabled = open;
    }
}
