using UnityEngine;

namespace Dialog
{
    public class Dialogue
    {
        public Dialogue() { }
        public Dialogue(State consequentState) { ConsequentState = consequentState; }
        protected readonly State ConsequentState;

        public Line FirstLine;
        public Sprite[] SpeakerIcon = new Sprite[0];
        public Color SpeakerColor = Color.white;
        public AudioClip StartSound;
        public bool PlayTypingSounds = true;

        public string SpeakerName = "\n";
        public string SpeakerText = string.Empty;

        public Dialogue SetSpeakerIcon(Sprite[] sprites) { SpeakerIcon = sprites; return this; }
        public Dialogue SetSpeakerColor(Color color) { SpeakerColor = color; return this; }
        public Dialogue SetSpeakerName(string name) { SpeakerName = name; return this; }
        public Dialogue SetSpeakerText(string speakerText) { SpeakerText = speakerText; return this; }
        //public Dialogue SetCharacterData(CharacterData characterData) { CharacterData = characterData; return this; }
        public Dialogue MuteTypingSounds() { PlayTypingSounds = false; return this; }

        public virtual Dialogue Initiate() { return this; }
    }
}