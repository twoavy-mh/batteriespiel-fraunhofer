using System;
using System.Collections.Generic;

namespace Helpers
{
    public class MultiLang<T>
    {
        private readonly Dictionary<string, T> _langSet = new Dictionary<string, T>();
        
        public MultiLang(T de, T en)
        {
            _langSet.Add("de", de);
            _langSet.Add("en", en);
        }
        

        public T GetLang(Language lang)
        {
            return _langSet[lang == Language.De ? "de" : "en"];
        }
    }
    
    public enum Language
    {
        De,
        En
    }
}