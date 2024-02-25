using System;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;
using System.Globalization;
using System.Collections;

// 14 rows * 7 cols = 98 objects
//  0 0 0 1 2 3 4
//  5 6 7 8 91011
// 12131415161718
// 19202122232425
// 262728293031 1
//  2 3 4 5 6 7 8
//  9101112131415
// 16171819202122
// 23242526272829
// 3031 1 2 3 4 5
//  6 7 8 9101112
// 13141516171819
// 20212223242526
// 2728293031 0 0

public class CalendarMonthUI : MonoBehaviour
{
	
	[TabGroup("Month"), SerializeField] private Transform _monthContainer;
    [TabGroup("Month"), SerializeField] private Transform _monthMiddlePosition;
    [TabGroup("Month"), SerializeField] private TMP_Text[] _monthTexts; // should be 3 objects

    [TabGroup("Day"), SerializeField] private Transform _dayContainer;
    [TabGroup("Day"), SerializeField] private Transform _dayMiddlePosition;
    [TabGroup("Day"), SerializeField] private DateObject[] _dateObjects; // should be 98 objects

    [TabGroup("Marker"), SerializeField] private Transform _todayMarker;

	[BoxGroup("Effect"), SerializeField] private float _moveTime = 0.2f;
    [BoxGroup("Effect"), SerializeField] private Color _currentViewColor = Color.black;
    [BoxGroup("Effect"), SerializeField] private Color _outOfViewColor = Color.gray;

    [BoxGroup("Runtime"), ReadOnly, ShowInInspector] private int _currentMonth;
    [BoxGroup("Runtime"), ReadOnly, ShowInInspector] private int _currentYear;
    [BoxGroup("Runtime"), ReadOnly, ShowInInspector] private int _currentMonthIndex;
    [BoxGroup("Runtime"), ReadOnly, ShowInInspector] private int _nextMonthIndex;
    [BoxGroup("Runtime"), ReadOnly, ShowInInspector] private int _currentMonth1stRowIndex;
    [BoxGroup("Runtime"), ReadOnly, ShowInInspector] private int _nextMonth1stRowIndex;

    private DateTime today;

    private bool ValidateMonth(int month) { return month >= 1 && month <= 12; }

    private void Start() {
        today = DateTime.Now;
        SetMonth(today.Month, today.Year);
    }

    private void SetMonthText(TMP_Text monthText, int month, int year) {
        monthText.text = $"{DateTimeFormatInfo.CurrentInfo.GetMonthName(month)} {year}";
    }

    private bool TryPutMarker(Transform parent, int day, int month, int year) {
        if (day == today.Day && month == today.Month && year == today.Year) {
            _todayMarker.gameObject.SetActive(true);
            _todayMarker.SetParent(parent, false);
            _todayMarker.localPosition = Vector3.zero;
            return true;
        }

        return false;
    }

