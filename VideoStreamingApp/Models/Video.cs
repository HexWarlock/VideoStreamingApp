using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStreamingApp.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }

        public DateTime UploadDate { get; set; }

        public string ContentProviderId { get; set; }
        [ForeignKey("ContentProviderId")]
        public IdentityUser ContentProvider { get; set; }
    }
}
