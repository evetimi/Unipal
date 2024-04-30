using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.EventHandlers {
    public class MarkChangedEventArgs : EventArgs {
        public Assignment Assignment { get; }
        public int OldMark { get; }
        public int NewMark { get; }

        public MarkChangedEventArgs(Assignment assignment, int oldMark, int newMark) {
            Assignment = assignment;
            OldMark = oldMark;
            NewMark = newMark;
        }
    }
}
