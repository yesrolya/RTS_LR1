using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS_LR1
{
    class ClassDateTime
    {
        public void WorkWithDateTime (System.Windows.Controls.TextBlock n1, System.Windows.Controls.TextBlock n2)
        {
            //статические поля класса DateTime
            var current = DateTime.Now; //текущее время ПК в его часовом поясе
            var currentUTC = DateTime.UtcNow; //показывает время универсальное глобальное (UTC)
            var today = DateTime.Today; //текущая дата без времени
            var min = DateTime.MinValue; //минимальное значение структуры DateTime
            var max = DateTime.MaxValue; //минимальное значение структуры DateTime
            bool isLeap = DateTime.IsLeapYear(2019); //высокосный год или нет
            int daysInMonth = DateTime.DaysInMonth(2019, 4); //возвращает количество дней в месяце

            //основные конструкторы
            var dt1 = new DateTime(100000000000); //число тактов, прошедшее от минимального значения
            var dt2 = new DateTime(1,1,1); //дата
            var dt3 = new DateTime(1, 1, 1, 0, 0, 1); //дата и время
            var ticks = dt3.Ticks; //1 tick = 1E-7, отсчитывается для заданной даты от DateTime.MinValue

            var cDate = current.Date;
            //составные части: current.Day, current.Month, current.Year
            var cTime = current.TimeOfDay; //точность до 1E-7 секунды, время от начала дня
            //составные части: current.Hour, current.Minute, current.Second, current.Millisecond 

            var cDayWeek = current.DayOfWeek;
            var cDayYear = current.DayOfYear;

            //преобразование строк в дату
            string dtstr1 = "3/30/2019";
            string dtstr2 = "4.30.2020 12:38:12";
            var parse1 = DateTime.Parse(dtstr1);
            DateTime parse2;
            DateTime.TryParse(dtstr2, out parse2);

            TimeSpan dto = parse2 - parse1;

            TimeSpan ts = new TimeSpan(4, 4, 0);
            var q1 = current.AddSeconds(1);
            parse1.ToString("yyyy-mm =dd hh:mm-ss");
        }
    }
}
