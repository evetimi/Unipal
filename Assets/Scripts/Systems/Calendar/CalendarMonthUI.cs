using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;

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
	[TabGroup("Setup"), SerializeField] private Transform _middlePosition;

    [TabGroup("Month"), SerializeField] private Transform _monthContainer;
    [TabGroup("Month"), SerializeField] private TMP_Text[] _monthTexts; // should be 3 objects

    [TabGroup("Day"), SerializeField] private GridLayoutGroup _gridLayout;
    [TabGroup("Day"), SerializeField] private Transform _dayContainer;
    [TabGroup("Day"), SerializeField] private TMP_Text[] _dayTexts; // should be 98 objects

	[BoxGroup("Effect"), SerializeField] private float _moveTime = 0.2f;

    [BoxGroup("Runtime"), SerializeField] private int _currentMonth;
    [BoxGroup("Runtime"), SerializeField] private int _currentYear;
    [BoxGroup("Runtime"), SerializeField] private int _currentMonthIndex;
    [BoxGroup("Runtime"), SerializeField] private int _nextMonthIndex;

	private bool ValidateMonth(int month) { return month >= 1 && month <= 12; }

	[Button]
    public void SetMonth(int month, int year) {
        if (month <= 0 || month > 12) {
            return;
        }

		_currentMonth = month;
		_currentYear = year;

        int index = 0;
        DateTime providedDate = new DateTime(year, month, 1);
        int dayOfWeek = (int)providedDate.DayOfWeek; // starts at 0 from Sunday
        int numberOfDays = DateTime.DaysInMonth(year, month);

	    // Set all the day (previous months)
        int previousNumberOfDays = ValidateMonth(month - 1) ? DateTime.DaysInMonth(year, month - 1) : DateTime.DaysInMonth(year - 1, 12);
	    int previousDayOfWeek = (int)providedDate.DayOfWeek; // starts at 0 from Sunday
	    if (previousDayOfWeek > 0) 
	    {
	    	int dayNumbs = ValidateMonth(month - 2) ? DateTime.DaysInMonth(year, month - 2) : DateTime.DaysInMonth(year - 1, 11);
	    	for (int i = previousDayOfWeek - 1; i >= 0; i--, dayNumbs--) 
	    	{
	    		_dayTexts[i].text = dayNumbs.ToString();
	    	}
	    	
	    	index = previousDayOfWeek;
	    }
	    
	    for (; index < previousDayOfWeek + previousNumberOfDays; index++)
	    {
	    	_dayTexts[index].text = (index - previousDayOfWeek + 1).ToString();
	    }

	    // current month
	    _currentMonthIndex = index;
	    for (; index < _currentMonthIndex + numberOfDays; index++)
	    {
	    	_dayTexts[index].text = (index - _currentMonthIndex + 1).ToString();
	    }

	    // next month
	    _nextMonthIndex = index;
        int nextNumberOfDays = ValidateMonth(month + 1) ? DateTime.DaysInMonth(year, month + 1) : DateTime.DaysInMonth(year + 1, 1);
	    for (; index < _nextMonthIndex + nextNumberOfDays; index++)
	    {
	    	_dayTexts[index].text = (index - _nextMonthIndex + 1).ToString();
	    }
	    
	    int lastIndex = index;
	    for (; index < _dayTexts.Length; index++)
	    {
	    	_dayTexts[index].text = (index - lastIndex + 1).ToString();
	    }

        // Make _currentMonthIndex to be on the 1st col
        _currentMonthIndex -= _currentMonthIndex % 7;
        
		// Make _nextMonthIndex to be on the 1st col
        _nextMonthIndex = _currentMonthIndex + 42; // 6 rows * 7 cols

        // Set container position
        Vector3 currentPosition = _dayTexts[_currentMonthIndex].transform.position;
		Vector3 moveRange = _middlePosition.position - currentPosition;
		_dayContainer.transform.position += moveRange;

		//// Disable all non-visible dates

		//for (int i = 0; i < _currentMonthIndex; i++) {
		//	_dayTexts[i].gameObject.SetActive(false);
		//}


		//for (int i = _nextMonthIndex; i < _dayTexts.Length; i++) {
		//	_dayTexts[i].gameObject.SetActive(false);
		//}
	}

	public void PreviousMonth() {
        Vector3 previousPosition = _dayContainer.parent.InverseTransformPoint(_dayTexts[0].transform.position);
        Vector3 moveRange = _middlePosition.localPosition - previousPosition;
		Vector3 moveTarget = _dayContainer.transform.localPosition + moveRange;
		//      for (int i = 0; i < _currentMonthIndex; i++) {
		//	_dayTexts[i].gameObject.SetActive(true);
		//}

		Debug.Log(previousPosition);
        Debug.Log(moveRange);
        Debug.Log(moveTarget);

        _dayContainer.DOLocalMove(moveTarget, _moveTime).SetEase(Ease.InOutSine).OnComplete(() => {
			if (ValidateMonth(_currentMonth - 1)) {
				SetMonth(_currentMonth - 1, _currentYear);
			} else {
                SetMonth(12, _currentYear - 1);
			}
        });
    }

    public void NextMonth() {
        _gridLayout.enabled = false;
    }
}
