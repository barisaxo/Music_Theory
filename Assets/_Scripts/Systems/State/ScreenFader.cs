using UnityEngine;
using UnityEngine.UI;

sealed class ScreenFader
{
    public ScreenFader()
    {
        _ = Screen;
    }

    public void SelfDestruct()
    {
        UnityEngine.Object.DestroyImmediate(Parent.gameObject);
    }

    private Transform _parent;
    public Transform Parent => _parent != null ? _parent : _parent = new GameObject(nameof(ScreenFader)).transform;

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            return _canvas != null ? _canvas : _canvas = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas c = new GameObject(nameof(Canvas)).AddComponent<Canvas>();
                c.transform.SetParent(Parent);
                c.renderMode = RenderMode.ScreenSpaceOverlay;
                c.sortingOrder = 9001;//KAKAROT!
                return c;
            }
        }
    }

    private Image _screen;
    public Image Screen
    {
        get
        {
            return _screen != null ? _screen : _screen = SetUpImage();
            Image SetUpImage()
            {
                Image go = new GameObject(nameof(Screen)).AddComponent<Image>();
                go.transform.SetParent(Canvas.transform);
                go.color = Color.clear;
                go.rectTransform.localScale = Vector3.one * 200;
                return go;
            }
        }
    }

}
