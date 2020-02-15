using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailSender.Classes
{
    public static class Regular
    {
        /// <summary>
        /// Поиск полного вхождения в строку
        /// </summary>
        /// <param name="find">искомая фраза</param>
        /// <param name="text">текст для поиска полного вхождения</param>
        public static bool FindString(string find, string text)
        {
            Regex regex = new Regex($@"\w*{find}");
            MatchCollection Matches = regex.Matches(text);
            if (Matches.Count > 0) return true;
            return false;
        }
    }
}
