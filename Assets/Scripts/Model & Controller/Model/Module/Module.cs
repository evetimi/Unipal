using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.EventHandlers;
using UnityEngine;

namespace Unipal.Model.Modules {
    /// <summary>
    /// Module to be created for this school. The system will ensure only unique module can be created, and no duplicate module existing.
    /// </summary>
    public class Module {
        private string _id;
        private string _name;
        private string _description;
        private List<Assignment> _assignments;

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public List<Assignment> Assignments => _assignments;
        public event EventHandler<AssignmentListEventArgs> OnAssignmentListUpdated;

        public Module(string id, string name, string description) {
            _id = id;
            _name = name;
            _description = description;
            _assignments = new List<Assignment>();
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj is not Module module) return false;
            if (_id.Equals(module._id)) return true;

            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        /// <summary>
        /// Check if the module has this assignment
        /// </summary>
        /// <param name="assignment">The assignment to check</param>
        /// <returns>TRUE if the assignment exists on this module</returns>
        public bool HasAssignment(Assignment assignment) {
            return _assignments.Contains(assignment);
        }

        /// <summary>
        /// Add new assignment to this module
        /// </summary>
        /// <param name="assignment">The assignment to add</param>
        public void AddAssignment(Assignment assignment) {
            _assignments.Add(assignment);
            AssignmentListUpdate(assignment, UpdateType.Add);
        }

        /// <summary>
        /// Remove existing assignment from this module
        /// </summary>
        /// <param name="assignment">The assignment to remove</param>
        /// <returns>TRUE if the removing process is successful</returns>
        public bool RemoveAssignment(Assignment assignment) {
            if (_assignments.Remove(assignment)) {
                AssignmentListUpdate(assignment, UpdateType.Remove);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove the existing assignment by using index
        /// </summary>
        /// <param name="index">Assignment index of the module</param>
        /// <returns>TRUE if the removing process is successful</returns>
        public bool RemoveAssignment(int index) {
            if (index < 0 || index >= _assignments.Count) {
                return false;
            }

            Assignment assignment = _assignments[index];
            _assignments.RemoveAt(index);

            AssignmentListUpdate(assignment, UpdateType.Remove);
            return true;
        }

        /// <summary>
        /// Call the event of adding/changing/removing the assignment
        /// </summary>
        /// <param name="assignment">Assignment that has done the process</param>
        /// <param name="updateType">Update type, is it add/change/remove?</param>
        private void AssignmentListUpdate(Assignment assignment, UpdateType updateType) {
            OnAssignmentListUpdated?.Invoke(this, new AssignmentListEventArgs(assignment, updateType));
        }
    }
}
