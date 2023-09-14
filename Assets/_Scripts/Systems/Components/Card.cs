using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card
{
    public Card(string name, Transform parent)
    {
        Name = name;
        GO = new GameObject(name);
        GO.transform.SetParent(parent, false);
    }

    private Card() { }

    private Card(string name, Card parentCard, Transform parent)
    {
        Name = name;
        ParentCard = parentCard;
        Parent = parent;
    }

    private Card(string name, Card parentCard, Transform parent, Canvas _)
    {
        Name = name;
        ParentCard = parentCard;
        Parent = parent;
        _canvas = parentCard.Canvas;
        _canvasScaler = parentCard.CanvasScaler;
    }

    public void SelfDestruct()
    {
        if (Children != null) { foreach (Card child in Children) child.SelfDestruct(); }
        if (GO != null) Object.Destroy(GO);
    }

    public string Name { get; private set; }
    public Card ParentCard { get; private set; } = null;
    public Card[] Children { get; private set; } = null;
    public GameObject GO { get; private set; } = null;
    public Clickable Clickable { get; private set; } = null;

    private Transform Parent;

    public string TextString { get => TMP.text; set => TMP.text = value; }
    private SpriteRenderer _sr = null;
    public SpriteRenderer SpriteRenderer => _sr != null ? _sr : _sr = GO.AddComponent<SpriteRenderer>();


    private TextMeshProUGUI _tmp;
    public TextMeshProUGUI TMP
    {
        get
        {
            return _tmp != null ? _tmp : _tmp = SetUpTMP();

            TextMeshProUGUI SetUpTMP()
            {
                TextMeshProUGUI t = new GameObject(Name + nameof(TMP)).AddComponent<TextMeshProUGUI>();
                t.transform.SetParent(Parent != null ? Parent : Canvas.transform, true);
                t.fontSizeMin = 8;
                t.fontSizeMax = 300;
                return t;
            }
        }
    }

    private Image _image;
    public Image Image
    {
        get
        {
            return _image = _image != null ? _image : SetUpImage();
            Image SetUpImage()
            {
                Image i = new GameObject(Name + nameof(Image)).AddComponent<Image>();
                i.transform.SetParent(Parent != null ? Parent : Canvas.transform, true);
                i.sprite = null;
                return i;
            }
        }
    }

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            return _canvas != null ? _canvas : _canvas = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas canvas = new GameObject(Name + nameof(Canvas)).AddComponent<Canvas>();
                canvas.transform.SetParent(GO.transform, false);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 0;
                if (_canvasScaler == null) _canvasScaler = SetUpCanvasScaler(canvas);
                return canvas;
            }
        }
    }

    private CanvasScaler _canvasScaler;
    public CanvasScaler CanvasScaler
    {
        get
        {
            if (_canvasScaler == null) _canvasScaler = SetUpCanvasScaler(Canvas);
            return _canvasScaler;
        }
    }

    CanvasScaler SetUpCanvasScaler(Canvas canvas)
    {
        if (canvas.gameObject.TryGetComponent(out CanvasScaler ca)) return ca;
        CanvasScaler cs = canvas.gameObject.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.matchWidthOrHeight = 1;
        cs.referenceResolution = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);
        return cs;
    }

    public Card CreateChild(string name, Transform parent)
    {
        Children = Children == null ? new Card[1] { NewCard() } : AddNewCard();
        return Children[^1];

        Card NewCard() => new(name, this, parent);


        Card[] AddNewCard()
        {
            Card[] temp = new Card[Children.Length + 1];
            Children.CopyTo(temp, 0);
            temp[^1] = NewCard();
            return temp;
        }
    }

    public Card CreateChild(string name, Transform parent, Canvas _)
    {
        Children = Children == null ? new Card[1] { NewCard() } : AddNewCard();
        return Children[^1];

        Card NewCard() => new(name, this, parent, Canvas);

        Card[] AddNewCard()
        {
            Card[] temp = new Card[Children.Length + 1];
            Children.CopyTo(temp, 0);
            temp[^1] = NewCard();
            return temp;
        }
    }

    public Card SetClickable(Clickable clickable) { Clickable = clickable; return this; }
}


