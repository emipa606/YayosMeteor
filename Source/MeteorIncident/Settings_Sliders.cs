using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MeteorIncident;

public static class Settings_Sliders
{
    private static readonly List<int> IntervalValues = new List<int>
    {
        1,
        100,
        200,
        300,
        400,
        500,
        600,
        750,
        900,
        1000,
        1100,
        1200,
        1300,
        1500,
        1700,
        1900
    };

    public static int MeteorEventFrequencyHorizontalSlider(Rect rect, int freq)
    {
        var index = IntervalValues.IndexOf(freq);
        if (index < 0)
        {
            index = IntervalValues.IndexOf(750);
        }

        var str = $"{IntervalValues[index] / 100f:F1} Sec";
        var num2 = index / (float)(IntervalValues.Count - 1);
        var num3 = Widgets.HorizontalSlider(rect, num2, 0f, 1f, true, str);
        if (num2 == (double)num3)
        {
            return freq;
        }

        var index2 = (int)Math.Round(num3 * (double)(IntervalValues.Count - 1));
        freq = IntervalValues[index2];

        return freq;
    }
}