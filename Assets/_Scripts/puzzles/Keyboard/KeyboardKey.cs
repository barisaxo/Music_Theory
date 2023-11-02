using UnityEngine;
using MusicTheory.Keys;

public enum KeyColor { White, Black }
public class KeyboardKey
{
    public Card Card;
    public GameObject Go;
    public readonly KeyboardNoteName KeyboardNoteName;
    public readonly Key Key;
    public readonly SpriteRenderer SR;
    public readonly KeyColor KeyColor;

    public static Color KeyboardBlack = new(0, 0, 0, 1.6f);
    public static Color KeyboardWhite = new(1, 1, 1, 1.6f);

    public KeyboardKey(KeyboardNoteName key, KeyColor c, Vector3 loc, Transform parent)
    {
        Go = new GameObject(key.ToString() + " " + nameof(Key));
        Go.transform.SetParent(parent);
        Go.transform.position = loc;
        Go.layer = 5;
        KeyColor = c;
        KeyboardNoteName = key;
        Key = key.NoteNameToKey();
        SR = Go.AddComponent<SpriteRenderer>();
        SR.sprite = Assets.White;
        SR.color = KeyColor == KeyColor.White ? KeyboardWhite : KeyboardBlack;
        //Go.AddComponent<BoxCollider2D>();
        Go.AddComponent<Clickable>();

        if (KeyColor == KeyColor.White)
        {
            Go.transform.localScale = new Vector3(.475f, 2.5f, 1) * Cam.Io.UICamera.aspect;
        }
        else
        {
            Go.transform.position = loc + .5f * Cam.Io.UICamera.aspect * Vector3.up;
            Go.transform.localScale = new Vector3(.35f, 1.5f, 1f) * Cam.Io.UICamera.aspect;
        }
    }
}