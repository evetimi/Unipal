using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unipal.Model.Assignments {
    public class Assignment {
        private string _id;
        private string _name;
        private string _description;

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;

        public Assignment(string id, string name, string description) {
            _id = id;
            _name = name;
            _description = description;
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj is not Assignment assignment) return false;
            if (_id.Equals(assignment._id)) return true;

            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
