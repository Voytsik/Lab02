using System;
using Lab02.Tools;
using Lab02.ViewModels;
using Lab02.Manager;

namespace Lab02.Models
{
    public class Person : BaseViewModel
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthdayDate = DateTime.Today ;
        private readonly bool _isAdult;
        private readonly string _westernZodiac;
        private readonly string _chineseZodiac;
        private readonly bool _isBirthday;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public DateTime DateOfBirth
        {
            get
            {
                return _birthdayDate;
            }
            set
            {
                _birthdayDate = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public bool getIsAdult => _isAdult;

        public string getWesternZodiac => _westernZodiac;

        public string getChineseZodiac => _chineseZodiac;

        public bool getIsBirthdayToday => _isBirthday;

        public int Age { get; set; }

        public Person(string email, string name, string surname) : this(email, name, surname, DateTime.Today)
        {
        }
        public Person(string name, string surname, DateTime dateOfBirth) : this("", name, surname, dateOfBirth)
        {
        }
        public Person(string email, string name, string surname, DateTime dateOfBirth)
        {
            Email = email;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            OperateZodiac zodiac = new OperateZodiac();
            _westernZodiac = zodiac.FindWestZodiac(DateOfBirth);
            _chineseZodiac = zodiac.FindChineseZodiac(DateOfBirth.Year);
            _isBirthday = zodiac.isBirthday(DateOfBirth);
            if (DateTime.Today > DateOfBirth)
            {
                if (DateTime.Today.Month > DateOfBirth.Month)
                {
                    Age = DateTime.Today.Year - DateOfBirth.Year;
                }
                else if (DateTime.Today.Month == DateOfBirth.Month && DateTime.Today.Day > DateOfBirth.Day)
                {
                    Age = DateTime.Today.Year - DateOfBirth.Year;
                }
                else if (DateTime.Today.Month == DateOfBirth.Month && DateTime.Today.Day == DateOfBirth.Day)
                {
                    Age = DateTime.Today.Year - DateOfBirth.Year;
                }
                else
                {
                    Age = DateTime.Today.Year - DateOfBirth.Year - 1;
                }
            }
            else
            {
                Age = 0;
            }
            _isAdult = CalcAdult();
        }
        public Person()
        {
        }

        private bool CalcAdult()
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;
            if (age > 17)
                return true;
            return false;
        }
    }
}