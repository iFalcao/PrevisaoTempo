using System.ComponentModel.DataAnnotations;

namespace APIPrevisaoTempo.WebApi.DTOs
{
    public class CityDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome da cidade é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome da cidade não deve ultrapassar 50 caracteres.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O código da cidade é obrigatório.")]
        [MaxLength(30, ErrorMessage = "O código da cidade não pode ultrapassar 30 caracteres.")]
        public string CustomCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [MaxLength(3, ErrorMessage = "O código de representação do país é inválido.")]
        public string Country { get; set; }
    }
}
