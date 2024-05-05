using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aspose.Email.Calendar;
using Aspose.Email.Clients.Google;
using Aspose.Email.Clients.Graph;
using Sirenix.OdinInspector;
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
        
        private void SetupCalendar() {
            var reader = new CalendarReader("Assets/calendar.ics");

            List<Appointment> appointments = reader.LoadAsMultiple();
            int i = 0;
            while (reader.NextEvent()) {
                var loadedAppointment = reader.Current;
                appointments[i++] = loadedAppointment;
            }

            appointments.Sort((x, y) => x.StartDate.CompareTo(y.StartDate));
            
            // foreach (Appointment appointment in appointments) {
            //     Debug.Log($"{appointment.StartDate:dddd dd MMMM} : " + appointment.StartDate.ToString() + " -> " + appointment.EndDate.ToString());
            // }
        }

        private void GenerateTestAppointments() {
            using (var calendar = new CalendarWriter("Assets/calendar.ics")) {
                for (int i = 0; i < 10; i++) {
                    int day = UnityEngine.Random.Range(1, 32);
                    int hour = UnityEngine.Random.Range(0, 20);
                    var appointment = new Appointment(
                        "Meeting Room A",
                        "Team Meeting",
                        "Please confirm your availability.",
                        new DateTime(2023, 7, day, hour, 0, 0),
                        new DateTime(2023, 7, day, hour + 2, 0, 0),
                        "sunmoon14103@gmail.com",
                        "sunmoon14103@gmail.com"
                    );

                    calendar.Write(appointment);
                }
            };
        }
    }
}
