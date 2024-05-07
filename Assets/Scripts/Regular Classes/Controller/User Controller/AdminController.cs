using System.Collections;
using System.Collections.Generic;
using Unipal.Model.User;
using UnityEngine;

namespace Unipal.Controller.User {
    public class AdminController : UserController {
        public static AdminController Instance { get; private set; }

        public new Student CurrentLoggedAccount {
            get => (Student)base.CurrentLoggedAccount;
            set {
                if (value is not Student) {
                    return;
                }
                base.CurrentLoggedAccount = value;
            }
        }

        public AdminController(UserAccount userAccount) : base(userAccount) {
            Instance = this;
        }

        // When get info from the API, the information will be stored in this class.
        // We will then make those info singleton, therefore, we can access those information
        // again in the future.

        // However, to get the application updated realtime, for each 30s or when user refresh,
        // the application will get new data from the server and reload the current one.

        public void EnrollUser(UserType userType, string name, string surname, string email, string address, string cellphone, string dob) {

        }
    }

    public enum UserType {
        Student,
        Teacher
    }

    public struct EnrollObject {
        public string name;
        public string surname;
        public string email;
        public string address;
        public string cellphone;
        public string dob;
    }

    public struct EnrollResponse {
        public string message;
        public string token;
    }
}
