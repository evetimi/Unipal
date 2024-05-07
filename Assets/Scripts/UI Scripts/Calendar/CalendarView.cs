using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aspose.Email.Calendar;
using Aspose.Email.Clients.Google;
using Aspose.Email.Clients.Graph;
using Sirenix.OdinInspector;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Utilities;

namespace UI.Calendars {
    public class CalendarView : MenuPanel {
        [BoxGroup("Calendar"), SerializeField] private ObjectPooling _calendarContainer;
        [BoxGroup("Calendar"), SerializeField] private CalendarObject _calendarObjectPrefab;

        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);

            if (enabled) {
                SetupCalendar();
            }
        }

        [Button]
        private void Testtt() {
            DateTime today = DateTime.Today;
            DateTime firstDayOfThisMonth = new DateTime(today.Year, today.Month, 1);
            Debug.Log(today.CompareTo(firstDayOfThisMonth));
            Debug.Log(firstDayOfThisMonth.CompareTo(today));
            Debug.Log(today);
        }
        
        private void SetupCalendar() {
            var reader = new CalendarReader("Assets/calendar.ics");

            List<Appointment> appointments = reader.LoadAsMultiple();
            int i = 0;
            while (reader.NextEvent()) {
                var loadedAppointment = reader.Current;
                appointments[i++] = loadedAppointment;
            }

            appointments.Sort((x, y) => x.StartDate.CompareTo(y.StartDate));
            
            foreach (Appointment appointment in appointments) {
                Debug.Log($"{appointment.StartDate:dddd dd MMMM} : " + appointment.StartDate.ToString() + " -> " + appointment.EndDate.ToString());
            }
            DateTime today = DateTime.Today;
            DateTime firstDayOfThisMonth = new DateTime(today.Year, today.Month, 1);

            int currentIndex;
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            for (currentIndex = 0; currentIndex < appointments.Count && appointments[currentIndex].StartDate.CompareTo(firstDayOfThisMonth) < 0; currentIndex++) {}
            Debug.Log("Start: " + currentIndex);

            int year = firstDayOfThisMonth.Year;
            int month = firstDayOfThisMonth.Month;
            
            for (int day = 1; day <= daysInMonth; day++) {
                bool hasEvent = false;

                CalendarObject calendarObject = null;
                void OnCalendarObjectCreated(int index, CalendarObject newCalendarObject) {
                    calendarObject = newCalendarObject;
                    calendarObject.ResetEvents();
                    calendarObject.SetDate(new DateTime(year, month, day));
                }
                _calendarContainer.NewObjects(1, _calendarObjectPrefab, OnCalendarObjectCreated);

                while (currentIndex < appointments.Count) {
                    DateTime startDate = appointments[currentIndex].StartDate;
                    if (year == startDate.Year && month == startDate.Month && day == startDate.Day) {
                        // TODO: set has event
                        Appointment app = appointments[currentIndex];
                        ColorUtility.TryParseHtmlString(app.Summary, out Color color);
                        calendarObject.AddEvent(app.Description, app.Location, startDate, app.EndDate, color);

                        hasEvent = true;
                        currentIndex++;
                    } else {
                        break;
                    }
                }

                calendarObject.SetIsNoEvent(!hasEvent);
            }
        }

        [Button]
        private void GenerateTestAppointments() {
            using (var calendar = new CalendarWriter("Assets/calendar.ics")) {
                for (int i = 0; i < 40; i++) {
                    int day = UnityEngine.Random.Range(1, 32);
                    int hour = UnityEngine.Random.Range(0, 20);
                    var appointment = new Appointment(
                        "Meeting Room A",
                        "Team Meeting",
                        "Please confirm your availability.",
                        new DateTime(2024, 5, day, hour, 0, 0),
                        new DateTime(2024, 5, day, hour + 2, 0, 0),
                        "sunmoon14103@gmail.com",
                        "sunmoon14103@gmail.com"
                    );

                    calendar.Write(appointment);
                }
            };
        }
    }
}
