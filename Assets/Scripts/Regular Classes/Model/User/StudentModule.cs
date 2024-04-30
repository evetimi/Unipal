using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.User {
    public class StudentModule {
        private Module _module;
        private MarkList _markList;

        public Module Module => _module;
        public MarkList MarkList => _markList;

        public StudentModule(Module module) {
            _module = module;
            _markList = new();
        }

        public void AddMark(Assignment assignment, int mark) 
        {
            _markList.AddMark(assignment, mark);
        }

        public void ChangeMark(Assignment assignment, int newMark)
        {
            _markList.ChangeMark(assignment, newMark);
        }

        public void RemoveMark(Assignment assignment)
        {
            _markList.RemoveMark(assignment);
        }
    }
}
