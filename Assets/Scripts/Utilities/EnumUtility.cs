using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
    public static class EnumUtility {
        public static T[] GetEnumArray<T>() where T : Enum {
            //if (cardColorsArray == null) {
            //    cardColorsArray = (CardColor[])Enum.GetValues(typeof(CardColor));
            //}

            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
