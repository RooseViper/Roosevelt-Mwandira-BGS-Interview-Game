using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    /// <summary>
    /// Used in the conversation with the Player
    /// </summary>
    [TextArea(3, 10)]
    public string[] sentences;
    [TextArea(3, 10)]
    public string[] greetings;
    public Canvas canvas;
    private Text _dialogueText;
    void Start()
    {
        _dialogueText = canvas.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Allows the Merchant to say some random dialogue as the Player walks in
    /// </summary>
    public void Speak()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            _dialogueText.text = greetings[Random.Range(0, greetings.Length)];
            StopAllCoroutines();
            StartCoroutine(CloseCanvas());
        }
    }

    private IEnumerator CloseCanvas()
    {
        yield return new WaitForSeconds(5f);
        canvas.enabled = false;
    }
}
