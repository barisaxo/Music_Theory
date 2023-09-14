using UnityEngine;
using UnityEngine.Video;

namespace Dialog
{
    public class Line
    {
        public Line(string speakerText) { SpeakerText = speakerText; }
        public Line(string speakerText, State nextState) { SpeakerText = speakerText; NextState = nextState; }
        public Line(string speakerText, Line nextLine) { SpeakerText = speakerText; NextLine = nextLine; }
        public Line(string speakerText, Response[] responses) { SpeakerText = speakerText; Responses = responses; }
        public Line(string speakerText, Dialogue dialogue) { SpeakerText = speakerText; NextDialogue = dialogue; }

        public Sprite[] SpeakerIcon { get; private set; } = new Sprite[0];
        public Color SpeakerColor { get; private set; } = Color.white;
        public string SpeakerName { get; private set; } = "\n";
        public string SpeakerText { get; private set; } 

        public VideoClip VideoClip { get; private set; } 

        public Line NextLine { get; private set; } 
        public State NextState { get; private set; } 
        public Dialogue NextDialogue { get; protected set; } 

        public Response[] Responses { get; protected set; }

        public bool FadeOut { get; private set; } 

        public Line FadeToNextState() { FadeOut = true; return this; }
        public Line SetSpeakerIcon(Sprite[] sprites) { SpeakerIcon = sprites; return this; }
        public Line SetSpeakerIcon(Sprite sprites) { SpeakerIcon = new [] { sprites }; return this; }
        public Line SetSpeakerColor(Color color) { SpeakerColor = color; return this; }
        public Line SetSpeakerName(string name) { SpeakerName = name; return this; }
        public Line SetSpeakerText(string speakerText) { SpeakerText = speakerText; return this; }
        public Line SetVideoClip(VideoClip videoClip) { VideoClip = videoClip; return this; }

        public Line SetResponses(Response[] responses) { Responses = responses; return this; }
        public void SetNextDialogue(Dialogue dialogue) { NextDialogue = dialogue; }

    }
}
