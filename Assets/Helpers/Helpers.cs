using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public enum Tailwind
    {
        Blue1,
        Blue2,
        Blue3,
        Blue4,
        Blue5,
        Violet1,
        Violet2,
        Violet3,
        Violet4,
        Red1,
        Red2,
        Red3,
        Orange1,
        Orange2,
        Orange3,
        Orange4,
        Yellow1,
        Yellow2,
        Yellow3,
        Yellow4,
        Green1,
        Green2,
        Green3,
        Green4,
        Green5,
        Azure1,
        Azure2,
        Azure3,
        Mud1,
        Mud2,
        Mud3,
        Sky1,
        Sky2,
        Sky3,
        Gray1,
    }

    class Settings
    {
        public static Dictionary<Tailwind, Color> ColorMap = new Dictionary<Tailwind, Color>
        {
            { Tailwind.Blue1, new Color(212 / 255f, 230 / 255f, 244 / 255f, 1) },
            { Tailwind.Blue2, new Color(136 / 255f, 188 / 255f, 226 / 255f, 1) },
            { Tailwind.Blue3, new Color(31 / 255f, 130 / 255f, 192 / 255f, 1) },
            { Tailwind.Blue4, new Color(0 / 255f, 90 / 255f, 148 / 255f, 1) },
            { Tailwind.Blue5, new Color(0 / 255f, 52 / 255f, 107 / 255f, 1) },
            { Tailwind.Violet1, new Color(199 / 255f, 193 / 255f, 226 / 255f, 1) },
            { Tailwind.Violet2, new Color(144 / 255f, 133 / 255f, 186 / 255f, 1) },
            { Tailwind.Violet3, new Color(57 / 255f, 55 / 255f, 107 / 255f, 1) },
            { Tailwind.Violet1, new Color(0 / 255f, 52 / 255f, 107 / 255f, 1) },
        };

        public static float MovementSpeed = 8f;
    }

    class Utility
    {
        public static T GetRandom<T>(T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static IEnumerator AnimateAnything(float duration, float start, float end, AnimationProgress func,
            Action finalCallback = null)
        {
            float counter = 0;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                func.Invoke(counter / duration, start, end);
                yield return null;
            }

            if (finalCallback != null)
            {
                finalCallback.Invoke();
            }
        }

        public static Vector3[] SpriteLocalToWorld(Transform transform, Sprite sp)
        {
            Vector3 pos = transform.position;
            Vector3[] array = new Vector3[2];
            //top left
            array[0] = pos + sp.bounds.min;
            // Bottom right
            array[1] = pos + sp.bounds.max;
            return array;
        }

        public delegate void AnimationProgress(float progress, float start, float end);
    }
}