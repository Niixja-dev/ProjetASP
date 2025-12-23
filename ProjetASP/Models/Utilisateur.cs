using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetASP.Models
{
    public class Utilisateur : IdentityUser
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = string.Empty;

        [Display(Name = "Date de création")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Display(Name = "Actif")]
        public bool EstActif { get; set; } = true;

        [Display(Name = "Nom complet")]
        public string NomComplet => $"{Prenom} {Nom}";
    }
}
