using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.User {
    public class Student : UserAccount {
        private List<StudentModule> _attendedModules;

        public List<StudentModule> AttendedModules => _attendedModules;

        public Student(string id, string name, string surname, string email, string address, string phoneNumber) : base(id, name, surname, email, address, phoneNumber) {
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
