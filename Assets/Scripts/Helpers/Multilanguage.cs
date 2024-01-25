using System;
using System.Collections.Generic;

namespace Helpers
{
    public class MultiLang
    {
        private readonly Dictionary<string, Dictionary<Language, string>>_langSet = new Dictionary<string, Dictionary<Language, string>>();
        
        public MultiLang()
        {
            
        }

        public void AddSingle(string key, Dictionary<Language, string> value)
        {
            _langSet.Add(key, value);
        }

        public void AddMultiple(string[] keys, Dictionary<Language, string>[] values)
        {
            if (keys.Length != values.Length)
            {
                throw new Exception("Keys and Values must have the same length");
            }

            for (int i = 0; i < keys.Length; i++)
            {
                _langSet.Add(keys[i], values[i]);
            }
        }

        public string Get(string key, Language lang)
        {
            return _langSet[key][lang];
        }
    }
    
    public enum Language
    {
        De,
        En
    }
}