using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Stores the Shop Keepers dialogue. Note: This code is reusable and can be used on any other NPC's
/// </summary>
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
    /// Greets the Player when they walk in the SHop.
    /// </summary>
    public void Greet()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            StopAllCoroutines();
            StartCoroutine(SlowlyTypeSentence(greetings[Random.Range(0, greetings.Length)]));
            StartCoroutine(CloseCanvas());
        }
    }

    /// <summary>
    /// Types in the greeting slowly in the text area
    /// </summary>
    /// <returns></returns>
    private IEnumerator SlowlyTypeSentence(string greeting)
    {
        _dialogueText.text = "";
        foreach (char letter in greeting.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private IEnumerator CloseCanvas()
    {
        yield return new WaitForSeconds(7.5f);
        canvas.enabled = false;
    }
}
