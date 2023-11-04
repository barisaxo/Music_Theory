using UnityEngine;
using TMPro;

namespace Dialog
{
    public class Reply
    {
        public Reply(Response[] responses)
        {
            Responses = responses;
            SetUpResponses();
        }

        public void SelfDestruct()
        {
            Object.Destroy(Parent);
        }
        public Response[] Responses;

        private GameObject _parent;
        public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(Reply));

        private Card[] _responseCards;
        public Card[] ResponseCards => _responseCards;

        public void SetUpResponses()
        {
            Card[] textCards = new Card[Responses.Length];

            for (int i = 0; i < Responses.Length; i++)
            {
                int iLIFO = Responses.Length - i - 1;

                textCards[i] = new Card(nameof(ResponseCards) + i, Parent.transform)
                    .SetTextString(Responses[i].Text)
                    .AutoSizeTextContainer(true)
                    .SetPositionAll(new Vector2(Cam.UIOrthoX - 2.5f, -Cam.UIOrthoY + 1 + (iLIFO * 1.15f)))
                    .SetTextAlignment(TextAlignmentOptions.Right)
                    .AutoSizeFont(true)
                    .SetTMPRectPivot(new Vector2(1, .5f))
                    .SetImageSprite(GetSprite(iLIFO))
                    .SetFontScale(.6f, .6f)
                    .SetImageSize(Vector2.one * .6f)
                    .OffsetImagePosition(Vector2.right)
                    .ImageClickable()
                    .TMPClickable();
            }

            _responseCards = textCards;

            Sprite GetSprite(int i) => i switch
            {
                _ => Assets.White,
            };
        }
    }
}