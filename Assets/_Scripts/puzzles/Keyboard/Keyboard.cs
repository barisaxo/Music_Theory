using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Keyboard
{
    public Keyboard(int numOfNotes, KeyboardNoteName bottomNote)
    {
        TwoOctaveKeyboard();
        SelectedKeys = new KeyboardKey[numOfNotes];
        SelectedKeys[0] = Keys[(int)bottomNote];
        LockBottomKey = true;
        SetKeyColors();
    }

    public Keyboard(int numOfKeys)
    {
        TwoOctaveKeyboard();
        SelectedKeys = new KeyboardKey[numOfKeys];
        SetKeyColors();
    }

    public void SelfDestruct()
    {
        GameObject.Destroy(Parent);
    }

    private GameObject _parent;
    public GameObject Parent => _parent ? _parent : _parent = new(nameof(Keyboard)) { layer = 5 };

    AudioParser AudioParser => Audio.AudioManager.Io.AudioParser;

    public List<SpriteRenderer> WhiteKeySRs = new List<SpriteRenderer>();
    public List<SpriteRenderer> BlackKeySRs = new List<SpriteRenderer>();
    public List<SpriteRenderer> twoOctave = new List<SpriteRenderer>();
    public List<KeyboardKey> Keys = new List<KeyboardKey>();

    public Color KeyboardBlack = new Color(0, 0, 0, .6f);
    public Color KeyboardWhite = new Color(.6f, .6f, .6f, .6f);
    public Color KeyboardWhiteHL = new Color(.75f, .75f, .15f, .55f);
    public Color KeyboardBlackHL = new Color(.65f, .65f, .1f, .55f);
    public Color KeyboardWhiteAHL = new Color(.25f, .25f, .65f, .65f);
    public Color KeyboardBlackAHL = new Color(.15f, .15f, .55f, .65f);
    public Color KeyboardWhiteAHL2 = new Color(.65f, .15f, .1f, .55f);
    public Color KeyboardBlackAHL2 = new Color(.55f, .15f, .15f, .55f);

    public bool LockBottomKey;
    public KeyboardKey[] SelectedKeys;

    public virtual void InteractWithKey(GameObject go)
    {
        foreach (KeyboardKey key in Keys)
        {
            if (key.Go == go)
            {
                Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
                if (LockBottomKey) LockedBottomKey(key);
                else UnlockedBottomKey(key);
                return;
            }
        }
        throw new System.ArgumentOutOfRangeException(go.name + " was not found in Keys!");
    }

    private void UnlockedBottomKey(KeyboardKey key)
    {
        if (SelectedKeys.Length == 1)
        {
            SelectedKeys[0] = SelectedKeys[0] == key ? null : key;

            ////if (key != null)
            //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));

            SetKeyColors();
            return;
        }

        for (int i = 0; i < SelectedKeys.Length; i++)
        {
            if (SelectedKeys[i] == key)
            {
                SelectedKeys[i] = null;
                SetKeyColors();
                return;
            }
        }

        for (int i = 0; i < SelectedKeys.Length; i++)
        {
            if (SelectedKeys[i] == null)
            {
                SelectedKeys[i] = key;
                //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
                SetKeyColors();
                return;
            }
        }
    }

    private void LockedBottomKey(KeyboardKey key)
    {
        if (SelectedKeys.Length <= 1)
        {
            //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
            return;
        }

        if (key.KeyboardNoteName <= SelectedKeys[0].KeyboardNoteName)
        {
            //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
            Debug.Log("you cannot select notes below the bottom note");//todo make this live feedback
            return;
        }

        if (SelectedKeys.Length == 2)
        {
            SelectedKeys[1] = SelectedKeys[1] == key ? null : key;

            //if (SelectedKeys[1] != null)
            //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
            //else Audio.AudioManager.Io.KBAudio.Stop();

            SetKeyColors();
            return;
        }

        for (int i = 1; i < SelectedKeys.Length; i++)
        {
            if (SelectedKeys[i] == key)
            {
                SelectedKeys[i] = null;
                //Audio.AudioManager.Io.KBAudio.Stop();
                //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
                SetKeyColors();
                return;
            }
        }

        for (int i = 1; i < SelectedKeys.Length; i++)
        {
            if (SelectedKeys[i] == null)
            {
                SelectedKeys[i] = key;
                //Audio.AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));
                SetKeyColors();
                return;
            }
        }
    }

    public virtual void SetKeyColors()
    {
        foreach (KeyboardKey key in Keys)
        {
            bool isSelected = false;
            foreach (KeyboardKey selectedKey in SelectedKeys) if (key == selectedKey) isSelected = true;
            if (!isSelected) key.SR.color = key.KeyColor == KeyColor.White ? KeyboardWhite : KeyboardBlack;
            else key.SR.color = key.KeyColor == KeyColor.White ? KeyboardWhiteAHL2 : KeyboardBlackAHL2;
        }

        if (SelectedKeys[0] != null)
            SelectedKeys[0].SR.color = SelectedKeys[0].KeyColor == KeyColor.White ? KeyboardWhiteAHL : KeyboardBlackAHL;
    }

    #region TwoOctaveKeyboard
    void TwoOctaveKeyboard()
    {
        //for (KeyboardNoteName key = 0; key < (KeyboardNoteName)(Enum.GetNames(typeof(KeyboardNoteName)).Length * .5f + 1); key++)
        for (KeyboardNoteName key = 0; key < (KeyboardNoteName)(Enum.GetNames(typeof(KeyboardNoteName)).Length); key++)
        {
            Keys.Add(new KeyboardKey(key, GetKeyColor(key), KeyPos(key) * Cam.Io.UICamera.aspect, Parent.transform));
        }

        Vector3 KeyPos(KeyboardNoteName key) => key switch
        {
            KeyboardNoteName.C3 => new Vector3(-3.5f, 0, 1F),
            KeyboardNoteName.Db3 => new Vector3(-3.3f, 0, 0),
            KeyboardNoteName.D3 => new Vector3(-3f, 0, 1F),
            KeyboardNoteName.Eb3 => new Vector3(-2.7f, 0, 0),
            KeyboardNoteName.E3 => new Vector3(-2.5f, 0, 1F),
            KeyboardNoteName.F3 => new Vector3(-2f, 0, 1F),
            KeyboardNoteName.Gb3 => new Vector3(-1.8f, 0, 0),
            KeyboardNoteName.G3 => new Vector3(-1.5f, 0, 1F),
            KeyboardNoteName.Ab3 => new Vector3(-1.25f, 0, 0),
            KeyboardNoteName.A3 => new Vector3(-1f, 0, 1F),
            KeyboardNoteName.Bb3 => new Vector3(-.7f, 0, 0),
            KeyboardNoteName.B3 => new Vector3(-.5f, 0, 1F),

            KeyboardNoteName.C4 => new Vector3(0, 0, 1F),
            KeyboardNoteName.Db4 => new Vector3(.2f, 0, 0),
            KeyboardNoteName.D4 => new Vector3(.5f, 0, 1F),
            KeyboardNoteName.Eb4 => new Vector3(.8f, 0, 0),
            KeyboardNoteName.E4 => new Vector3(1f, 0, 1F),
            KeyboardNoteName.F4 => new Vector3(1.5f, 0, 1F),
            KeyboardNoteName.Gb4 => new Vector3(1.7f, 0, 0),
            KeyboardNoteName.G4 => new Vector3(2f, 0, 1F),
            KeyboardNoteName.Ab4 => new Vector3(2.25f, 0, 0),
            KeyboardNoteName.A4 => new Vector3(2.5f, 0, 1F),
            KeyboardNoteName.Bb4 => new Vector3(2.8f, 0, 0),
            KeyboardNoteName.B4 => new Vector3(3f, 0, 1F),

            KeyboardNoteName.C5 => new Vector3(3.5f, 0, 1F),
            _ => throw new ArgumentOutOfRangeException(key.ToString())
        };

        KeyColor GetKeyColor(KeyboardNoteName key) => key switch
        {
            KeyboardNoteName.Db3 or KeyboardNoteName.Eb3 or KeyboardNoteName.Gb3 or KeyboardNoteName.Ab3 or KeyboardNoteName.Bb3 or
            KeyboardNoteName.Db4 or KeyboardNoteName.Eb4 or KeyboardNoteName.Gb4 or KeyboardNoteName.Ab4 or KeyboardNoteName.Bb4 =>
                KeyColor.Black,
            _ => KeyColor.White,
        };

        #endregion

    }
}

