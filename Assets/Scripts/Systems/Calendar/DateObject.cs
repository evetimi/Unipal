using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [ReadOnly, ShowInInspector] private int date;
    [ReadOnly, ShowInInspector] private int month;
    [ReadOnly, ShowInInspector] private int year;

    public TMP_Text Text => _text;
    public int Date => date;
    public int Month => month;
    public int Year => year;

    private void Reset() {
        _text = GetComponent<TMP_Text>();
    }

    public void SetDate(int date, int month, int year) {
        this.date = date;
        this.month = month;
        this.year = year;

        _text.text = date.ToString();
    }
}
