using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class Clickable : MonoBehaviour { }

internal sealed class ClickFeedback
{
    #region INSTANCE
    private ClickFeedback() { }

    static private ClickFeedback Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static ClickFeedback _io;
        internal static ClickFeedback Io => _io ??= new ClickFeedback();
    }
    #endregion INSTANCE

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInit() => InputKey.MouseClickEvent += Io.Clicked;

    private readonly List<(SpriteRenderer sr, Color nativeColor)> spriteFeedback = new();
    private readonly List<(TextMeshProUGUI tmp, Color nativeColor)> tmpFeedback = new();
    private readonly List<(Image img, Color nativeColor)> imageFeedback = new();

    private GameObject _clickedGO;
    private GameObject ClickedGO
    {
        set
        {
            if (value == null && _clickedGO != null)
            {
                RestoreColor();
                _clickedGO = null;
            }

            else if (_clickedGO != value)
            {
                if (_clickedGO != null)
                {
                    RestoreColor();
                }

                _clickedGO = value;

                if (_clickedGO.TryGetComponent(out SpriteRenderer sr)) spriteFeedback.Add((sr, sr.color));
                if (_clickedGO.TryGetComponent(out TextMeshProUGUI tmp)) tmpFeedback.Add((tmp, tmp.color));
                if (_clickedGO.TryGetComponent(out Image img)) imageFeedback.Add((img, img.color));

                AlterColor();
            }
        }
    }

    private Click Clicked(MouseAction action, Vector3 mousePos)
    {
        if (action == MouseAction.LUp)
        {
            ClickedGO = null;
            return Click.Up;
        }

        RaycastHit2D hitUI = Physics2D.Raycast(mousePos, Vector2.zero);
        RaycastHit2D hitGO = Physics2D.Raycast(Cam.Io.UICamera.ScreenToWorldPoint(mousePos), Vector2.zero);
        RaycastHit2D hitNoUI = Physics2D.GetRayIntersection(Cam.Io.Camera.ScreenPointToRay(mousePos));

        if (hitUI.collider != null && hitUI.collider.gameObject.TryGetComponent<Clickable>(out _))
        {
            ClickedGO = hitUI.collider.gameObject;
        }
        else if (hitGO.collider != null)
        {
            ClickedGO = hitGO.collider.gameObject;
        }
        else if (hitNoUI.collider != null && hitNoUI.collider.gameObject.TryGetComponent<Clickable>(out _))
        {
            ClickedGO = hitNoUI.collider.gameObject;
        }
        else
        {
            ClickedGO = null;
        }

        return Click.Down;
    }

    private void AlterColor()
    {
        for (int i = 0; i < spriteFeedback.Count; i++)
        {
            spriteFeedback[i].sr.color *= new Color(1, 1, 1, .65f);
        }

        for (int i = 0; i < tmpFeedback.Count; i++)
        {
            tmpFeedback[i].tmp.color *= new Color(1, 1, 1, .65f);
        }

        for (int i = 0; i < imageFeedback.Count; i++)
        {
            imageFeedback[i].img.color *= new Color(1, 1, 1, .65f);
        }
    }

    private void RestoreColor()
    {
        for (int i = 0; i < spriteFeedback.Count; i++)
        {
            spriteFeedback[i].sr.color = spriteFeedback[i].nativeColor;
        }
        for (int i = 0; i < tmpFeedback.Count; i++)
        {
            tmpFeedback[i].tmp.color = tmpFeedback[i].nativeColor;
        }
        for (int i = 0; i < imageFeedback.Count; i++)
        {
            imageFeedback[i].img.color = imageFeedback[i].nativeColor;
        }
        spriteFeedback.Clear();
        tmpFeedback.Clear();
        imageFeedback.Clear();
    }
}

public static class ClickableSystems
{
    public static GameObject GetClickable(this Card c)
    {
        var clickable = c.GO.GetComponentInChildren<Clickable>();
        return clickable.gameObject != null ? clickable.gameObject : null;
    }
}