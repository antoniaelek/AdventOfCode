﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class Helpers
    {
        public static DateTime ClearSeconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }

        public static int Max(this int[] arr, out int maxIdx)
        {
            maxIdx = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[maxIdx])
                {
                    maxIdx = i;
                }
            }
            return arr[maxIdx];
        }
    }
}
