using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public enum Tailwind
    {
        Blue
    }

    class Settings
    {
        public static Dictionary<Tailwind, Color> ColorMap = new Dictionary<Tailwind, Color>
        {
            { Tailwind.Blue, new Color(0, 52 / 255f, 107 / 255f, 1) }
        };

        public static float MovementSpeed = 10f;
    }
}