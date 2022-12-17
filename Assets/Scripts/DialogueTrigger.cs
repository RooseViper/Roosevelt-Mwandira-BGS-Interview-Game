using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   public Dialogue dialogue;

   /// <summary>
   /// Called when Player collides into Dialogue Trigger
   /// </summary>
   public void TriggerGreetings()
   {
      dialogue.Greet();
   }
}
