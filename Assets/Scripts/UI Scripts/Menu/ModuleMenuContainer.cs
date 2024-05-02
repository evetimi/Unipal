using System.Collections;
using System.Collections.Generic;
using UI.Menu.PanelIDs;
using UI.Modules;
using UnityEngine;

namespace UI.Menu {
    public class ModuleMenuContainer : MenuContainer<ModulePanelID> {
        public void ChangeInfo() {
            ChangePanel(ModulePanelID.Info);
        }

        public void ChangeAssignments() {
            ChangePanel(ModulePanelID.Assignments);
        }

        public void ChangeAssignmentInfo() {
            ChangePanel(ModulePanelID.AssignmentInfo);
        }

        public void ChangeGrades() {
            ChangePanel(ModulePanelID.Grades);
        }

        public void ChangeGradeInfo() {
            ChangePanel(ModulePanelID.GradeInfo);
        }

        public void ChangeResources() {
            ChangePanel(ModulePanelID.Resources);
        }
    }
}
