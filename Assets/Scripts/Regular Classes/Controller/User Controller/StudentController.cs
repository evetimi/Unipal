using System.Collections;
using System.Collections.Generic;
using Unipal.Model.User;
using UnityEngine;

namespace Unipal.Controller.User {
    public class StudentController : UserController<StudentController> {
        [SerializeField] private string ass = "";

        public new Student CurrentLoggedAccount {
            get => (Student)base.CurrentLoggedAccount;
            set {
                if (value is not Student) {
                    return;
                }
                base.CurrentLoggedAccount = value;
            }
        }

        // When get info from the API, the information will be stored in this class.
        // We will then make those info singleton, therefore, we can access those information
        // again in the future.

        // However, to get the application updated realtime, for each 30s or when user refresh,
        // the application will get new data from the server and reload the current one.

        public void GetAttendedModules() {
            // This might also get the assignment status of the modules as well
        }

        public void GetTimeTable() {

        }

        public void GetAttendanceStatus() {

        }
    }
}
