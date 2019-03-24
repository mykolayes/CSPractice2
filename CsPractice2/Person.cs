using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace NaUKMA.CS.Practice02
{
    [Serializable]
    public class Person : INotifyPropertyChanged
    {
        private String _name;
        private String _surname;
        private String _email;
        private DateTime? _birthDate;

        private Boolean? _isAdult;
        private String _sunSign;
        private String _chineseSign;
        private Boolean? _isBirthday;

        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public String Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                ReCalcAdditionalInfo();
                OnPropertyChanged("BirthDate");
            }
        }

        [XmlIgnore]
        public Boolean? IsAdult
        {
            get { return _isAdult; }
            private set
            {
                _isAdult = value;
                OnPropertyChanged("IsAdult");
            }
        }

        [XmlIgnore]
        public String SunSign
        {
            get { return _sunSign; }
            private set
            {
                _sunSign = value;
                OnPropertyChanged("SunSign");
            }
        }

        [XmlIgnore]
        public String ChineseSign
        {
            get { return _chineseSign; }
            private set
            {
                _chineseSign = value;
                OnPropertyChanged("ChineseSign");
            }
        }

        [XmlIgnore]
        public Boolean? IsBirthday
        {
            get { return _isBirthday; }
            private set
            {
                _isBirthday = value;
                OnPropertyChanged("IsBirthday");
            }
        }

        private Person()
        {

        }

        internal Person(string name, string surname, string email, Object birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            if (birthDate is DateTime)
            {
                BirthDate = (DateTime?)birthDate;

                ReCalcAdditionalInfo();
            }
            else
            {
                BirthDate = null;

                IsAdult = null;
                SunSign = null;
                ChineseSign = null;
                IsBirthday = null;
            }
        }

        internal Person(string name, string surname, string email):this(name,surname,email, null)
        {
        }

        internal Person(string name, string surname, DateTime birthDate):this(name, surname, "", birthDate)
        {
        }

        private bool IsAdultCalc()
        {
                int age = DateTime.Now.Year - BirthDate.Value.Year;
                if (BirthDate > DateTime.Now.AddYears(-age))
                    age--;

                return (age > 18);
        }

        internal string SunSignCalc()
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

        internal string ChineseSignCalc()
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

        internal bool IsBirthdayCalc()
        {
                return DateTime.Now.Month.Equals(BirthDate.Value.Month) && DateTime.Now.Day.Equals(BirthDate.Value.Day);
        }

        private void ReCalcAdditionalInfo()
        {
            IsAdult = IsAdultCalc();
            SunSign = SunSignCalc();
            ChineseSign = ChineseSignCalc();
            IsBirthday = IsBirthdayCalc();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
