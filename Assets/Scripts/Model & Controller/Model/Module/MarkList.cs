using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.EventHandlers;
using UnityEngine;

namespace Unipal.Model.Modules {
    /// <summary>
    /// MarkList contains the list of mark associated with the assignment, this class stands for the helper of adding, removing or changing marks.
    /// The class can be added as student's field to represent student's marks.
    /// </summary>
    public class MarkList {
        private Dictionary<Assignment, int> _marks;
        public event EventHandler<MarkChangedEventArgs> OnMarkChanged;

        public MarkList() {
            _marks = new();
        }

        /// <summary>
        /// Check if the mark exist for this student
        /// </summary>
        /// <param name="assignment">Assignment to get the mark from</param>
        /// <returns>TRUE if the mark of the assignment exists</returns>
        public bool IsMarkExist(Assignment assignment) {
            return _marks.ContainsKey(assignment);
        }

        /// <summary>
        /// Get the mark of the assignment
        /// </summary>
        /// <param name="assignment">Assignment to get the mark from</param>
        /// <returns>Mark of the assignment, -1 if the mark is not exist</returns>
        public int GetMark(Assignment assignment) {
            if (!IsMarkExist(assignment)) {
                return -1;
            }
            return _marks[assignment];
        }

        /// <summary>
        /// Add the mark to the assignment
        /// </summary>
        /// <param name="assignment">Assignment to add</param>
        /// <param name="mark">Mark to add to the assignment</param>
        public void AddMark(Assignment assignment, int mark)  {
            if (mark < 0 || mark > 100) {
                return;
            }
            _marks.Add(assignment, mark);

            MarkChanged(assignment, mark); 
        }

        /// <summary>
        /// Change the existing mark of the assignment
        /// </summary>
        /// <param name="assignment">Assignment to change mark</param>
        /// <param name="newMark">The new mark of the assignment</param>
        public void ChangeMark(Assignment assignment, int newMark) {
            if (!IsMarkExist(assignment) || newMark < 0 || newMark > 100) {
                return;
            }
            var oldMark = _marks[assignment];
            _marks[assignment] = newMark;

            MarkChanged(assignment, newMark, oldMark);
        }

        /// <summary>
        /// Remove the existing mark from the assignment
        /// </summary>
        /// <param name="assignment">Assignment's mark to remove</param>
        public void RemoveMark(Assignment assignment) {
            if (!IsMarkExist(assignment)) {
                return;
            }
            
            int removedMark = _marks[assignment];
            _marks.Remove(assignment);

            MarkChanged(assignment, removedMark, -1);
        }

        /// <summary>
        /// Call the event of adding/changing the mark
        /// </summary>
        /// <param name="assignment">Assignment that has gotten the changes</param>
        /// <param name="newMark">New mark of the assignment</param>
        /// <param name="oldMark">Old mark of the assignment</param>
        private void MarkChanged(Assignment assignment, int newMark, int oldMark = -1) {
            OnMarkChanged?.Invoke(this, new MarkChangedEventArgs(assignment, oldMark, newMark));
        }
    }
}
