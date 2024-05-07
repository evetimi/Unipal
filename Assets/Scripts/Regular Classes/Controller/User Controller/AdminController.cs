using System.Collections;
using System.Collections.Generic;
using Unipal.API;
using Unipal.Model.User;
using UnityEngine;
using System.Threading.Tasks;

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

        public async Task<ApiReceiveObject<EnrollResponse>> EnrollUser(UserType userType, string name, string surname, string gender, string email, string address, string cellphone, string dob) {
            // MVC
            EnrollObject enrollObject = new EnrollObject() {
                name = name,
                surname = surname,
                email = email,
                address = address,
                cellphone = cellphone,
                gender = gender,
                dob = dob,
                type = (int)userType
            };
            ApiSendObject<EnrollObject> send = new ApiSendObject<EnrollObject>() {
                body = enrollObject
            };

            UnipalMessage<EnrollResponse> response = await UnipalClient.DoPostRequestAsync<EnrollObject, EnrollResponse>(ApiPathContainer.ApiPath.enrollUser, send);
            if (!response.receiveMessageSuccess) {
                return null;
            } else {
                Debug.Log(response.receivedMessage.status);
                Debug.Log(response.receivedMessage.body.message);
                Debug.Log(response.receivedMessage.body.token);
                return response.receivedMessage;
            }
        }
    }

    public enum UserType {
        Student = 1,
        Teacher = 2
    }

    public struct EnrollObject {
        public string name;
        public string surname;
        public string email;
        public string address;
        public string cellphone;
        public string dob;
        public string gender;
        public int type;
    }

    public struct EnrollResponse {
        public string message;
        public string token;
    }
}
