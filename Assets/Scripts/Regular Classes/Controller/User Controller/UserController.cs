using System.Collections;
using System.Collections.Generic;
using Unipal.Model.User;
using UnityEngine;

namespace Unipal.Controller.User {
    public abstract class UserController<T> : MonoBehaviourSingleton<T> where T : Component {
        private UserAccount _currentLoggedAccount;

        public UserAccount CurrentLoggedAccount {
            get => _currentLoggedAccount;
            set => _currentLoggedAccount = value;
        }

        public void GetUserInformation() {
            
        }
    }
}
