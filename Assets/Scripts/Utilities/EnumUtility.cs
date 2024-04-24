using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
    public static class EnumUtility {
        public static Dictionary<Type, Array> DictionaryOfEnums;

        public static T[] GetEnumArray<T>() where T : Enum {
            DictionaryOfEnums ??= new();

            if (DictionaryOfEnums.TryGetValue(typeof(T), out Array value)) {
                if (value is T[] v) {
                    return v;
                } else {
                    DictionaryOfEnums.Remove(typeof(T));
                }
            }

            Array newValue = Enum.GetValues(typeof(T));
            DictionaryOfEnums.Add(typeof(T), newValue);
            return (T[])newValue;
        }
    }
}
