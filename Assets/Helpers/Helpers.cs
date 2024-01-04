using System;
using System.Collections;
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

        public static float MovementSpeed = 8f;
    }

    class Utility
    {
        public static T GetRandom<T>(T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
        
        public static IEnumerator AnimateAnything(float duration, float start, float end, AnimationProgress func, Action finalCallback = null)
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