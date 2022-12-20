using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the State and the general functions of the game. Note: This script is using the Singleton Design pattern.
/// </summary>
public class GameManager : MonoBehaviour
{
    public RectTransform continueButtonRect;
    public RectTransform continueClosePosition;
    public RectTransform continueOpenPosition;
    public RectTransform popUpDialogBoxRect;
    /// <summary>
    /// Stores and contorols NPC Dialogoue
    /// </summary>
    public Dialogue npcDialogue;    
    public Text popUpDialogueText;
    public Text instructionsText;
    /// <summary>
    /// A small canvas that pops up whenever the Player is close to a pickable Item
    /// </summary>
    public Canvas interactCanvas;

    public Text interactText;
    private Canvas _popUpDialogueCanvas;
    private Vector3 _popupDialogBoxPosition;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    /// <summary>
    /// If true it means player cannot move or interact with objects
    /// </summary>
    private bool _playerIsDormant = false;

    private AudioSource _audioSource;
    /// <summary>
    /// Checks if the game state is Paused
    /// </summary>
    private bool isPaused = false;
    public Canvas[] canvases;
    public bool PlayerIsDormant { get => _playerIsDormant; set => _playerIsDormant = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    private void Awake()
    {
        _playerIsDormant = true;
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        _popUpDialogueCanvas = continueButtonRect.GetComponentInParent<Canvas>();
        _popupDialogBoxPosition = popUpDialogBoxRect.localPosition;
        LeanTween.moveLocal(continueButtonRect.gameObject, continueClosePosition.localPosition, 0F);
        LeanTween.moveLocalX(popUpDialogBoxRect.gameObject, -1200F, 0F);
    }

    /// <summary>
    /// Opens and shows the Pop up Dialogue box for the Shop Keeper/NPC when talking to the Player when they are interacted with.
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

    public void PauseResumeGame()
    { 
        isPaused = !isPaused;
        _audioSource.enabled = !isPaused;
        OpenCloseCanvas("CanvasMainMenu", isPaused);
    }

    public void CloseMainMenu()
    {
        OpenCloseCanvas("CanvasMainMenu", false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleInstructions(Toggle toggle)
    {
        instructionsText.enabled = toggle.isOn;
    }

    /// <summary>
    /// Opens ont the main Canvases
    /// </summary>
    public void OpenCloseCanvas(string tagName, bool open)
    {
        Canvas canvas = Array.Find(canvases, c => c.CompareTag(tagName));
        canvas.enabled = open;
    }
    
    /// <summary>
    /// Pops up an Interact Action Hover Message telling players to press a button inorder to interact with something.
    /// </summary>
    public void ShowInteractAction(string action)
    {
        interactText.text = action;
        interactCanvas.enabled = true;
    }

    public void ActivatePlayer()
    {
        _playerIsDormant = false;
    }
}
