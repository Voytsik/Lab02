using Lab02.Manager;
using Lab02.Models;
using Lab02.Tools;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Lab02.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public LoginModel LModel { get; private set; }
        private ICommand _loginCommand;
        private DateTime _birthDate;
        private string _email;
        private string _name;
        private string _surname;
        private int _age;
        private string _westernZodiac;
        private string _chineseZodiac;

        public LoginViewModel(Storage storage)
        {
            LModel = new LoginModel(storage);
            BirthDate = DateTime.Today.Date;
            LModel.UInfoChanged += UIOnDateChanged;
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                InvokePropertyChanged("BirthDate");
            }
        }

        public string UserEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                //InvokePropertyChanged("UserName");
                InvokePropertyChanged("UserEmail");
            }
        }

        public string UserName
        {
            get { return _name; }
            set
            {
                _name = value;
                InvokePropertyChanged("UserName");
                //InvokePropertyChanged("UserSurname");
            }
        }

        public string UserSurname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                InvokePropertyChanged("UserSurname");
                //InvokePropertyChanged("UserEmail");
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand<object>(LoginExecute, LoginCanExecute);
                }
                return _loginCommand;
            }
            set { _loginCommand = value; }
        }

        private bool LoginCanExecute(object obj)
        {
            return true;
        }

        private void LoginExecute(object obj)
        {
            CheckDate(Date);
        }

        public void IsEmailValid(string emailaddress)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            if (Regex.IsMatch(_surname, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Email правильний");
            }
            else
            {
                MessageBox.Show("Некоректний email");
            }
        }

        public void IsCorrectDateOfBirth(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;

            if (date > DateTime.Today)
                MessageBox.Show("Вм ще не народилися.");
            else if (age > 135)
                MessageBox.Show("Вам дійсно більше 135 років?");
        }

        public void CheckDate(DateTime date)
        {
            var age = DateTime.Today.Date.Year - date.Date.Year;
            OperateZodiac zodiac = new OperateZodiac();
            if (zodiac.isBirthday(date.Date))
            {
                MessageBox.Show("Вітаємо з днем народження!");
            }
            IsCorrectDateOfBirth(date);
            IsEmailValid(_surname);
            Person _mainPerson = new Person(_email, _name, _surname, date);

            LModel._storage.ChangeInfo(_mainPerson);
        }

        public DateTime Date
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    InvokePropertyChanged("Date");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

        private void UIOnDateChanged(Person info)
        {
            uName = info.Name;
            uSurname = info.Surname;
            uEmail = info.Email;
            Age = info.Age;
            WesternZodiac = info.getWesternZodiac;
            ChineseZodiac = info.getChineseZodiac;
        }

        public string uName
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    InvokePropertyChanged("uName");
                }
            }
        }

        public string uSurname
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    InvokePropertyChanged("uSurname");
                }
            }
        }

        public string uEmail
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    InvokePropertyChanged("uEmail");
                }
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    InvokePropertyChanged("Age");
                }
            }
        }

        public string WesternZodiac
        {
            get { return _westernZodiac; }
            set
            {
                if (_westernZodiac != value)
                {
                    _westernZodiac = value;
                    InvokePropertyChanged("WesternZodiac");
                }
            }
        }

        public string ChineseZodiac
        {
            get { return _chineseZodiac; }
            set
            {
                if (_chineseZodiac != value)
                {
                    _chineseZodiac = value;
                    InvokePropertyChanged("ChineseZodiac");
                }
            }
        }
    }
}