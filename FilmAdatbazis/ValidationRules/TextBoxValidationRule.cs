using System.Globalization;
using System.Windows.Controls;

namespace FilmAdatbazis.ValidationRules
{
    /// <summary>
    /// Validációs szabályok a szöveg bevitelhez
    /// </summary>
    class TextBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            // Nem lett adat beírva?
            if (string.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(false, "Nincs megadva érték!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
