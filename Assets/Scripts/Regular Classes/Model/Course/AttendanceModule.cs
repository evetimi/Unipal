using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Unipal.Model.Course {
    public class AttendanceModule {
        private Module _module;
        private List<AttendanceInfo> _attendanceInfos;

        public AttendanceModule(Module module) {
            _module = module;
            _attendanceInfos = new(20); // 20 is the good start size of attendance info for one module
        }

        public void AddAttendance(AttendanceInfo attendanceInfo) {
            _attendanceInfos.Add(attendanceInfo);
        }

        public void AddAttendance(DateTime dateTime, AttendanceStatus status) {
            AddAttendance(new AttendanceInfo() {
                dateTime = dateTime,
                status = status
            });
        }

        public void RemoveAttendance(int year, int month, int day) {
            for (int i = 0; i < _attendanceInfos.Count; i++) {
                DateTime current = _attendanceInfos[i].dateTime;
                if (current.Year == year && current.Month == month && current.Day == day) {
                    _attendanceInfos.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public struct AttendanceInfo {
        public DateTime dateTime;
        public AttendanceStatus status;
    }

    public enum AttendanceStatus {
        Present = 0,
        Late = 1,
        Absent = 2
    }
}
