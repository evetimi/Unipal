using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unipal.Model.User {
    public abstract class UserAccount {
        private string id;
        private string name;
        private string surname;
        private string email;
        private string gender;
        private DateTime dob;
        private string address;
        private string phoneNumber;

        public string Id { get => id; set { id = value; } }
        public string Name { get => name; set { name = value; } }
        public string Surname { get => surname; set { surname = value; } }
        public string Email { get => email; set { email = value; } }
        public string Gender { get => gender; set { gender = value; } }
        public DateTime Dob { get => dob; set { dob = value; } }
        public string Address { get => address; set { address = value; } }
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; } }

        public UserAccount(string id, string name, string surname, string email, string gender, DateTime dob, string address, string phoneNumber) {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.gender = gender;
            this.dob = dob;
        }
    }
}
