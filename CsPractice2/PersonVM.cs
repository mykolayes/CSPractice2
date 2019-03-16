using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NaUKMA.CS.Practice02
{
    class PersonVm : INotifyPropertyChanged
    {
        public PersonVm()
        {
            CurrentBirthDate = DateTime.Today;
        }

        //input properties
        #region CurrentName

        public String CurrentName
        {
            get { return _currentName; }
            set
            {
                _currentName = value;
                OnPropertyChanged(nameof(CurrentName));
            }
        }

        String _currentName;

        #endregion

        #region CurrentSurname

        public String CurrentSurname
        {
            get { return _currentSurname; }
            set
            {
                _currentSurname = value;
                OnPropertyChanged(nameof(CurrentSurname));
            }
        }

        String _currentSurname;

        #endregion

        #region CurrentEmail

        public String CurrentEmail
        {
            get { return _currentEmail; }
            set
            {
                _currentEmail = value;
                OnPropertyChanged(nameof(CurrentEmail));
            }
        }

        String _currentEmail;

        #endregion

        #region CurrentBirthDate

        public DateTime CurrentBirthDate
        {
            get { return _currentBirthDate; }
            set
            {
                _currentBirthDate = value;
                OnPropertyChanged(nameof(CurrentBirthDate));
            }
        }

        DateTime _currentBirthDate;

        #endregion

        //output properties

        #region ResultName

        public string ResultName
        {
            get { return _resultName; }
            set
            {
                _resultName = value;
                OnPropertyChanged();
            }
        }

        private string _resultName;

        #endregion

        #region ResultSurname

        public string ResultSurname
        {
            get { return _resultSurname; }
            set
            {
                _resultSurname = value;
                OnPropertyChanged();
            }
        }

        private string _resultSurname;

        #endregion

        #region ResultEmail

        public string ResultEmail
        {
            get { return _resultEmail; }
            set
            {
                _resultEmail = value;
                OnPropertyChanged();
            }
        }

        private string _resultEmail;

        #endregion

        #region ResultBirthDate

        public string ResultBirthDate
        {
            get { return _resultBirthDate; }
            set
            {
                _resultBirthDate = value;
                OnPropertyChanged();
            }
        }

        private string _resultBirthDate;

        #endregion

        #region ResultIsAdult

        public string ResultIsAdult
        {
            get { return _resultIsAdult; }
            set
            {
                _resultIsAdult = value;
                OnPropertyChanged();
            }
        }

        private string _resultIsAdult;

        #endregion

        #region ResultSunSign

        public string ResultSunSign
        {
            get { return _resultSunSign; }
            set
            {
                _resultSunSign = value;
                OnPropertyChanged();
            }
        }

        private string _resultSunSign;

        #endregion

        #region ResultChineseSign

        public string ResultChineseSign
        {
            get { return _resultChineseSign; }
            set
            {
                _resultChineseSign = value;
                OnPropertyChanged();
            }
        }

        private string _resultChineseSign;

        #endregion

        #region ResultIsBirthday

        public string ResultIsBirthday
        {
            get { return _resultIsBirthday; }
            set
            {
                _resultIsBirthday = value;
                OnPropertyChanged();
            }
        }

        private string _resultIsBirthday;

        #endregion

        #region ResultBdNote

        public string ResultBdNote
        {
            get { return _resultBdNote; }
            set
            {
                _resultBdNote = value;
                OnPropertyChanged();
            }
        }

        private string _resultBdNote;

        #endregion

        //commands

        private ICommand _checkAndProceedCommand;

        public ICommand CheckAndProceedCommand
        {
            get
            {
                return _checkAndProceedCommand ?? (_checkAndProceedCommand = new RelayCommand<object>(
                           async o =>
                           {
                               await CheckAndProceed();

                           }, o => CanExecuteCommand()));
            }
        }

        //logic

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrEmpty(CurrentName) && !string.IsNullOrEmpty(CurrentSurname) && !string.IsNullOrEmpty(CurrentEmail) && !string.IsNullOrEmpty(CurrentBirthDate.ToString());
        }

        private async Task CheckAndProceed()
        {
            //check
            try
            {
                await CheckAge();
                await CheckEmail();
            }
            catch (BirthDateException ex)
            {
                MessageBox.Show("This program works only for people born on 02.19.1901 or later, up to today.");
                return;
            }
            catch (EmailException ex)
            {
                MessageBox.Show("Please, enter existing email address.");
                return;
            }

            //proceed
            if (DateTime.Now.Month.Equals(CurrentBirthDate.Month) &&
                DateTime.Now.Day.Equals(CurrentBirthDate.Day))
            {
                ResultBdNote = "Happy bd!";
            }
            else
            {
                ResultBdNote = "";
            }
            
            Person tmp = new Person(CurrentName, CurrentSurname, CurrentEmail, CurrentBirthDate);

            ResultName = tmp.Name;
            ResultSurname = tmp.Surname;
            ResultEmail = tmp.Email;
            ResultBirthDate = tmp.BirthDate.Value.ToLongDateString();
            ResultIsAdult = tmp.IsAdult.ToString();
            ResultSunSign = tmp.SunSign;
            ResultChineseSign = tmp.ChineseSign;
            ResultIsBirthday = tmp.IsBirthday.ToString();
        }

        private async Task CheckAge()
        {
            long ticks = new DateTime(1901, 02, 19, 00, 00, 00, new CultureInfo("en-US", false).Calendar).Ticks;
            DateTime dtStart = new DateTime(ticks);
            
            if (!(CurrentBirthDate > dtStart && CurrentBirthDate <= DateTime.Today))
            {
                //throw new BirthDateException("Person must be already born.");
                throw new BirthDateException();
            }
        }

        private async Task CheckEmail()
        {    
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(CurrentEmail);
            }
            catch
            {
                //throw new EmailException("Please, enter existing email address.");
                throw new EmailException();
            }
        }

        //event handling
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
