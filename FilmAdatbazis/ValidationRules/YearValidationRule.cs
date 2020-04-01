using System.Globalization;
using System.Windows.Controls;

namespace FilmAdatbazis.ValidationRules
{
    /// <summary>
    /// Validációs szabályok az évszám bevitel ellenőrzésére
    /// </summary>
    class YearValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;
                        
            // Számot adtak meg
            if (!int.TryParse(str, out int number))
            {
                return new ValidationResult(false, "Nem számot adott meg!");
            }

            // A megadott szám az 1000 - 3000 között van
            if (number < 1000 || number > 3000)
            {
                return new ValidationResult(false, "A megadott szám 1000 és 3000 között kell legyen!");
            }
            return ValidationResult.ValidResult;
        }
    }
}