public enum KeyboardNoteName
{
    C3, Db3, D3, Eb3, E3, F3, Gb3, G3, Ab3, A3, Bb3, B3,
    C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4, C5,
}

public static class KeyboardSystems
{
    public static KeyboardNoteName GetKeyboardNoteName(this MusicTheory.Keys.Key key) => key.Id switch
    {
        0 => KeyboardNoteName.C3,
        1 => KeyboardNoteName.Db3,
        2 => KeyboardNoteName.D3,
        3 => KeyboardNoteName.Eb3,
        4 => KeyboardNoteName.E3,
        5 => KeyboardNoteName.F3,
        6 => KeyboardNoteName.Gb3,
        7 => KeyboardNoteName.G3,
        8 => KeyboardNoteName.Ab3,
        9 => KeyboardNoteName.A3,
        10 => KeyboardNoteName.Bb3,
        11 => KeyboardNoteName.B3,
        _ => throw new System.ArgumentOutOfRangeException()
    };

    public static MusicTheory.Keys.Key NoteNameToKey(this KeyboardNoteName key) => key switch
    {
        KeyboardNoteName.C3 => new MusicTheory.Keys.C(),
        KeyboardNoteName.Db3 => new MusicTheory.Keys.Db(),
        KeyboardNoteName.D3 => new MusicTheory.Keys.D(),
        KeyboardNoteName.Eb3 => new MusicTheory.Keys.Eb(),
        KeyboardNoteName.E3 => new MusicTheory.Keys.E(),
        KeyboardNoteName.F3 => new MusicTheory.Keys.F(),
        KeyboardNoteName.Gb3 => new MusicTheory.Keys.Gb(),
        KeyboardNoteName.G3 => new MusicTheory.Keys.G(),
        KeyboardNoteName.Ab3 => new MusicTheory.Keys.Ab(),
        KeyboardNoteName.A3 => new MusicTheory.Keys.A(),
        KeyboardNoteName.Bb3 => new MusicTheory.Keys.Bb(),
        KeyboardNoteName.B3 => new MusicTheory.Keys.B(),

        KeyboardNoteName.C4 => new MusicTheory.Keys.C(),
        KeyboardNoteName.Db4 => new MusicTheory.Keys.Db(),
        KeyboardNoteName.D4 => new MusicTheory.Keys.D(),
        KeyboardNoteName.Eb4 => new MusicTheory.Keys.Eb(),
        KeyboardNoteName.E4 => new MusicTheory.Keys.E(),
        KeyboardNoteName.F4 => new MusicTheory.Keys.F(),
        KeyboardNoteName.Gb4 => new MusicTheory.Keys.Gb(),
        KeyboardNoteName.G4 => new MusicTheory.Keys.G(),
        KeyboardNoteName.Ab4 => new MusicTheory.Keys.Ab(),
        KeyboardNoteName.A4 => new MusicTheory.Keys.A(),
        KeyboardNoteName.Bb4 => new MusicTheory.Keys.Bb(),
        KeyboardNoteName.B4 => new MusicTheory.Keys.B(),

        KeyboardNoteName.C5 => new MusicTheory.Keys.C(),
        _ => throw new System.ArgumentOutOfRangeException()
    };
}