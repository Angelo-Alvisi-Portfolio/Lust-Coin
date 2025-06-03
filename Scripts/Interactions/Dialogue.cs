using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dialogue {

    private DialogueLine[] lines;
    private string errorDialogue = "Sorry, not implemented yet.";

    /// <summary>
    /// Only use the 0 parameter constructor for the error message.
    /// </summary>
    public Dialogue() {
        lines = new DialogueLine[1];
        lines[0] = new DialogueLine('!', "Alex", errorDialogue);
    }

    public Dialogue(DialogueLine[] lines) {
        this.lines = lines;
    }

    public DialogueLine this[int i] {
        get {
            return lines[i];
        }        
    }

    public int Length() {
        return lines.Length;
    } 


    /// <summary>
    /// Constructor to use normally
    /// </summary>
    /// <param name="lines"></param>
    public Dialogue(string[] rawStrings) {
        lines = new DialogueLine[rawStrings.Length];

        for (int i = 0; i < rawStrings.Length; i++) {
            string s = rawStrings[i];
            int colIndex = s.IndexOf(':');
            DialogueLine line = new DialogueLine(s[0], s.Substring(1, colIndex - 1), s.Substring(colIndex + 2, s.Length - (colIndex + 2)));
            lines[i] = line;
        }
    }
    public class DialogueLine {
        private bool multichoice;
        private string talker;
        private List<string> text = new();

        public bool Multichoice => multichoice;
        public string Talker => talker;
        public List<string> Text => text;
        /// <summary>
        /// Use this format: "?name: " for the data. If the passed lineN is multitext, every option should have a ! added before it, if you want to add an introduction leave it blank.
        /// Es. ?Alex: Do I like pie? !Yes. !No.
        /// </summary>
        /// <param name="diagType"></param>
        /// <param name="talker"></param>
        /// <param name="rawText"></param>
        public DialogueLine(char diagType, string talker, string rawText) {
            multichoice = diagType == '?' ? true : false;
            this.talker = talker;
            if (!multichoice) {
                text.Add(rawText);
            } else {
                string[] arr = rawText.Split(" !");                
            }
        }

        public string this[int i] {
            get {
                return text[i];
            }
        }

    }
}
