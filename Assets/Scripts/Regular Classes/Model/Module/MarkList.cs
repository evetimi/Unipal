using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.EventHandlers;
using UnityEngine;

namespace Unipal.Model.Modules {
    public class MarkList : MonoBehaviour {
        private Dictionary<Assignment, int> _marks;
        public event EventHandler<MarkChangedEventArgs> OnMarkChanged;

        public MarkList() {
            _marks = new();
        }

        public bool IsMarkExist(Assignment assignment) {
            return _marks.ContainsKey(assignment);
        }

        public void AddMark(Assignment assignment, int mark) 
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

            _marks.Add(assignment, mark);

            MarkChanged(assignment, mark); 
        }

        public void ChangeMark(Assignment assignment, int newMark)
        {
            // if (index < 0 || index >= _marks.Count)
            // {
            //     throw new ArgumentOutOfRangeException("index", "Index out of range");
            // }
            if (!IsMarkExist(assignment) || newMark < 0 || newMark > 100)
            {
                // throw new ArgumentOutOfRangeException("newMark", "Mark must be between 0 and 100");
                return;
            }

            // var oldMark = (int)_marks[index]["score"];
            // _marks[index]["type"] = newMarkType;
            // _marks[index]["score"] = newMark;
            var oldMark = _marks[assignment];
            _marks[assignment] = newMark;

            MarkChanged(assignment, newMark, oldMark);
        }

        public void RemoveMark(Assignment assignment)
        {
            // if (index < 0 || index >= _marks.Count)
            // {
            //     throw new ArgumentOutOfRangeException("index", "Index out of range");
            // }

            // MarkType removedMarkType = (MarkType)_marks[index]["type"];
            // int removedMark = (int)_marks[index]["score"];
            // _marks.RemoveAt(index);

            if (!IsMarkExist(assignment)) {
                return;
            }
            
            int removedMark = _marks[assignment];
            _marks.Remove(assignment);

            MarkChanged(assignment, removedMark, -1); 
        }

        private void MarkChanged(Assignment assignment, int newMark, int oldMark = -1)
        {
            OnMarkChanged?.Invoke(this, new MarkChangedEventArgs(assignment, oldMark, newMark));
        }
    }
}
