using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.User {
    public class Teacher : UserAccount {
        private List<Module> _teachModules;

        public List<Module> TeachModules => _teachModules;

        public Teacher(string id, string name, string surname, string email, string gender, DateTime dob, string address, string phoneNumber) : base(id, name, surname, email, gender, dob, address, phoneNumber) {
            _teachModules = new();
        }

        public void AddNewModule(Module newModule) {
            _teachModules.ForEach((existedModule) => {
                if (newModule.Equals(existedModule)) return;
            });

            _teachModules.Add(newModule);
        }

        public void RemoveModule(Module module) {
            _teachModules.ForEach((existedModule) => {
                if (module.Equals(existedModule)) {
                    _teachModules.Remove(existedModule);
                    return;
                }
            });
        }
    }
}
