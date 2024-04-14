using System.Collections;
using System.Collections.Generic;
using Unipal.Modules;
using UnityEngine;

namespace Unipal.User {
    public class Student : UserAccount {
        private List<Module> attendedModules;

        public Student(string id, string name) : base(id, name) { }
    }

    class StudentModule {
        private Module module;
        private Dictionary<MarkType, int> marks;
    }

    enum MarkType {
        Workshop = 0,
        Exam = 1,
        Assignment = 2
    }
}
