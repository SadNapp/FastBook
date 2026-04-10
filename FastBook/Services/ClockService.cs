using System;
using System.Collections.Generic;
using System.Text;

namespace FastBook.Services
{
    public class ClockService
    {
        public string GetFormattedTime() => DateTime.Now.ToString("ddd dd.MM.yy HH:mm:ss").ToLower();
    }
}
