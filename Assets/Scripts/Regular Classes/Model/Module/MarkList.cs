using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unipal.Modules {
    public class MarkList : MonoBehaviour {
        private Dictionary<MarkType, int> _marks;
        public event EventHandler<MarkChangedEventArgs> MarkChanged;

        public MarkList() {
            _marks = new();
        }

        public bool IsMarkExist(MarkType type) {
            return _marks.ContainsKey(type);
        }

        public void AddMark(MarkType markType, int mark) 
        {
            if (mark < 0 || mark > 100)
            {
                // throw new ArgumentOutOfRangeException("mark", "Mark must be between 0 and 100");
                return;
            }

            // _marks.Add(new Dictionary<string, object> 
            // {
            //     { "type", markType },
            //     { "score", mark }
            // });

            _marks.Add(markType, mark);

            OnMarkChanged(markType, mark); 
        }

        public void ChangeMark(MarkType markType, int newMark)
        {
            // if (index < 0 || index >= _marks.Count)
            // {
            //     throw new ArgumentOutOfRangeException("index", "Index out of range");
            // }
            if (!IsMarkExist(markType) || newMark < 0 || newMark > 100)
            {
                // throw new ArgumentOutOfRangeException("newMark", "Mark must be between 0 and 100");
                return;
            }

            // var oldMark = (int)_marks[index]["score"];
            // _marks[index]["type"] = newMarkType;
            // _marks[index]["score"] = newMark;
            var oldMark = _marks[markType];
            _marks[markType] = newMark;

            OnMarkChanged(markType, newMark, oldMark);
        }

        public void RemoveMark(MarkType markType)
        {
            // if (index < 0 || index >= _marks.Count)
            // {
            //     throw new ArgumentOutOfRangeException("index", "Index out of range");
            // }

            // MarkType removedMarkType = (MarkType)_marks[index]["type"];
            // int removedMark = (int)_marks[index]["score"];
            // _marks.RemoveAt(index);

            if (!IsMarkExist(markType)) {
                return;
            }
            
            int removedMark = _marks[markType];
            _marks.Remove(markType);

            OnMarkChanged(markType, removedMark, -1); 
        }

        private void OnMarkChanged(MarkType markType, int newMark, int oldMark = -1)
        {
            MarkChanged?.Invoke(this, new MarkChangedEventArgs(markType, oldMark, newMark));
        }
    }

    // Event argument class
    public class MarkChangedEventArgs : EventArgs {
        public MarkType MarkType { get; }
        public int OldMark { get; }
        public int NewMark { get; }

        public MarkChangedEventArgs(MarkType markType, int oldMark, int newMark) {
            MarkType = markType;
            OldMark = oldMark;
            NewMark = newMark;
        }
    }
}
