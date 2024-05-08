using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Calendars {
    public class CalendarEvent : MonoBehaviour {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _roomText;
        [SerializeField] private TMP_Text _startTimeText;
        [SerializeField] private TMP_Text _endTimeText;
        [SerializeField] private Image _eventColorImage;

        private DateTime _startTime;
        private DateTime _endTime;

        public void SetTitleText(string text) {
            if (_titleText) {
                _titleText.text = text;
            }
        }

        public void SetRoomText(string text) {
            if (_roomText) {
                _roomText.text = text;
            }
        }

        public void SetTime(DateTime startTime, DateTime endTime) {
            _startTime = startTime;
            _endTime = endTime;

            if (_startTimeText != null && _startTime != null) {
                string hour = _startTime.Hour < 10 ? $"0{_startTime.Hour}" : _startTime.Hour.ToString();
                string minute = _startTime.Minute < 10 ? $"0{_startTime.Minute}" : _startTime.Hour.ToString();
                _startTimeText.text = $"{hour}:{minute}";
            }

            if (_endTimeText != null && _endTime != null) {
                string hour = _endTime.Hour < 10 ? $"0{_endTime.Hour}" : _endTime.Hour.ToString();
                string minute = _endTime.Minute < 10 ? $"0{_endTime.Minute}" : _endTime.Hour.ToString();
                _endTimeText.text = $"{hour}:{minute}";
            }
        }

        public void SetEventColor(Color color) {
            if (_eventColorImage) {
                _eventColorImage.color = color;
            }
        }
    }
}
