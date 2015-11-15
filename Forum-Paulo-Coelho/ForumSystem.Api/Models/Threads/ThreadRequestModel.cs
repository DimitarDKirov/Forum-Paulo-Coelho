namespace ForumSystem.Api.Models.Threads
{
    using System.ComponentModel.DataAnnotations;

    public class ThreadRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}