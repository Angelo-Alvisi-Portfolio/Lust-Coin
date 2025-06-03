using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Dialogue;

public class DialogueSystem : MonoBehaviour {

    private Dialogue loadedDialogue;

    private int lineN = 0;
    [SerializeField]
    private TMP_Text nameBox, textBox;
    [SerializeField]
    private SpriteRenderer secondCharPortrait;
    public SpriteRenderer SecondCharPortrait => secondCharPortrait;

    private void OnEnable() {
        Time.timeScale = 0f;
        NextLine();
    }

    private void OnDisable() {
        Time.timeScale = 1f;
        loadedDialogue = null;
        lineN = 0;
    }

    public void NextLine() {
        if (loadedDialogue.Length() == lineN) {
            gameObject.SetActive(false);

        } else {
            DialogueLine line = loadedDialogue[lineN];

            nameBox.SetText(line.Talker);
            if (!line.Multichoice) {
                textBox.SetText(line[0]);
            } else {
                string text = "";
                int n = 0;
                if (line[0][0] != '!') {
                    text += line[0][0] + "\n";
                    n++;
                }
                for (int i = n; i < line.Text.Count; i++) {
                    text += $"{line[i]}";
                    if (i < line.Text.Count - 1) {
                        text += "\n";
                    }
                }
            }
            lineN++;
        }
    }    

    public void LoadDialogue(string[] diag) {
        if (diag == null) {
            loadedDialogue = new Dialogue();
        } else {
            loadedDialogue = new Dialogue(diag);
        }
    }

    public bool NextDiagLine() {
        NextLine();
        return gameObject.activeSelf;
    }

}

