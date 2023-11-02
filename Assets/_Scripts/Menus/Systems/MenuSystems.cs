using UnityEngine;
using TMPro;

namespace Menus
{
    public static class Menu_Systems
    {
        public static void ScrollMenuItems<T>(this IMenu<T> menu, Dir dir) where T : DataEnum, new()
        {
            switch (menu.Style)
            {
                case MenuLayoutStyle.Header:
                    switch (dir)
                    {
                        case Dir.Left: menu.Selection = PrevItem(); break;
                        case Dir.Right: menu.Selection = NextItem(); break;
                    }; break;

                case MenuLayoutStyle.TwoColumns:
                    switch (dir)
                    {
                        case Dir.Up: menu.Selection = PrevItem(); break;
                        case Dir.Down: menu.Selection = NextItem(); break;
                        case Dir.Left: if (menu.Style == MenuLayoutStyle.TwoColumns) menu.Selection = ScrollLeft(); break;
                        case Dir.Right: if (menu.Style == MenuLayoutStyle.TwoColumns) menu.Selection = ScrollRight(); break;
                    }; break;

                default:
                    switch (dir)
                    {
                        case Dir.Up: menu.Selection = PrevItem(); break;
                        case Dir.Down: menu.Selection = NextItem(); break;
                    }; break;
            }

            menu.UpdateTextColors();

            MenuItem<T> PrevItem() => menu.Style switch
            {
                MenuLayoutStyle.TwoColumns => (
                    menu.Selection == Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) ||
                    menu.Selection <= 0) ?
                        menu.Selection : menu.MenuItems[menu.Selection - 1],

                _ => menu.Selection <= 0 ? menu.Selection : menu.MenuItems[menu.Selection - 1]
            };

            MenuItem<T> NextItem() => menu.Style switch
            {
                MenuLayoutStyle.TwoColumns => (
                 menu.Selection == Mathf.FloorToInt((menu.MenuItems.Length - .5f) * .5f) ||
                 menu.Selection == menu.MenuItems[^1]) ?
                    menu.Selection : menu.MenuItems[menu.Selection + 1],

                _ => menu.Selection == menu.MenuItems[^1] ? menu.Selection : menu.MenuItems[menu.Selection + 1]
            };

            MenuItem<T> ScrollRight() => menu.Selection + Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) < menu.MenuItems.Length ?
                menu.MenuItems[menu.Selection + Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f)] : menu.Selection;

            MenuItem<T> ScrollLeft() => menu.Selection - Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) >= 0 ?
                menu.MenuItems[menu.Selection - Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f)] : menu.Selection;
        }

        public static void UpdateTextColors<T>(this IMenu<T> menu) where T : DataEnum, new()
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
            {
                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                {
                    menu.MenuItems[i].Card.SetTextColor(menu.Selection == i ? Color.white : Color.gray);
                }
            }
        }

        public static MenuItem<T>[] SetUpMenuCards<T>(this IMenu<T> menu, Transform parent, MenuLayoutStyle style) where T : DataEnum, new()
        {
            MenuItem<T>[] items = new MenuItem<T>[menu.DataItems.Length];
            for (int i = 0; i < menu.DataItems.Length; i++)
            {
                items[i] = new()
                {
                    Item = menu.DataItems[i],
                    Card = new Card(menu.DataItems[i].Name, parent)
                       .SetTextString(menu.DataItems[i].Name)
                       //.SetTMPSize(new Vector2(7, .7f))
                       .AutoSizeTextContainer(true)
                       .SetTMPPosition(GetPosition(i))
                       .AutoSizeFont(true)
                       .SetTextAlignment(style switch
                       {
                           MenuLayoutStyle.AlignRight => TextAlignmentOptions.Right,
                           MenuLayoutStyle.Header => TextAlignmentOptions.Center,
                           _ => TextAlignmentOptions.Left
                       })
                       .AllowWordWrap(false)
                       .SetTMPRectPivot(style switch
                       {
                           MenuLayoutStyle.AlignRight => new Vector2(1, .5f),
                           MenuLayoutStyle.Header => new Vector2(.5f, .5f),
                           _ => new Vector2(0, .5f)
                       })
                       .SetFontScale(.6f, .6f)
                       .TMPClickable()
                };
            }

            return items;

            Vector2 GetPosition(int i) => style switch
            {
                MenuLayoutStyle.AlignRight =>
                        new Vector2(Cam.UIOrthoX - 2.5f, 1.8f - (i * .8f)),

                MenuLayoutStyle.AlignLeft =>
                       new Vector2(-Cam.UIOrthoX + 2.5f, 1.8f - (i * .8f)),

                MenuLayoutStyle.TwoColumns =>
                new Vector2(i < menu.DataItems.Length * .5f ? -Cam.UIOrthoX + 2.5f : 2,
                -1.8f - (i % Mathf.CeilToInt(menu.DataItems.Length * .5f) * .8f) + (menu.DataItems.Length * .5f)),

                MenuLayoutStyle.Header => new Vector2(2 - Cam.UIOrthoX + (2 * (Cam.UIOrthoX - 2) / (menu.DataItems.Length - 1) * i),
                Cam.UIOrthoY - 1),

                _ => Vector2.zero,
            };
        }




    }

    public enum MenuLayoutStyle { AlignRight, TwoColumns, AlignLeft, Header }
}
