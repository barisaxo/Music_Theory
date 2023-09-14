using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Keys;

namespace MusicTheory
{
    public class DiatonicKeyCircle : Circle
    {
        public DiatonicKeyCircle(string name, float radius, Vector2 pos) : base(name, radius, pos) { }


        private Key _key = new C();
        public Key Key
        {
            get => _key;
            set { _key = value; }
        }




    }
}