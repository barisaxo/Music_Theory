using System.Collections.Generic;
using UnityEngine;
using System;


namespace Menus
{
    public abstract class Menu<T, TMenu> : IMenu<T> where T : DataEnum, new() where TMenu : Menu<T, TMenu>
    {
        protected Menu(string name) { Name = name; }
        private readonly string Name;
        private Transform _parent;
        protected Transform Parent => _parent != null ? _parent : _parent = new GameObject(Name).transform;
        private MenuItem<T> _selection { get; set; }
        public MenuItem<T> Selection { get => _selection; set { _selection = value; ItemDescriptionText = value.Item.Description; } }
        public T[] DataItems => Enumeration.All<T>();
        private MenuItem<T>[] _menuItems;
        public MenuItem<T>[] MenuItems => _menuItems ??= this.SetUpMenuCards(Parent, Style);
        public virtual MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;
        public string ItemDescriptionText { set => ItemDescription.TMP.text = value; }
        private Card _itemDescription;
        public Card ItemDescription => _itemDescription ??= new Card(nameof(ItemDescription), Parent)
            .SetFontScale(.6f, .6f)
            .SetTMPPosition(0, -Cam.UIOrthoY * .5f)
            .SetTMPSize(Cam.UIOrthoX * 1.7f, 1)
            .AllowWordWrap(false)
            .AutoSizeFont(true);

        public virtual Menu<T, TMenu> Initialize()
        {
            return Initialize(null);
        }

        public virtual Menu<T, TMenu> Initialize(T t)
        {
            Selection = MenuItems[t ?? 0];
            this.UpdateTextColors();
            this.ScrollMenuItems(Dir.Reset);
            return this;
        }

        public virtual void SelfDestruct()
        {
            UnityEngine.Object.Destroy(_parent.gameObject);
            Resources.UnloadUnusedAssets();
        }
    }

    public struct MenuItem<T> where T : DataEnum, new()
    {
        public T Item;
        public Card Card;

        public static int operator +(MenuItem<T> a, int b) => a.Item.Id + b;
        public static int operator -(MenuItem<T> a, int b) => a.Item.Id - b;
        public static int operator +(MenuItem<T> a, MenuItem<T> b) => a.Item.Id + b.Item.Id;
        public static int operator -(MenuItem<T> a, MenuItem<T> b) => a.Item.Id - b.Item.Id;

        public static bool operator ==(MenuItem<T> a, int b) => a.Item.Id == b;
        public static bool operator !=(MenuItem<T> a, int b) => a.Item.Id != b;
        public static bool operator ==(MenuItem<T> a, MenuItem<T> b) => a.Item.Id == b.Item.Id;
        public static bool operator !=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id != b.Item.Id;

        public static bool operator <=(MenuItem<T> a, int b) => a.Item.Id <= b;
        public static bool operator >=(MenuItem<T> a, int b) => a.Item.Id >= b;
        public static bool operator <=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id <= b.Item.Id;
        public static bool operator >=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id >= b.Item.Id;

        public static implicit operator int(MenuItem<T> a) => a.Item.Id;

        public override bool Equals(object obj) => obj is MenuItem<T> e && Item.Id == e.Item.Id;
        public override int GetHashCode() => HashCode.Combine(Item.Id);
    }
}