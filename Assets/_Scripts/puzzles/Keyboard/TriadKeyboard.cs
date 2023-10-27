//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TriadKeyboard : Keyboard
//{
//    public TriadKeyboard(KeyboardNoteName bottom) : base()
//    {
//        SelectedKey1 = Keys[(int)bottom];
//        SetKeyColors();
//    }

//    public override void InteractWithKey(KeyboardKey key)
//    {
//        if (SelectedKey2 == key)
//        {
//            SelectedKey2 = null;
//        }
//        else if (SelectedKey3 == key)
//        {
//            SelectedKey3 = null;
//        }
//        else if (SelectedKey2 == null)
//        {
//            SelectedKey2 = key;
//        }
//        else if (SelectedKey3 == null)
//        {
//            SelectedKey3 = key;
//        }

//        SetKeyColors();
//    }

//    public KeyboardKey SelectedKey3;


//    public override void SetKeyColors()
//    {
//        foreach (KeyboardKey key in Keys)
//        {
//            if (key == SelectedKey1)
//            { key.SR.color = key.KeyColor == KeyColor.White ? KeyboardWhiteAHL : KeyboardBlackAHL; }
//            else if (key == SelectedKey2)
//            { key.SR.color = key.KeyColor == KeyColor.White ? KeyboardWhiteAHL2 : KeyboardBlackAHL2; }
//            else if (key == SelectedKey3)
//            { key.SR.color = key.KeyColor == KeyColor.White ? KeyboardWhiteAHL2 : KeyboardBlackAHL2; }
//            else { key.SR.color = key.KeyColor == KeyColor.White ? KeyboardWhite : KeyboardBlack; }
//        }
//    }
//}

