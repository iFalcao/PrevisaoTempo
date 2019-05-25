using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPrevisaoTempo.WebApi.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome da cidade é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome da cidade não deve ultrapassar 50 caracteres.")]
        public string Name { get; set; }

        /// <summary>
        /// 3rd party library, currently OpenWeather, uses this code as their City's ID
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(30, ErrorMessage = "O código da cidade não pode ultrapassar 30 caracteres.")]
        public string CustomCode { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public double? Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public double? Longitude { get; set; }

        [MaxLength(3, ErrorMessage = "O código de representação do país é inválido.")]
        public string Country { get; set; }
    }
}
