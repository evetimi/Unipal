using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utilities {
    public class UISimpleDateInput : MonoBehaviour {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_Text _placeholder;
        [SerializeField] private TMP_Text _showingText;
        [SerializeField] private string _dateSeparator = " - ";

        private Color _placeholderDefaultColor;
        private Color _transparent = new Color(0f, 0f, 0f, 0f);
        private string _baseText;

        public string Year { get; private set; }
        public string Month { get; private set; }
        public string Day { get; private set; }

        private void Start() {
            // _inputField.characterLimit = 8;
            _placeholderDefaultColor = _placeholder.color;

            _inputField.onSelect.AddListener(OnSelect);
            _inputField.onEndEdit.AddListener(OnSubmit);
            _inputField.onDeselect.AddListener(OnSubmit);
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private string GetDisplay(string text, out string year, out string month, out string day) {
            string display;

            if (text.Length > 0) {
                // Pad the string to ensure it has 8 characters
                display = text;
                for (int i = display.Length; i < 8; i++) {
                    if (i < 4) {
                        display += 'y';
                    } else if (i < 6) {
                        display += 'm';
                    } else {
                        display += 'd';
                    }
                }

                // Extract year, month, and day
                year = display.Substring(0, 4);
                month = display.Substring(4, 2);
                day = display.Substring(6, 2);
                display = $"{year}{_dateSeparator}{month}{_dateSeparator}{day}";
            } else {
                display = $"yyyy{_dateSeparator}mm{_dateSeparator}dd";
                year = "";
                month = "";
                day = "";
            }

            return display;
        }

        private void OnSelect(string text) {
            _placeholder.color = _transparent;
            _showingText.gameObject.SetActive(true);
            _showingText.text = GetDisplay(text, out _, out _, out _);
        }
        
        private void OnValueChanged(string text) {
            _showingText.text = GetDisplay(text, out var year, out var month, out var day);
            Year = year;
            Month = month;
            Day = day;
        }

        private void OnSubmit(string text) {
            if (text.Length == 0) {
                _placeholder.color = _placeholderDefaultColor;
                _showingText.gameObject.SetActive(false);
            } else if (text.Length == 8) {
                ValidateDate();
            }
        }

        private void ValidateDate() {
            int month = int.Parse(Month);
            if (month > 12) {
                month = 12;
                Month = month.ToString();
            } else if (month < 1) {
                month = 1;
                Month = "0" + month.ToString();
            }

            int year = int.Parse(Year);
            int day = int.Parse(Day);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            if (day > daysInMonth) {
                day = daysInMonth;
                Day = day.ToString();
            } else if (day < 1) {
                day = 1;
                Day = "0" + day.ToString();
            }

            _inputField.text = Year + Month + Day;
        }
    }
}
