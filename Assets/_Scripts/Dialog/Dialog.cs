using UnityEngine;
using UnityEngine.Video;

namespace Dialog
{
    public sealed class Dialog
    {
        public Dialog(Dialogue dialogue)
        {
            Dialogue = dialogue;
            _ = DialogCard;
            _ = TextBackground;
            _ = DialogText;
            CurrentLine = Dialogue.FirstLine;
            NPCIcon(Dialogue.FirstLine);
        }

        public void SelfDestruct()
        {
            Object.Destroy(_parent);
        }

        public bool LetType;
        public Line CurrentLine;
        public Dialogue Dialogue;

        private GameObject _parent;
        public GameObject Parent => !_parent ? _parent = new GameObject(nameof(Dialog)) : _parent;

        private Card _textBackground;
        private Card TextBackground => _textBackground ??= new Card(nameof(TextBackground), Parent.transform)
            .SetImageSize(Vector2.one * 9001)
            .SetImageSprite(Assets.White)
            .SetImageColor(new Color(0f, .0f, 0f, .666f))
            .SetCanvasSortingOrder(-1);

        private Card[] _npcIcon;
        public void NPCIcon(Line line) => NPCIcon(line.SpeakerIcon, line.SpeakerColor);
        void NPCIcon(Sprite[] sprites, Color col)
        {
            if (_npcIcon != null) foreach (Card c in _npcIcon) UnityEngine.Object.DestroyImmediate(c.GO);
            if (sprites != null) _npcIcon = SetUpNPCIcon();

            Card[] SetUpNPCIcon()
            {
                Card[] cs = new Card[sprites.Length];
                for (int i = 0; i < sprites.Length; i++)
                {
                    cs[i] = new Card(nameof(NPCIcon), Parent.transform)
                          .SetImageSprite(sprites[i])
                          .SetImageSize(Vector3.one * 2f)
                          .SetImagePosition(new Vector3(-Cam.UIOrthoX + .75f, Cam.UIOrthoY - .75f))
                          .SetImageColor(col)
                          .SetCanvasSortingOrder(i + 11);
                }
                return cs;
            }
        }

        private Card _dialogCard;
        public Card DialogCard => _dialogCard ??= new Card(nameof(DialogCard), Parent.transform)
            .SetTextAlignment(TMPro.TextAlignmentOptions.TopLeft)
            .SetImageSize(new Vector2(4f * Cam.Io.UICamera.aspect * 2, 5f))
            .SetImagePosition(new Vector3(0, 2.5f))
            .SetImageSprite(Assets.White)
            .SetImageColor(new Color(.15f, .15f, .15f, .65f))
            .SetCanvasSortingOrder(0);

        private Card _dialogText;
        public Card DialogText => _dialogText ??= new Card(nameof(DialogText), Parent.transform)
            .SetTextAlignment(TMPro.TextAlignmentOptions.TopLeft)
            .SetTMPSize(new Vector2(3.5f * Cam.Io.UICamera.aspect * 2, 4f))
            .SetTMPPosition(new Vector3(0, 2.5f))
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(true)
            .SetCanvasSortingOrder(1)
            .TMPClickable();

        private VideoPlayer _videoPlayer;
        public VideoPlayer VideoPlayer => _videoPlayer = _videoPlayer != null ? _videoPlayer : SetUpVideo();
        VideoPlayer SetUpVideo()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
            go.GetComponent<MeshRenderer>().material = Assets.Video_Mat;
            go.name = nameof(VideoPlayer);
            go.transform.SetParent(Parent.transform);
            go.transform.position = new Vector3(0, -1, -1f);
            VideoPlayer v = go.AddComponent<VideoPlayer>();
            v.playOnAwake = false;
            return v;
        }
    }
}