using System;
using System.ComponentModel.DataAnnotations;
using CarRental.Core.Domain;
using CarRental.Infrastructure.Attributes;

namespace CarRental.Web.Models
{
    public class RentViewModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Podane imię jest za krótkie", MinimumLength = 4)]
        public string FistName { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Display(Name = "Numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(?<!\w)(\(?(\+|00)?48\)?)?[ -]?\d{3}[ -]?\d{3}[ -]?\d{3}(?!\w)", ErrorMessage = "Wprowadzony numer nie jest poprawny")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        [Display(Name = "Miasto")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Podane miasto jest za krótkie", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "Ulica jest wymgana")]
        [Display(Name = "Ulica")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Podana ulica jest za krótka", MinimumLength = 2)]
        public string Street { get; set; }

        [Display(Name = "Typ pojazdu")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "Data wypożyczenia jest wymagana")]
        [Display(Name = "Data wypożyczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateRange(30, ErrorMessage = "Wprowadzona data jest błędna")]
        public DateTime? DateTime { get; set; }
    }
}
