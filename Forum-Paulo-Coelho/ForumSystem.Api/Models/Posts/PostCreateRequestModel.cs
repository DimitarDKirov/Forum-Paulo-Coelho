namespace ForumSystem.Api.Models.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class PostCreateRequestModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(2000)]
        public string Content { get; set; }
    }
}