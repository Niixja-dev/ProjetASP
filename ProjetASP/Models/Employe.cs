using System.ComponentModel.DataAnnotations;

namespace ProjetASP.Models
{
    public class Employe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le matricule est obligatoire")]
        [StringLength(20)]
        [Display(Name = "Matricule")]
        public string Matricule { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Téléphone")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "La date d'embauche est obligatoire")]
        [DataType(DataType.Date)]
        [Display(Name = "Date d'embauche")]
        public DateTime DateEmbauche { get; set; }

        [Required(ErrorMessage = "Le salaire de base est obligatoire")]
        [Range(0, double.MaxValue, ErrorMessage = "Le salaire doit être positif")]
        [DataType(DataType.Currency)]
        [Display(Name = "Salaire de base")]
        public decimal SalaireBase { get; set; }

        [StringLength(100)]
        [Display(Name = "Poste")]
        public string? Poste { get; set; }

        [StringLength(100)]
        [Display(Name = "Département")]
        public string? Departement { get; set; }

        [Display(Name = "Solde congés (jours)")]
        public int SoldeConges { get; set; } = 30;

        [Display(Name = "Actif")]
        public bool EstActif { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Conge> Conges { get; set; } = new List<Conge>();
        public virtual ICollection<Paie> Paies { get; set; } = new List<Paie>();

        [Display(Name = "Nom complet")]
        public string NomComplet => $"{Prenom} {Nom}";
    }
}
