using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataValidation
{
    public static class DataValidation
    {
        public static bool ValidName(string name, out string errorMessage)
        {
            errorMessage = "";
            
            if (name.Length == 0)
            {
                errorMessage = "Imie wymagane.";
                return false;
            }

            if (name.Length > 20)
            {
                errorMessage = "Imie za długie. Maksymalna ilość znaków: 20";
                return false;
            }

            string expression = "^[A-Z][a-z]*$";

            if (Regex.IsMatch(name, expression))
            {
                return true;
            }

            errorMessage = "Imie powinno mieć odpowiedni format.\n"+"Na przykład: 'Jan'";
            return false;
        }

        public static bool ValidSurname(string surname, out string errorMessage)
        {
            errorMessage = "";
            
            if (surname.Length == 0)
            {
                errorMessage = "Nazwisko wymagane.";
                return false;
            }

            if (surname.Length > 45)
            {
                errorMessage = "Nazwisko za długie. Maksymalna ilość znaków: 45";
                return false;
            }

            string expression = "^[A-Z][a-z]*$";

            if (Regex.IsMatch(surname, expression))
            {
                return true;
            }

            errorMessage = "Nazwisko powinno mieć odpowiedni format.\n" + "Na przykład: 'Kowalski'";
            return false;
        }

        public static bool ValidPesel(string pesel, out string errorMessage)
        {
            errorMessage = "";
            
            if (pesel.Length == 0)
            {
                errorMessage = "Pesel wymagany.";
                return false;
            }

            if (pesel.Length > 11 || pesel.Length < 11)
            {
                errorMessage = "Pesel - niepoprawna ilość znaków. Wymagana ilość znaków: 11";
                return false;
            }

            string expression = "^[0-9]*$";

            if (Regex.IsMatch(pesel, expression))
            {
                return true;
            }

            errorMessage = "Pesel powinien mieć odpowiedni format.\n" + "Na przykład: '12345678900'";
            return false;
        }

        public static bool ValidDateOfBirth(string date, out string errorMessage)
        {
            errorMessage = "";
            
            if (date.Length == 0)
            {
                errorMessage = "Data urodzenia wymagana.";
                return false;
            }

            //if (date.Length > 10 || date.Length < 10)
            //{
            //    errorMessage = "Niepoprawna ilość znaków. Wymagana ilość znaków: 10";
            //    return false;
            //}

            string expression = "^\\d{4}-\\d{2}-\\d{2}$";

            if (Regex.IsMatch(date, expression))
            {
                return true;
            }

            errorMessage = "Data urodzenia powinna mieć odpowiedni format.\n" + "Na przykład: '1970-01-01'";
            return false;
        }

        public static bool ValidSex(string sex, out string errorMessage)
        {
            errorMessage = "";
            
            if (sex.Length == 0)
            {
                errorMessage = "Płeć wymagana.";
                return false;
            }

            if (sex.Length > 1)
            {
                errorMessage = "Płeć - niepoprawna ilość znaków. Wymagana ilość znaków: 1";
                return false;
            }

            string expression = "^[mk]$";

            if (Regex.IsMatch(sex, expression))
            {
                return true;
            }

            errorMessage = "Płeć powinna mieć odpowiedni format.\n" + "Na przykład: 'm' lub 'k'";
            return false;
        }

        public static bool ValidContactNumber(string number, out string errorMessage)
        {
            errorMessage = "";
            
            if (number.Length == 0)
            {
                errorMessage = "Numer kontaktowy wymagany.";
                return false;
            }

            if (number.Length > 9 || number.Length < 9)
            {
                errorMessage = "Numer kontaktowy - niepoprawna ilość znaków. Wymagana ilość znaków: 9";
                return false;
            }

            string expression = "^[0-9]*$";

            if (Regex.IsMatch(number, expression))
            {
                return true;
            }

            errorMessage = "Numer kontaktowy powinien mieć odpowiedni format.\n" + "Na przykład: '123456789'";
            return false;
        }

        public static bool ValidCountry(string country, out string errorMessage)
        {
            errorMessage = "";
            
            if (country.Length == 0)
            {
                errorMessage = "Kraj wymagany.";
                return false;
            }

            if (country.Length > 30)
            {
                errorMessage = "Nazwa kraju za długa. Maksymalna ilość znaków: 30";
                return false;
            }

            string expression = "^[A-Z][a-z]*$";

            if (Regex.IsMatch(country, expression))
            {
                return true;
            }

            errorMessage = "Kraj powinien mieć odpowiedni format.\n" + "Na przykład: 'Polska'";
            return false;
        }

        public static bool ValidCity(string city, out string errorMessage)
        {
            errorMessage = "";
            
            if (city.Length == 0)
            {
                errorMessage = "Miasto wymagane.";
                return false;
            }

            if (city.Length > 30)
            {
                errorMessage = "Nazwa miasta za długa. Maksymalna ilość znaków: 30";
                return false;
            }

            string expression = "^[A-Z][a-z]*$";

            if (Regex.IsMatch(city, expression))
            {
                return true;
            }

            errorMessage = "Miasto powinno mieć odpowiedni format.\n" + "Na przykład: 'Wrocław'";
            return false;
        }

        public static bool ValidStreet(string street, out string errorMessage)
        {
            errorMessage = "";
            
            if (street.Length == 0)
            {
                errorMessage = "Ulica wymagana.";
                return false;
            }

            if (street.Length > 60)
            {
                errorMessage = "Nazwa miasta za długa. Maksymalna ilość znaków: 60";
                return false;
            }

            string expression = "^[A-Z][a-z]*$";

            if (Regex.IsMatch(street, expression))
            {
                return true;
            }

            errorMessage = "Ulica powinna mieć odpowiedni format.\n" + "Na przykład: 'Opolska'";
            return false;
        }

        public static bool ValidHouseNumber(string houseNumber, out string errorMessage)
        {
            errorMessage = "";
            
            if (houseNumber.Length == 0)
            {
                errorMessage = "Numer domu wymagany.";
                return false;
            }

            if (houseNumber.Length > 10)
            {
                errorMessage = "Numer domu za długi. Maksymalna ilość znaków: 10";
                return false;
            }

            string expression = "^[0-9]+[A-Za-z]*$";

            if (Regex.IsMatch(houseNumber, expression))
            {
                return true;
            }

            errorMessage = "Numer domu powinien mieć odpowiedni format.\n" + "Na przykład: '12a'";
            return false;
        }

        public static bool ValidApartmentNumber(string apartment, out string errorMessage)
        {
            errorMessage = "";
            
            //if (apartment.Length == 0)
            //{
            //    errorMessage = "Numer lokalu wymagany.";
            //    return false;
            //}

            if (apartment.Length > 10)
            {
                errorMessage = "Numer lokalu za długi. Maksymalna ilość znaków: 10";
                return false;
            }

            string expression = "^[0-9]*$";

            if (Regex.IsMatch(apartment, expression))
            {
                return true;
            }

            errorMessage = "Numer lokalu powinien mieć odpowiedni format.\n" + "Na przykład: '12'";
            return false;
        }

        public static bool ValidPostalCode(string code, out string errorMessage)
        {
            errorMessage = "";
            
            if (code.Length == 0)
            {
                errorMessage = "Kod pocztowy wymagany.";
                return false;
            }

            if (code.Length > 5)
            {
                errorMessage = "Kod pocztowy za długi. Maksymalna ilość znaków: 5";
                return false;
            }

            string expression = "^[0-9]*$";

            if (Regex.IsMatch(code, expression))
            {
                return true;
            }

            errorMessage = "Kod pocztowy powinien mieć odpowiedni format.\n" + "Na przykład: '45820'";
            return false;
        }

        public static bool ValidLogin(string login, out string errorMessage)
        {
            errorMessage = "";
            
            if (login.Length == 0)
            {
                errorMessage = "Login wymagany.";
                return false;
            }

            if (login.Length > 15)
            {
                errorMessage = "Login za długi. Maksymalna ilość znaków: 15";
                return false;
            }

            string expression = "[A-Z]+[a-z]+[0-9]+";

            if (Regex.IsMatch(login, expression))
            {
                return true;
            }

            errorMessage = "Login powinien mieć odpowiedni format.\n" + "Powinien zawierać dużą literę, małą literę i cyfrę.\n" + "Na przykład: 'Login123'";
            return false;
        }

        public static bool ValidPassword(string password, out string errorMessage)
        {
            errorMessage = "";
            
            if (password.Length == 0)
            {
                errorMessage = "Hasło wymagane.";
                return false;
            }

            if (password.Length > 15)
            {
                errorMessage = "Hasło za długie. Maksymalna ilość znaków: 15";
                return false;
            }

            if (password.Length < 6)
            {
                errorMessage = "Hasło za krótkie. Minimalna ilość znaków: 6";
                return false;
            }

            string expression = "[A-Z]+[a-z]+[0-9]+";

            if (Regex.IsMatch(password, expression))
            {
                return true;
            }

            errorMessage = "Hasło powinno mieć odpowiedni format.\n" + "Powinno zawierać dużą literę, małą literę i cyfrę.\n" + "Na przykład: 'Password123'";
            return false;
        }

        public static bool ValidRole(string role, out string errorMessage)
        {
            errorMessage = "";
            
            if (role.Length == 0)
            {
                errorMessage = "Rola wymagana.";
                return false;
            }

            if (role.Length > 1)
            {
                errorMessage = "Rola - niepoprawna ilość znaków. Wymagana ilość znaków: 1";
                return false;
            }

            string expression = "^[spda]$";

            if (Regex.IsMatch(role, expression))
            {
                return true;
            }

            errorMessage = "Rola powinna mieć odpowiedni format.\n" + "Na przykład: 's'";
            return false;
        }

        public static bool ValidStudyField(string field, out string errorMessage)
        {
            errorMessage = "";
            
            if (field.Length == 0)
            {
                errorMessage = "Kierunek wymagany.";
                return false;
            }

            if (field.Length > 30)
            {
                errorMessage = "Nazwa kierunku za długa. Maksymalna ilość znaków: 30";
                return false;
            }

            string expression = "^[A-Z][a-z]*$";

            if (Regex.IsMatch(field, expression))
            {
                return true;
            }

            errorMessage = "Nazwa kierunku powinna mieć odpowiedni format.\n" + "Na przykład: 'Automatyka'";
            return false;
        }

        public static bool ValidDegree(string degree, out string errorMessage)
        {
            errorMessage = "";
            
            if (degree.Length == 0)
            {
                errorMessage = "Stopień studiów wymagany.";
                return false;
            }

            if (degree.Length > 1)
            {
                errorMessage = "Stopień studiów za długi. Maksymalna ilość znaków: 1";
                return false;
            }

            string expression = "^[0-9]$";

            if (Regex.IsMatch(degree, expression))
            {
                return true;
            }

            errorMessage = "Stopień studiów powinien mieć odpowiedni format.\n" + "Na przykład: '1'";
            return false;
        }

        public static bool ValidSemestr(string semestr, out string errorMessage)
        {
            errorMessage = "";

            if (semestr.Length == 0)
            {
                errorMessage = "Semestr wymagany.";
                return false;
            }

            if (semestr.Length > 1)
            {
                errorMessage = "Semestr za długi. Maksymalna ilość znaków: 1";
                return false;
            }

            string expression = "^[0-9]$";

            if (Regex.IsMatch(semestr, expression))
            {
                return true;
            }

            errorMessage = "Semestr powinien mieć odpowiedni format.\n" + "Na przykład: '1'";
            return false;
        }
    }
}
