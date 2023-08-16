using Microsoft.Build.Framework;

namespace UTS.Models
{
    public class horario_agendaModel
    {
        public int idhorario { get; set; }
        public int idaula2 { get; set; }
        [Required] 
        public string dia { get; set; }
        [Required]

        public string mes { get; set; }
        [Required]

        public string? years { get; set; }
        public string hora { get; set; }
        



    }
}
