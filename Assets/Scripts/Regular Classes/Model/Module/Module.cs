using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.EventHandlers;
using UnityEngine;

namespace Unipal.Model.Modules {
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

        public bool HasAssignment(Assignment assignment) {
            return _assignments.Contains(assignment);
        }

        public void AddAssignment(Assignment assignment) {
            _assignments.Add(assignment);
            AssignmentListUpdate(assignment, UpdateType.Add);
        }

        public bool RemoveAssignment(Assignment assignment) {
            if (_assignments.Remove(assignment)) {
                AssignmentListUpdate(assignment, UpdateType.Remove);
                return true;
            }
            return false;
        }

        public bool RemoveAssignment(int index) {
            if (index < 0 || index >= _assignments.Count) {
                return false;
            }

            Assignment assignment = _assignments[index];
            _assignments.RemoveAt(index);

            AssignmentListUpdate(assignment, UpdateType.Remove);
            return true;
        }

        private void AssignmentListUpdate(Assignment assignment, UpdateType updateType) {
            OnAssignmentListUpdated?.Invoke(this, new AssignmentListEventArgs(assignment, updateType));
        }
    }
}
