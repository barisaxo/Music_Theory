using System.Collections.Generic;
using UnityEngine;

namespace Menus
{
    public interface IMenu<T> where T : DataEnum, new()
    {
        public MenuItem<T> Selection { get; set; }
        public Card ItemDescription { get; }
        public MenuItem<T>[] MenuItems { get; }
        public MenuLayoutStyle Style { get; }
        public T[] DataItems { get; }
    }
}