using System.Collections;
using System.Collections.Generic;
using Unipal.Modules;
using UnityEngine;

namespace Unipal.User {
    public class Student : UserAccount {
        private List<Module> attendedModules;

        public Student(string id, string name) : base(id, name) { }
    }
}
