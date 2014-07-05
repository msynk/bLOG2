using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace bLOG.Core.Localization
{
  public sealed class PersianCultureInfo : CultureInfo
  {
    private readonly Calendar _cal;
    private readonly Calendar[] _optionals;

    public PersianCultureInfo(string cultureName = "fa-IR")
      : base(cultureName, true)
    {
      _cal = base.OptionalCalendars[0];

      var optionalCalendars = new List<Calendar>();
      optionalCalendars.AddRange(base.OptionalCalendars);
      optionalCalendars.Insert(0, new PersianCalendar());


      var formatType = typeof(DateTimeFormatInfo);
      var calendarType = typeof(Calendar);


      var idProperty = calendarType.GetProperty("ID", BindingFlags.Instance | BindingFlags.NonPublic);
      var optionalCalendarfield = formatType.GetField("optionalCalendars", BindingFlags.Instance | BindingFlags.NonPublic);

      var newOptionalCalendarIDs = new Int32[optionalCalendars.Count];
      for (int i = 0; i < newOptionalCalendarIDs.Length; i++)
      {
        newOptionalCalendarIDs[i] = (Int32)idProperty.GetValue(optionalCalendars[i], null);
      }

      if (optionalCalendarfield != null)
      {
        optionalCalendarfield.SetValue(DateTimeFormat, newOptionalCalendarIDs);
      }

      _optionals = optionalCalendars.ToArray();
      _cal = _optionals[0];
      DateTimeFormat.Calendar = _optionals[0];

      DateTimeFormat.MonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
      DateTimeFormat.MonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
      DateTimeFormat.AbbreviatedMonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
      DateTimeFormat.AbbreviatedMonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };


      DateTimeFormat.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
      DateTimeFormat.ShortestDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
      DateTimeFormat.DayNames = new[] { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };

      DateTimeFormat.AMDesignator = "ق.ظ";
      DateTimeFormat.PMDesignator = "ب.ظ";

      
      DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
      DateTimeFormat.LongDatePattern = "yyyy/MM/dd";
             
      DateTimeFormat.SetAllDateTimePatterns(new[] {"yyyy/MM/dd"}, 'd');
      DateTimeFormat.SetAllDateTimePatterns(new[] {"dddd dd MMMM yyyy، ساعت HH:mm"}, 'D');
      
      DateTimeFormat.SetAllDateTimePatterns(new[] {"yyyy MMMM"}, 'y');
      DateTimeFormat.SetAllDateTimePatterns(new[] {"yyyy MMMM"}, 'Y');
      

    }

    public override Calendar Calendar
    {
      get { return _cal; }
    }

    public override Calendar[] OptionalCalendars
    {
      get { return _optionals; }
    }
  }
}