    public void SetMonth(int month, int year) {
        if (month <= 0 || month > 12) {
            return;
        }

		_currentMonth = month;
		_currentYear = year;

        int index = 0;
        int numberOfDays = DateTime.DaysInMonth(year, month);

		// Set all the month
		if (ValidateMonth(month - 1))
            SetMonthText(_monthTexts[0], month - 1, year);
        else
            SetMonthText(_monthTexts[0], 12, year - 1);
        
        SetMonthText(_monthTexts[1], month, year);

        if (ValidateMonth(month + 1))
            SetMonthText(_monthTexts[2], month + 1, year);
        else
            SetMonthText(_monthTexts[2], 1, year + 1);

        _monthContainer.transform.position = _monthMiddlePosition.position;

        // Set all the day (previous months)
        int previousNumberOfDays;
        int previousMonth;
        int previousYear;
		DateTime previousDate;

		if (ValidateMonth(month - 1)) {
            previousMonth = month - 1;
            previousYear = year;
        } else {
            previousMonth = 12;
            previousYear = year - 1;
        }

        previousDate = new DateTime(previousYear, previousMonth, 1);
        previousNumberOfDays = DateTime.DaysInMonth(previousYear, previousMonth);

        int previousDayOfWeek = (int)previousDate.DayOfWeek; // starts at 0 from Sunday

	    if (previousDayOfWeek > 0)  {
            int m = ValidateMonth(month - 2) ? month - 2 : 11;
            int y = ValidateMonth(month - 2) ? year : year - 1;
            int dayNumbs = DateTime.DaysInMonth(y, m);
	    	for (int i = previousDayOfWeek - 1; i >= 0; i--, dayNumbs--)  {
                _dateObjects[i].SetDate(dayNumbs, m, y);
	    		//_dateObjects[i].text = dayNumbs.ToString();
       //         TryPutMarker(_dateObjects[i].transform, dayNumbs, m, y);
            }
	    	
	    	index = previousDayOfWeek;
	    }
	    
	    for (; index < previousDayOfWeek + previousNumberOfDays; index++) {
            int dayNumbs = index - previousDayOfWeek + 1;
            _dateObjects[index].SetDate(dayNumbs, previousMonth, previousYear);
            //_dateObjects[index].text = dayNumbs.ToString();
            //      TryPutMarker(_dateObjects[index].transform, dayNumbs, previousMonth, previousYear);
        }

	    // current month
	    _currentMonthIndex = index;
	    for (; index < _currentMonthIndex + numberOfDays; index++) {
            int dayNumbs = index - _currentMonthIndex + 1;
            _dateObjects[index].SetDate(dayNumbs, month, year);
            //_dateObjects[index].text = dayNumbs.ToString();
            //TryPutMarker(_dateObjects[index].transform, dayNumbs, month, year);
        }

	    // next month
	    _nextMonthIndex = index;
        int nM, nnM, nY, nnY; // nextMonth, nextYear, nextNextMonth, nextNextYear
        if (ValidateMonth(month + 1)) {
            nM = month + 1;
            nY = year;
        } else {
            nM = 1;
            nY = year + 1;
        }

        if (ValidateMonth(month + 2)) {
            nnM = month + 2;
            nnY = year;
        } else {
            nnM = 2;
            nnY = year + 1;
        }

        int nextNumberOfDays = DateTime.DaysInMonth(nY, nM);
	    for (; index < _nextMonthIndex + nextNumberOfDays; index++) {
            int dayNumbs = index - _nextMonthIndex + 1;
            _dateObjects[index].SetDate(dayNumbs, nM, nY);
            //_dateObjects[index].text = dayNumbs.ToString();
            //TryPutMarker(_dateObjects[index].transform, dayNumbs, nM, nY);
        }
	    
	    int lastIndex = index;
	    for (; index < _dateObjects.Length; index++) {
            int dayNumbs = index - lastIndex + 1;
            _dateObjects[index].SetDate(dayNumbs, nnM, nnY);
            //_dateObjects[index].text = dayNumbs.ToString();
            //TryPutMarker(_dateObjects[index].transform, dayNumbs, nnM, nnY);
        }

        // Make _nextMonthIndex to be on the 1st col
        _nextMonth1stRowIndex = _currentMonthIndex + numberOfDays;
        _nextMonth1stRowIndex -= _nextMonth1stRowIndex % 7;

        // Make _currentMonthIndex to be on the 1st col
        _currentMonth1stRowIndex = _currentMonthIndex - _currentMonthIndex % 7;

        // Set container position
        //StartCoroutine(DelaySetDatePosition());
        Vector3 currentPosition = _dateObjects[_currentMonth1stRowIndex].transform.position;
        Vector3 moveRange = _dayMiddlePosition.position - currentPosition;
        _dayContainer.transform.position += moveRange;

        // Set today marker
        bool foundMarker = false;
        foreach (var date in _dateObjects) {
            if (TryPutMarker(date.transform, date.Date, date.Month, date.Year)) {
                foundMarker = true;
                break;
            }
        }

        if (!foundMarker) {
            _todayMarker.gameObject.SetActive(false);
        }
	}

