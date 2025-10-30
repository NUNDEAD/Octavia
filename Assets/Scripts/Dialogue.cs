using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]
public struct DialoguePiece
{
    public string name;
    [TextArea] public string dialogue;

}

public class Dialogue : MonoBehaviour
{
    public List<DialoguePiece> dialogue;
    public float textSpeed = 0.1f;

    public TMPro.TMP_Text dialogueName;
    public TMPro.TMP_Text dialogueText;
    public UnityEvent onDialogueEnd;

    private int dialogueIndex;
    private bool isDialogueRunning;

    private static Dialogue currentDialogue;

    public void StartDialogue()
    {
        currentDialogue = this;

        StopAllCoroutines();
        gameObject.SetActive(true);
        dialogueIndex = 0;

        StartCoroutine(WriteDialoguePiece(dialogue[0]));

    }

    public void StopDialogue()
    {
        onDialogueEnd?.Invoke();
        gameObject.SetActive(false);
    }

    public void NextDialogueOrStop(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 0 || currentDialogue.isDialogueRunning)
            return;

        ++currentDialogue.dialogueIndex;

        if (currentDialogue.dialogueIndex >= currentDialogue.dialogue.Count)
        {
            currentDialogue.StopDialogue();
            return;
        }

        currentDialogue.StartCoroutine(currentDialogue.WriteDialoguePiece(currentDialogue.dialogue[currentDialogue.dialogueIndex]));
    }

    public IEnumerator WriteDialoguePiece(DialoguePiece dialogue)
    {
        dialogueName.SetText(dialogue.name);
        dialogueText.SetText("");

        isDialogueRunning = true;

        for (int i = 0; i < dialogue.dialogue.Length; ++i)
        {
            dialogueText.text += dialogue.dialogue[i];
            yield return new WaitForSeconds(textSpeed);
        }

        isDialogueRunning = false;
    }

}