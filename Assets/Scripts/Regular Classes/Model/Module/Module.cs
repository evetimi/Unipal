using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unipal.Model.Modules {
    public class Module {
        private string _id;
        private string _name;
        private string _description;

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;

        public Module(string id, string name, string description) {
            _id = id;
            _name = name;
            _description = description;
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj is not Module module) return false;
            if (_id.Equals(module._id)) return true;

            return false;
        }
    }
}
