using System.Collections;
using System.Collections.Generic;
using Unipal.Modules;
using UnityEngine;

namespace Unipal.User {
    public class Student : UserAccount {
        private List<StudentModule> _attendedModules;

        public List<StudentModule> AttendedModules => _attendedModules;

        public Student(string id, string name) : base(id, name) {
            _attendedModules = new();
        }

        public void AddNewModule(Module newModule) {
            _attendedModules.ForEach((existedStudentModule) => {
                if (newModule.Equals(existedStudentModule.Module)) return;
            });

            _attendedModules.Add(new(newModule));
        }

        public void RemoveModule(Module module) {
            _attendedModules.ForEach((existedStudentModule) => {
                if (module.Equals(existedStudentModule.Module)) {
                    _attendedModules.Remove(existedStudentModule);
                    return;
                }
            });
        }
    }
}
