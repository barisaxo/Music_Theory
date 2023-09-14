namespace Dialog
{
    public class Response
    {
        public Response(string text) { Text = text; }
        public Response(string text, Line goToLine) { Text = text; GoToLine = goToLine; }
        public Response(string text, State nextState) { Text = text; NextState = nextState; }
        public Response(string text, Dialogue dialogue) { Text = text; GoToDialogue = dialogue; }

        public string Text { get; private set; } = null;
        public Line GoToLine { get; private set; } = null;
        public State NextState { get; private set; } = null;
        public Dialogue GoToDialogue { get; private set; } = null;
        public bool FadeOut { get; private set; } = false;

        public Response SetGoToLine(Line gotoLine) { GoToLine = gotoLine; return this; }
        public Response FadeToNextState() { FadeOut = true; return this; }
    }
}

