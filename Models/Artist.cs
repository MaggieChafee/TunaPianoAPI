using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TunaPianoAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Bio {  get; set; }
    }
}
