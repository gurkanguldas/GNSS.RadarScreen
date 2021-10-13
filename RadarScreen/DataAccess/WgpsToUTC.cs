using System;
using System.Collections.Generic;

namespace RadarScreen.DataAccess
{
    public class WgpsToUTC
    {
        private int day, month, year;
        public WgpsToUTC(double t0, double wgps)
        {

            year = (int)wgps;
            day = (int)t0;

            if (t0 - day != 0.0)
                day++;

            month = 1;

            Dictionary<int, int> MaxDay = Months(year);

            for (int i = 1; i < 12; i++)
            {
                if(day > MaxDay[i])
                {
                    month++;
                    day -= MaxDay[i];
                }
            }
        }
        public int getMaxDay(int Month, int Year)
        {
            return Months(Year)[Month];
        }
        public Dictionary<int, int> Months(int year)
        {
            Dictionary<int, int> MaxDay = new Dictionary<int, int>();

            MaxDay.Add(1, 31);
            MaxDay.Add(2, year % 4.0 == 0.0 ? 29 : 28);
            MaxDay.Add(3, 31);
            MaxDay.Add(4, 30);
            MaxDay.Add(5, 31);
            MaxDay.Add(6, 30);
            MaxDay.Add(7, 31);
            MaxDay.Add(8, 31);
            MaxDay.Add(9, 30);
            MaxDay.Add(10, 31);
            MaxDay.Add(11, 30);
            MaxDay.Add(12, 31);

            return MaxDay;
        }
        public int getDay() => day;
        public int getMonth() => month;
        public int getYear() => year;
    }
}