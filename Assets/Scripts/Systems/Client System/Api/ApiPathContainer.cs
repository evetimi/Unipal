using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Unipal.API {
    public struct ApiPath {
        public string emailVerification;
        public string tokenVerification;
        public string login;
        public string signup;
    }

    public static class ApiPathContainer {

        public static readonly string PathToResourcesApiPathFile = "apiPath";

        private static ApiPath _apiPath;
        public static ApiPath ApiPath { get => _apiPath; }

        [RuntimeInitializeOnLoadMethod]
        private static void InitializePath() {
            TextAsset jsonFile = Resources.Load<TextAsset>(PathToResourcesApiPathFile);
            if (jsonFile != null) {
                _apiPath = JsonUtility.FromJson<ApiPath>(jsonFile.text);
                ValidateApiPath();
            } else {
                GenerateDefaultApiPath();
            }
        }

        public static void SaveCurrentApiPathToFile() {
            string json = JsonUtility.ToJson(ApiPath);
            json = Regex.Replace(json, ",", ",\n    ");
            json = "{\n    " + json[1..^1] + "\n}";

            string filePath = Path.Combine(Application.dataPath, "Resources", PathToResourcesApiPathFile + ".json");
            File.WriteAllText(filePath, json);
        }

        private static void ValidateApiPath() {
            bool changed = false;
            Type type = typeof(ApiPath);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields) {
                string value = (string)field.GetValue(ApiPath);
                if (string.IsNullOrEmpty(value)) {
                    value = field.Name + ".php";
                    field.SetValueDirect(__makeref(_apiPath), value);
                    changed = true;
                }
            }

            if (changed) {
                SaveCurrentApiPathToFile();
            }
        }

        private static void GenerateDefaultApiPath() {
            _apiPath = new ApiPath();

            Type type = typeof(ApiPath);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields) {
                string name = field.Name;
                string value = name + ".php";
                field.SetValueDirect(__makeref(_apiPath), value);
            }

            SaveCurrentApiPathToFile();
        }
    }
}