using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.User {
    /// <summary>
    /// This class contains module and mark list of the module. Showing that one module can have multiple assignments and marks
    /// </summary>
    public class StudentModule {
        private Module _module;
        private MarkList _markList;

        public Module Module => _module;
        public MarkList MarkList => _markList;

        public StudentModule(Module module) {
            _module = module;
            _markList = new();
        }

        /// <summary>
        /// Check if the mark exist for this student
        /// </summary>
        /// <param name="assignment">Assignment to get the mark from</param>
        /// <returns>TRUE if the mark of the assignment exists</returns>
        public bool IsMarkExist(Assignment assignment) {
            return _markList.IsMarkExist(assignment);
        }

        /// <summary>
        /// Get the mark of the assignment
        /// </summary>
        /// <param name="assignment">Assignment to get the mark from</param>
        /// <returns>Mark of the assignment, -1 if the mark is not exist</returns>
        public int GetMark(Assignment assignment) {
            return _markList.GetMark(assignment);
        }

        /// <summary>
        /// Add the mark to the assignment
        /// </summary>
        /// <param name="assignment">Assignment to add</param>
        /// <param name="mark">Mark to add to the assignment</param>
        public void AddMark(Assignment assignment, int mark)  {
            _markList.AddMark(assignment, mark);
        }

        /// <summary>
        /// Change the existing mark of the assignment
        /// </summary>
        /// <param name="assignment">Assignment to change mark</param>
        /// <param name="newMark">The new mark of the assignment</param>
        public void ChangeMark(Assignment assignment, int newMark) {
            _markList.ChangeMark(assignment, newMark);
        }

        /// <summary>
        /// Remove the existing mark from the assignment
        /// </summary>
        /// <param name="assignment">Assignment's mark to remove</param>
        public void RemoveMark(Assignment assignment) {
            _markList.RemoveMark(assignment);
        }
    }
}
