using System;
using System.Collections;
using System.Collections.Generic;
using Unipal.Model.Assignments;
using Unipal.Model.Modules;
using UnityEngine;

namespace Unipal.Model.EventHandlers {
    public class AssignmentListEventArgs : EventArgs {
        public Assignment Assignment { get; }
        public UpdateType UpdateType { get; }

        public AssignmentListEventArgs(Assignment assignment, UpdateType updateType) {
            Assignment = assignment;
            UpdateType = updateType;
        }
    }

    public enum UpdateType {
        Add,
        Remove
    }
}
