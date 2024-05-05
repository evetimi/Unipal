using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Calendars {
    public class CalendarObject : MonoBehaviour {
        [SerializeField] private GameObject _hasEvent;
        [SerializeField] private GameObject _noEvent;
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private ObjectPooling _eventPool;
        [SerializeField] private CalendarEvent _calendarEventPrefab;

        private DateTime _dateTime;

        public void SetIsNoEvent(bool isNoEvent) {
            _hasEvent.SetActive(!isNoEvent);
            _noEvent.SetActive(isNoEvent);
        }

        public void SetDate(DateTime date) {
            _dateTime = date;

            if (_dateText != null && _dateTime != null) {
                _dateText.text = _dateTime.ToString("dddd dd MMMM");
            }
        }

        public void ResetEvents() {
            _eventPool.DisableAll();
        }

        public void AddEvent(string title, string room, DateTime startTime, DateTime endTime, Color eventColor) {
            void OnObjectEnabled(int index, CalendarEvent calendarEvent) {
                calendarEvent.SetTitleText(title);
                calendarEvent.SetRoomText(room);
                calendarEvent.SetTime(startTime, endTime);
                calendarEvent.SetEventColor(eventColor);
            }

            _eventPool.NewObjects(1, _calendarEventPrefab, OnObjectEnabled);
        }
    }
}
