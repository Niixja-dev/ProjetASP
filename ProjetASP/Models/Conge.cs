using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetASP.Models
{
    public enum TypeConge
    {
        [Display(Name = "Congé annuel")]
        Annuel,
        [Display(Name = "Congé maladie")]
        Maladie,
        [Display(Name = "Congé maternité")]
        Maternite,
        [Display(Name = "Congé paternité")]
        Paternite,
        [Display(Name = "Congé sans solde")]
        SansSolde,
        [Display(Name = "Congé exceptionnel")]
        Exceptionnel
    }

    public enum StatutConge
    {
        [Display(Name = "En attente")]
        EnAttente,
        [Display(Name = "Approuvé")]
        Approuve,
        [Display(Name = "Refusé")]
        Refuse,
        [Display(Name = "Annulé")]
        Annule
    }

    public class Conge
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'employé est obligatoire")]
        [Display(Name = "Employé")]
        public int EmployeId { get; set; }

        [Required(ErrorMessage = "Le type de congé est obligatoire")]
        [Display(Name = "Type de congé")]
        public TypeConge Type { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de début")]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "La date de fin est obligatoire")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de fin")]
        public DateTime DateFin { get; set; }

        [StringLength(500)]
        [Display(Name = "Motif")]
        public string? Motif { get; set; }

        [Display(Name = "Statut")]
        public StatutConge Statut { get; set; } = StatutConge.EnAttente;

        [DataType(DataType.Date)]
        [Display(Name = "Date de demande")]
        public DateTime DateDemande { get; set; } = DateTime.Now;

        [Display(Name = "Commentaire RH")]
        public string? CommentaireRH { get; set; }

        // Navigation property
        [ForeignKey("EmployeId")]
        public virtual Employe? Employe { get; set; }

        [Display(Name = "Nombre de jours")]
        public int NombreJours => (DateFin - DateDebut).Days + 1;
    }
}
