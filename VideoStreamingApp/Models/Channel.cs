using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStreamingApp.Models
{
    public class Channel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ContentProviderId { get; set; }
        [ForeignKey("ContentProviderId")]
        public IdentityUser ContentProvider { get; set; }

        public ICollection<Video> Videos { get; set; }
    }
}
