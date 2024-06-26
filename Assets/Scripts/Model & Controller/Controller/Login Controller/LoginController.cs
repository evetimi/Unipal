using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipal.API;
using Unipal.Controller.User;
using Unipal.Model.User;
using UnityEngine;

namespace Unipal.Controller.Login {
    /// <summary>
    /// The controller for the login and signup functionality of the application, this method will help the other script to validate and send request to the server.
    /// </summary>
    public class LoginController {
        private static LoginController instance;
        public static LoginController Instance {
            get {
                instance ??= new LoginController();
                return instance;
            }
        }

        /// <summary>
        /// To verify if the email is signed up in the server.
        /// </summary>
        /// <param name="email">The email to send request</param>
        /// <returns>The request status (Success if email verified, Fail if does not, Error if connection problem to the API)</returns>
        public async Task<CredentialStatus> VerifyEmail(string email) {
            ApiSendObject<EmailVerifyMessage> apiSendObject = new ApiSendObject<EmailVerifyMessage>() {
                body = new EmailVerifyMessage() {
                    email = email
                }
            };

            var unipalMsg = await UnipalClient.DoPostRequestAsync<EmailVerifyMessage, EmailVerifyReceiveMessage>(ApiPathContainer.ApiPath.emailVerification, apiSendObject);
            
            // TODO: Check if the email is good to go: it has to be in the system but not registered yet
            if (unipalMsg.receiveMessageSuccess) {
                if (unipalMsg.receivedMessage.status.Equals("201")) {
                    return CredentialStatus.Success;
                } else {
                    return CredentialStatus.Fail;
                }
            }

            return CredentialStatus.Error;
        }

        /// <summary>
        /// To verify if the token matches the registered email.
        /// </summary>
        /// <param name="email">Email to register</param>
        /// <param name="token">Token for the email</param>
        /// <returns>The request status (Success if token verified successful, Fail if does not, Error if connection problem to the API)</returns>
        public async Task<CredentialStatus> VerifyToken(string email, string token) {
            ApiSendObject<TokenMessage> tokenVerifyObject = new ApiSendObject<TokenMessage>() {
                body = new TokenMessage() {
                    email = email,
                    token = token
                }
            };

            var unipalMsg = await UnipalClient.DoPostRequestAsync<TokenMessage, string>(ApiPathContainer.ApiPath.tokenVerification, tokenVerifyObject);

            // TODO: Check if the token is good to go
            if (unipalMsg.receiveMessageSuccess) {
                if (unipalMsg.receivedMessage.status.Equals("201")) {
                    return CredentialStatus.Success;
                } else {
                    return CredentialStatus.Fail;
                }
            }

            return CredentialStatus.Error;
        }

        /// <summary>
        /// Send sign up request to the server, this method will validate the email and password be sending request, if the validation failed, the request will not be made.
        /// </summary>
        /// <param name="email">Email to sign up</param>
        /// <param name="password">Password to sign up</param>
        /// <param name="confirmPassword">To confirm the password if it is matched password or not</param>
        /// <returns>The signup request status (Success if signup successful, Fail if does not, Error if connection problem to the API)</returns>
        public async Task<SignupStatus> Signup(string email, string password, string confirmPassword) {
            SignupStatus signupStatus = new SignupStatus();

            // confirmPassword
            if (!password.Equals(confirmPassword)) {
                signupStatus.status = CredentialStatus.Fail;
                signupStatus.statusMessage = "Password does not match!";
                return signupStatus;
            }
            
            ApiSendObject<SignupMessage> signupObject = new ApiSendObject<SignupMessage>() {
                body = new SignupMessage() {
                    email = email,
                    password = password
                }
            };

            var unipalMsg = await UnipalClient.DoPostRequestAsync<SignupMessage, string>(ApiPathContainer.ApiPath.signup, signupObject);
            if (unipalMsg.receiveMessageSuccess) {
                if (unipalMsg.receivedMessage.status.Equals("201")) {
                    signupStatus.status = CredentialStatus.Success;
                    signupStatus.statusMessage = "Signing up account successfully";
                } else {
                    signupStatus.status = CredentialStatus.Fail;
                    signupStatus.statusMessage = "There was an error signing up your account";
                }
            }

            return signupStatus;
        }

        /// <summary>
        /// Send login request to the server, will send the user to the Home page if the login is successful.
        /// </summary>
        /// <param name="email">Email to login</param>
        /// <param name="password">Password to login</param>
        /// <returns>The request status (Success if login successful, Fail if does not, Error if connection problem to the API)</returns>
        public async Task<LoginStatus> Login(string email, string password) {
            ApiSendObject<LoginMessage> loginObject = new ApiSendObject<LoginMessage>() {
                body = new LoginMessage() {
                    email = email,
                    password = password
                }
            };

            var unipalMsg = await UnipalClient.DoPostRequestAsync<LoginMessage, LoginReceiveMessage>(ApiPathContainer.ApiPath.login, loginObject);

            LoginStatus loginStatus = new();

            if (unipalMsg.receiveMessageSuccess) {
                if (unipalMsg.receivedMessage.status.Equals("200")) {
                    var body = unipalMsg.receivedMessage.body;

                    // Extract dob
                    string dobStr = body.dob;
                    var dobArr = dobStr.Split('-');
                    int year = -1, month = -1, day = -1;
                    int.TryParse(dobArr[0], out year);
                    int.TryParse(dobArr[1], out month);
                    int.TryParse(dobArr[2], out day);

                    DateTime dob = new DateTime(2000, 1, 1);
                    if (year != -1 && month != -1 && day != -1) {
                        dob = new DateTime(year, month, day);
                    }

                    // TODO: create user object here
                    if (body.type == "1") {
                        loginStatus.userType = UserType.Student;
                        Student student = new Student(
                            body.id,
                            body.name,
                            body.surname,
                            body.email,
                            body.gender,
                            dob,
                            body.address,
                            body.cellphone
                        );
                        new StudentController(student);
                    } else {
                        loginStatus.userType = UserType.Teacher;
                        Teacher teacher = new Teacher(
                            body.id,
                            body.name,
                            body.surname,
                            body.email,
                            body.gender,
                            dob,
                            body.address,
                            body.cellphone
                        );
                        new AdminController(teacher);
                    }

                    loginStatus.status = CredentialStatus.Success;
                } else {
                    loginStatus.status = CredentialStatus.Fail;
                }
            } else {
                loginStatus.status = CredentialStatus.Error;
            }

            return loginStatus;
        }
    }

    #region Used for LoginController to return the message status of the API request
    public enum CredentialStatus {
        Success,
        Fail,
        Error
    }

    public struct SignupStatus {
        public CredentialStatus status;
        public string statusMessage;
    }

    public struct LoginStatus {
        public CredentialStatus status;
        public UserType userType;
    }
    #endregion

    #region Email Verify
    public struct EmailVerifyMessage {
        public string email;
    }

    public struct EmailVerifyReceiveMessage {
        public string message;
    }
    #endregion

    #region Token
    public struct TokenMessage {
        public string email;
        public string token;
    }

    public struct TokenReceiveMessage {

    }
    #endregion

    #region Signup
    public struct SignupMessage {
        public string email;
        public string password;
    }

    public struct SignupReceiveMessage {

    }
    #endregion

    #region Login
    public struct LoginMessage {
        public string email;
        public string password;
    }

    public struct LoginReceiveMessage {
        public string id;
        public string name;
        public string surname;
        public string email;
        public string address;
        public string type;
        public string gender;
        public string dob;
        public string cellphone;
    }
    #endregion
}