using System;

namespace RadarScreen.DataAccess
{
	public class GPSweek
	{
        public int Day = 0;
        public int dy = 0;
        public int GPS_day = 0;
        public int GPS_month = 0;

        public GPSweek(int day, int Month, int Year)
        {

            float Delta = Year - 1978;

            /********** 01/01/.... Which Day **************/
            for (int i = 0; i < Delta; i++)
            {
                if (i % 4 == 2)
                    dy += 2;
                else
                    dy += 1;
            }
            dy = dy % 7;
            /***********************************************/

            /********** Day/Month/Year Which Week **********/
            int AraMonth = 12;
            double TopDay = 0;
            for (int j = 0; j <= Delta; j++)
            {
                if (j + 1978 == Year)
                {
                    AraMonth = Month;
                    TopDay = Day;
                    Day = day;
                }
                else
                    Day += 31;
                if (AraMonth >= 12)
                    Day += 30;
                if (AraMonth >= 11)
                    Day += 31;
                if (AraMonth >= 10)
                    Day += 30;
                if (AraMonth >= 9)
                    Day += 31;
                if (AraMonth >= 8)
                    Day += 31;
                if (AraMonth >= 7)
                    Day += 30;
                if (AraMonth >= 6)
                    Day += 31;
                if (AraMonth >= 5)
                    Day += 30;
                if (AraMonth >= 4)
                    Day += 31;
                if (AraMonth >= 3)
                {
                    if ((j + 1978) % 4.0f == 0.0f)
                        Day += 29;
                    else
                        Day += 28;
                }
                if (AraMonth >= 2)
                    Day += 31;
            }
            TopDay += Day;
            GPS_month = -105;
            GPS_month += (int)Math.Ceiling((TopDay / 7.0)) - 1;
            /***********************************************/

            /********** Day/Month/Year Which Day ***********/
            GPS_day = (Day - 1 + dy) % 7;
            /***********************************************/
        }
    }
}