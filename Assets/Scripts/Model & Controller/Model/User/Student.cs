using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.User {
    public class Student : UserAccount {
        private List<StudentModule> _attendedModules;

        public List<StudentModule> AttendedModules => _attendedModules;

        public Student(string id, string name, string surname, string email, string gender, DateTime dob, string address, string phoneNumber) : base(id, name, surname, email, gender, dob, address, phoneNumber) {
            _attendedModules = new();
        }

        /// <summary>
        /// Add new module to this student
        /// </summary>
        /// <param name="newModule">New module to be added to this student</param>
        public void AddNewModule(Module newModule) {
            _attendedModules.ForEach((existedStudentModule) => {
                if (newModule.Equals(existedStudentModule.Module)) return;
            });

            _attendedModules.Add(new(newModule));
        }

        /// <summary>
        /// Remove the existing module from this student
        /// </summary>
        /// <param name="module">Module to be removed from this student</param>
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