    private IEnumerator DelaySetDatePosition() {
        yield return null;

        Vector3 currentPosition = _dateObjects[_currentMonth1stRowIndex].transform.position;
        Vector3 moveRange = _dayMiddlePosition.position - currentPosition;
        _dayContainer.transform.position += moveRange;
    }

	public void PreviousMonth() {
        animationValidation = 2;

		{
            Vector3 previousLocalPosition = _dayContainer.parent.InverseTransformPoint(_dateObjects[0].transform.position);
            Vector3 moveRange = _dayMiddlePosition.localPosition - previousLocalPosition;
            Vector3 moveTarget = _dayContainer.transform.localPosition + moveRange;

            _dayContainer.DOLocalMove(moveTarget, _moveTime).SetEase(Ease.InOutSine).OnComplete(() => AnimationComplete(true));
        }

        {
            Vector3 previousLocalPosition = _monthContainer.parent.InverseTransformPoint(_monthTexts[0].transform.position);
            Vector3 moveRange = _monthMiddlePosition.localPosition - previousLocalPosition;
            Vector3 moveTarget = _monthContainer.transform.localPosition + moveRange;

            _monthContainer.DOLocalMove(moveTarget, _moveTime).SetEase(Ease.InOutSine).OnComplete(() => AnimationComplete(true));
        }
    }

    public void NextMonth() {
        animationValidation = 2;

        {
            Vector3 nextLocalPosition = _dayContainer.parent.InverseTransformPoint(_dateObjects[_nextMonth1stRowIndex].transform.position);
            Vector3 moveRange = _dayMiddlePosition.localPosition - nextLocalPosition;
            Vector3 moveTarget = _dayContainer.transform.localPosition + moveRange;

            _dayContainer.DOLocalMove(moveTarget, _moveTime).SetEase(Ease.InOutSine).OnComplete(() => AnimationComplete(false));
        }

        {
            Vector3 nextLocalPosition = _monthContainer.parent.InverseTransformPoint(_monthTexts[2].transform.position);
            Vector3 moveRange = _monthMiddlePosition.localPosition - nextLocalPosition;
            Vector3 moveTarget = _monthContainer.transform.localPosition + moveRange;

            _monthContainer.DOLocalMove(moveTarget, _moveTime).SetEase(Ease.InOutSine).OnComplete(() => AnimationComplete(false));
        }
    }

    private int animationValidation;
    private void AnimationComplete(bool movePreviousMonth) {
        animationValidation--;

        if (animationValidation <= 0) {
            if (movePreviousMonth) {
                if (ValidateMonth(_currentMonth - 1)) {
                    SetMonth(_currentMonth - 1, _currentYear);
                } else {
                    SetMonth(12, _currentYear - 1);
                }

                for (int i = _nextMonthIndex; i < _dateObjects.Length; i++) {
                    _dateObjects[i].Text.color = _currentViewColor;
                    _dateObjects[i].Text.DOColor(_outOfViewColor, _moveTime).SetEase(Ease.Linear);
                }

                for (int i = _currentMonthIndex; i < _nextMonthIndex; i++) {
                    _dateObjects[i].Text.color = _outOfViewColor;
                    _dateObjects[i].Text.DOColor(_currentViewColor, _moveTime).SetEase(Ease.Linear);
                }

            } else {
                if (ValidateMonth(_currentMonth + 1)) {
                    SetMonth(_currentMonth + 1, _currentYear);
                } else {
                    SetMonth(1, _currentYear + 1);
                }

                for (int i = 0; i < _currentMonthIndex; i++) {
                    _dateObjects[i].Text.color = _currentViewColor;
                    _dateObjects[i].Text.DOColor(_outOfViewColor, _moveTime).SetEase(Ease.Linear);
                }

                for (int i = _currentMonthIndex; i < _nextMonthIndex; i++) {
                    _dateObjects[i].Text.color = _outOfViewColor;
                    _dateObjects[i].Text.DOColor(_currentViewColor, _moveTime).SetEase(Ease.Linear);
                }

            }
        }
    }
}
