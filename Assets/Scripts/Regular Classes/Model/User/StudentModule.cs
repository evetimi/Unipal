using System.Collections;
using System.Collections.Generic;
using Unipal.Modules;
using UnityEngine;

namespace Unipal.User {
    public class StudentModule {
        private Module _module;
        private MarkList _markList;

        public Module Module => _module;
        public MarkList MarkList => _markList;

        public StudentModule(Module module) {
            _module = module;
            _markList = new();
        }

        public void AddMark(MarkType markType, int mark) 
        {
            _markList.AddMark(markType, mark);
        }

        public void ChangeMark(MarkType markType, int newMark)
        {
            _markList.ChangeMark(markType, newMark);
        }

        public void RemoveMark(MarkType markType)
        {
            _markList.RemoveMark(markType);
        }
    }
}
