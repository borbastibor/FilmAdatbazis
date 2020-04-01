using System.Globalization;
using System.Windows.Controls;

namespace FilmAdatbazis.ValidationRules
{
    /// <summary>
    /// Validációs szabályok a szám bevitelhez
    /// </summary>
    class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;
                        
            // Számot adtak meg
            if (!int.TryParse(str, out int number))
            {
                return new ValidationResult(false, "Nem számot adott meg!");
            }

            // A megadott szám az 0 - 10000 között van
            if (number < 0 || number > 10000)
            {
                return new ValidationResult(false, "A megadott szám 0 és 10000 között kell legyen!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
