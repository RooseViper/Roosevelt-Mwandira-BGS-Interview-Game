using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Stores the Shop Keepers dialogue. Note: This code is reusable and can be used on any other NPC's
/// </summary>
public class Dialogue : MonoBehaviour
{
    /// <summary>
    /// Used in the conversation with the Player in the Shop
    /// </summary>
    [TextArea(3, 10)]
    public string[] sentences;
    /// <summary>
    /// Greeting lines the NPC says to the the Player when they see them.
    /// </summary>
    [TextArea(3, 10)]
    public string[] greetings;
    public Canvas canvas;
    private Text _dialogueText;
    void Start()
    {
        _dialogueText = canvas.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Chooses and displays a random greeting line when the NPC sees the Player. e.g. When the Player enters their Shop.
    /// </summary>
    public void Greet()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            StopAllCoroutines();
            StartCoroutine(SlowlyTypeSentence(greetings[Random.Range(0, greetings.Length)], _dialogueText, 0.05F));
            StartCoroutine(CloseCanvas());
        }
    }
    /// <summary>
    /// Chooses and displays a random sentence when interacting with the Player.
    /// </summary>
    public void Speak(Text dialogueText)
    {
        StartCoroutine(SlowlyTypeSentence(greetings[Random.Range(0, greetings.Length)], dialogueText, 0.025F));
    }

    /// <summary>
    /// Types in the greeting slowly in the text area
    /// </summary>
    /// <returns></returns>
    private IEnumerator SlowlyTypeSentence(string words, Text dialogueText, float speed)
    {
        dialogueText.text = "";
        foreach (char letter in words.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
    private IEnumerator CloseCanvas()
    {
        yield return new WaitForSeconds(7.5f);
        canvas.enabled = false;
    }
}
