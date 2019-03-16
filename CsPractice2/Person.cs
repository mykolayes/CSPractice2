using System;

namespace NaUKMA.CS.Practice02
{
    internal class Person
    {
        public String Name { get; private set; }
        public String Surname { get; private set; }
        public String Email { get; private set; }
        public DateTime? BirthDate { get; private set; }

        public Person(string name, string surname, string email, Object birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            if (birthDate is DateTime)
            {
                BirthDate = (DateTime?)birthDate;
            }
            else
            {
                BirthDate = null;
            }
        }

        public Person(string name, string surname, string email):this(name,surname,email, null)
        {
        }

        public Person(string name, string surname, DateTime birthDate):this(name, surname, "", birthDate)
        {
        }

        public bool IsAdult
        {
            get
            {
                int age = DateTime.Now.Year - BirthDate.Value.Year;
                if (BirthDate > DateTime.Now.AddYears(-age))
                    age--;

                return (age > 18);
            }
        }

        public string SunSign
        {
            get
            {
                int month = BirthDate.Value.Month;
                int day = BirthDate.Value.Day;
                switch (month)
                {
                    case 1:
                        if (day <= 19)
                            return "Capricorn";
                        else
                            return "Aquarius";
                    case 2:
                        if (day <= 18)
                            return "Aquarius";
                        else
                            return "Pisces";
                    case 3:
                        if (day <= 20)
                            return "Pisces";
                        else
                            return "Aries";
                    case 4:
                        if (day <= 19)
                            return "Aries";
                        else
                            return "Taurus";
                    case 5:
                        if (day <= 20)
                            return "Taurus";
                        else
                            return "Gemini";
                    case 6:
                        if (day <= 20)
                            return "Gemini";
                        else
                            return "Cancer";
                    case 7:
                        if (day <= 22)
                            return "Cancer";
                        else
                            return "Leo";
                    case 8:
                        if (day <= 22)
                            return "Leo";
                        else
                            return "Virgo";
                    case 9:
                        if (day <= 22)
                            return "Virgo";
                        else
                            return "Libra";
                    case 10:
                        if (day <= 22)
                            return "Libra";
                        else
                            return "Scorpio";
                    case 11:
                        if (day <= 21)
                            return "Scorpio";
                        else
                            return "Sagittarius";
                    case 12:
                        if (day <= 21)
                            return "Sagittarius";
                        else
                            return "Capricorn";
                    default:
                        return "";
                }
            }
        }

        public string ChineseSign
        {
            get
            {
                var c = new System.Globalization.ChineseLunisolarCalendar();
                var y = c.GetSexagenaryYear(BirthDate.Value);
                var s = c.GetCelestialStem(y) - 1;
                return
                    ",Rat,Ox,Tiger,Rabbit,Dragon,Snake,Horse,Goat,Monkey,Rooster,Dog,Pig".Split(',')[
                        c.GetTerrestrialBranch(y)]
                    + " - "
                    + "Wood,Fire,Earth,Metal,Water".Split(',')[s / 2]
                    + " - Y" + (s % 2 > 0 ? "in" : "ang");
            }
        }

        public bool IsBirthday
        {
            get
            {
                return DateTime.Now.Month.Equals(BirthDate.Value.Month) && DateTime.Now.Day.Equals(BirthDate.Value.Day);
            }
        }
    }
}
