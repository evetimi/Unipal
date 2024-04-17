using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unipal.Modules {
    public class Module {
        private string id;
        private string name;
        private string description;

        public Module(string id, string name, string description) {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj is not Module module) return false;
            if (id.Equals(module.id)) return true;

            return false;
        }
    }
}
