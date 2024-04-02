using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unipal.User {
    public abstract class UserAccount {
        private string id;
        private string name;

        public string Id => id;
        public string Name => name;

        public UserAccount(string id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}
