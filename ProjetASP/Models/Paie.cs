using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetASP.Models
{
    public class Paie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'employé est obligatoire")]
        [Display(Name = "Employé")]
        public int EmployeId { get; set; }

        [Required(ErrorMessage = "Le mois est obligatoire")]
        [Range(1, 12, ErrorMessage = "Le mois doit être entre 1 et 12")]
        [Display(Name = "Mois")]
        public int Mois { get; set; }

        [Required(ErrorMessage = "L'année est obligatoire")]
        [Display(Name = "Année")]
        public int Annee { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Salaire de base")]
        public decimal SalaireBase { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Primes")]
        public decimal Primes { get; set; } = 0;

        [DataType(DataType.Currency)]
        [Display(Name = "Heures supplémentaires")]
        public decimal HeuresSupplementaires { get; set; } = 0;

        [DataType(DataType.Currency)]
        [Display(Name = "Retenues")]
        public decimal Retenues { get; set; } = 0;

        [DataType(DataType.Currency)]
        [Display(Name = "Cotisations sociales")]
        public decimal CotisationsSociales { get; set; } = 0;

        [DataType(DataType.Currency)]
        [Display(Name = "Impôt sur le revenu")]
        public decimal ImpotRevenu { get; set; } = 0;

        [Display(Name = "Jours travaillés")]
        public int JoursTravailles { get; set; }

        [Display(Name = "Jours d'absence")]
        public int JoursAbsence { get; set; } = 0;

        [DataType(DataType.Date)]
        [Display(Name = "Date de paiement")]
        public DateTime? DatePaiement { get; set; }

        [Display(Name = "Payé")]
        public bool EstPaye { get; set; } = false;

        [StringLength(500)]
        [Display(Name = "Commentaire")]
        public string? Commentaire { get; set; }

        // Navigation property
        [ForeignKey("EmployeId")]
        public virtual Employe? Employe { get; set; }

        // Propriétés calculées
        [Display(Name = "Salaire brut")]
        public decimal SalaireBrut => SalaireBase + Primes + HeuresSupplementaires - Retenues;

        [Display(Name = "Total déductions")]
        public decimal TotalDeductions => CotisationsSociales + ImpotRevenu;

        [Display(Name = "Salaire net")]
        public decimal SalaireNet => SalaireBrut - TotalDeductions;

        [Display(Name = "Période")]
        public string Periode => $"{Mois:D2}/{Annee}";
    }
}

