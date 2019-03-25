using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace NaUKMA.CS.Practice02
{
    class PersonVm : INotifyPropertyChanged
    {
        internal PersonVm()
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

        private String _currentName;

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

        private String _currentSurname;

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

        private String _currentEmail;

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

        private DateTime _currentBirthDate;

        #endregion

        //output properties

        #region ResultName

        public string ResultName
        {
            get { return _resultName; }
            private set
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
            private set
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
            private set
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
            private set
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
            private set
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
            private set
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
            private set
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
            private set
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
            private set
            {
                _resultBdNote = value;
                OnPropertyChanged();
            }
        }

        private string _resultBdNote;

        #endregion

        //collection
        #region PeopleCollectionVM

        public ObservableCollection<Person> PeopleCollectionVM
        {
            get
            {
                if (_peopleCollectionVM is null)
                {
                    LoadPeopleList();
                }
                return _peopleCollectionVM;
            }
            private set
            {
                _peopleCollectionVM = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Person> _peopleCollectionVM;

        #endregion

        #region PeopleCollectionVMFilteredOut

        public ObservableCollection<Person> PeopleCollectionVMFilteredOut
        {
            get
            {
                return _peopleCollectionVMFilteredOut;
            }
            private set
            {
                _peopleCollectionVMFilteredOut = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Person> _peopleCollectionVMFilteredOut;

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
                               await Task.Run(() => CheckAndProceed());

                           }, o => CanAddCommand()));
            }
        }

        private ICommand _closingCommand;

        public ICommand ClosingCommand
        {
            get
            {
                return _closingCommand ?? (_closingCommand = new RelayCommand<object>(
                            o =>
                           {
                               SavePeopleList();

                           }));
            }
        }

        //logic

        private bool CanAddCommand()
        {
            return !string.IsNullOrEmpty(CurrentName) && !string.IsNullOrEmpty(CurrentSurname) && !string.IsNullOrEmpty(CurrentEmail) && !string.IsNullOrEmpty(CurrentBirthDate.ToString()) && PeopleCollectionVMFilteredOut is null;
        }

        private async Task CheckAndProceed()
        {
            //check
            try
            {
                CheckAge();
                CheckEmail();
            }
            catch (BirthDateException)
            {
                MessageBox.Show("This program works only for people born on 19.02.1901 (UKR locale) or later, up to today.");
                return;
            }
            catch (EmailException)
            {
                MessageBox.Show("Please, enter existing email address, which was not already added.");
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

            //call ui thread to modify the collection
            await App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                PeopleCollectionVM.Add(tmp);
            });

        }

        private void CheckAge()
        {
            long ticks = new DateTime(1901, 02, 19, 00, 00, 00, new CultureInfo("en-US", false).Calendar).Ticks;
            DateTime dtStart = new DateTime(ticks);

            if (!(CurrentBirthDate >= dtStart && CurrentBirthDate <= DateTime.Today))
            {
                //throw new BirthDateException("Person must be already born.");
                throw new BirthDateException();
            }
        }

        private void CheckEmail()
        {
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(CurrentEmail);
                if (PeopleCollectionVM.Any(p => p.Email.Equals(CurrentEmail)))
                {
                    throw new EmailException();
                }
            }
            catch
            {
                //throw new EmailException("Please, enter existing email address.");
                throw new EmailException();
            }
        }

        private void SavePeopleList()
        {
            MergePeople();

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));
            try
            {
                using (FileStream stream = new FileStream("People.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, PeopleCollectionVM);
                }

                MessageBoxResult ok = MessageBox.Show("Users' data saved.");
            }
            catch
            {
                MessageBoxResult ok = MessageBox.Show("Could not save users' data.");
            }
        }

        private void LoadPeopleList()
        {
            _peopleCollectionVM = new ObservableCollection<Person>();

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));

            try
            {
                using (FileStream stream = new FileStream("People.xml", FileMode.Open))
                {
                    IEnumerable<Person> personData = (IEnumerable<Person>) serializer.Deserialize(stream);

                    foreach (Person p in personData)
                    {
                        PeopleCollectionVM.Add(p);
                    }
                }

            }
            catch (FileNotFoundException)
            {
                //file not found -> first launch of the application detected (or possible reset via removal of the 'People.xml')                

                for (int i = 0; i < 50; i++)
                {
                    _peopleCollectionVM.Add(new Person("a" + i, "b" + i, "a" + i + "@b.c",
                        new DateTime(1960 + i, 03, 01)));
                }
            }
            catch (Exception ex)
            {
                //suppress other ex-s.

                //MessageBox.Show(ex.ToString());
            }
        }

        private void MergePeople()
        {
            if (!(PeopleCollectionVMFilteredOut is null))
            {
                PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Concat(PeopleCollectionVMFilteredOut).OrderBy(x => x.Name));
                PeopleCollectionVMFilteredOut = null;
            }
        }

        //event handling
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //datagrid validation
        //public class CourseValidationRule : ValidationRule
        //{
        //    public override ValidationResult Validate(object value,
        //        System.Globalization.CultureInfo cultureInfo)
        //    {
        //        Course course = (value as BindingGroup).Items[0] as Course;
        //        if (course.StartDate > course.EndDate)
        //        {
        //            return new ValidationResult(false,
        //                "Start Date must be earlier than End Date.");
        //        }
        //        else
        //        {
        //            return ValidationResult.ValidResult;
        //        }
        //    }
        //}

        //datagrid / PeopleCollectionVM logic
        #region SelectedPerson

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        private Person _selectedPerson;

        #endregion

        private ICommand _deleteUserCommand;

        public ICommand DeleteUserCommand
        {
            get
            {
                return _deleteUserCommand ?? (_deleteUserCommand = new RelayCommand<object>(
                           async o =>
                           {
                               await Task.Run(() => DeleteUser());

                           }, o => CanDeleteCommand()));
            }
        }

        private bool CanDeleteCommand()
        {
            return !(SelectedPerson is null);
        }

        private async Task DeleteUser()
        {
            //call ui thread to modify the collection
            await App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                PeopleCollectionVM.Remove(SelectedPerson);
            });
        }
        //filtering
        #region CurrentFilterText

        public String CurrentFilterText
        {
            get { return _currentFilterText; }
            set
            {
                _currentFilterText = value;
                OnPropertyChanged(nameof(CurrentFilterText));
            }
        }

        private String _currentFilterText;

        #endregion

        #region CurrentFilterType

        public String CurrentFilterType
        {
            get { return _currentFilterType; }
            set
            {
                _currentFilterType = value;
                OnPropertyChanged(nameof(CurrentFilterType));
            }
        }

        private String _currentFilterType;

        #endregion

        private ICommand _filterUserCommand;

        public ICommand FilterUserCommand
        {
            get
            {
                return _filterUserCommand ?? (_filterUserCommand = new RelayCommand<object>(
                           async o =>
                           {
                               await Task.Run(() => FilterUsers());

                           }, o => CanFilterCommand()));
            }
        }

        private bool CanFilterCommand()
        {
            return !string.IsNullOrEmpty(CurrentFilterText) && !string.IsNullOrEmpty(CurrentFilterType);
        }

        private async Task FilterUsers()
        {
            MergePeople();

            switch (CurrentFilterType)
            {
                case "Name":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.Name.Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.Name.Contains(CurrentFilterText)));
                    return;
                case "Surname":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.Surname.Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.Surname.Contains(CurrentFilterText)));
                    return;
                case "Email":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.Email.Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.Email.Contains(CurrentFilterText)));
                    return;
                case "Birth Date":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.BirthDate.ToString().Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.BirthDate.ToString().Contains(CurrentFilterText)));
                    return;
                case "Is Adult":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.IsAdult.ToString().Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.IsAdult.ToString().Contains(CurrentFilterText)));
                    return;
                case "Sun Sign":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.SunSign.Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.SunSign.Contains(CurrentFilterText)));
                    return;
                case "Chinese Sign":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.ChineseSign.Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.ChineseSign.Contains(CurrentFilterText)));
                    return;
                case "Has Birthday Today":
                    PeopleCollectionVMFilteredOut = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => !x.IsBirthday.ToString().Contains(CurrentFilterText)));
                    PeopleCollectionVM = new ObservableCollection<Person>(PeopleCollectionVM.Where(x => x.IsBirthday.ToString().Contains(CurrentFilterText)));
                    return;
                default:
                    return;
            }
        }
        //ResetFilterCommand
        //merge 2 collections back and nullify the CurrentFilterText/CurrentFilterType, as well as the 2nd collection, leaving only PeopleCollVM
        private ICommand _resetFilterCommand;

        public ICommand ResetFilterCommand
        {
            get
            {
                return _resetFilterCommand ?? (_resetFilterCommand = new RelayCommand<object>(
                           async o =>
                           {
                               await Task.Run(() => ResetFilter());

                           }, o => CanResetFilterCommand()));
            }
        }

        private bool CanResetFilterCommand()
        {
            return !string.IsNullOrEmpty(CurrentFilterText) || !string.IsNullOrEmpty(CurrentFilterType);
        }

        private async Task ResetFilter()
        {
            MergePeople();
            CurrentFilterText = null;
            CurrentFilterType = null;
        }
    }
}
