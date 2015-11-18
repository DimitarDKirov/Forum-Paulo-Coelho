namespace ForumSystem.Api.Models.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class PostsRequestModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Post {0} must be between 10 and 2000 symbols long")]
        [MaxLength(2000, ErrorMessage = "Post {0} must be between 10 and 2000 symbols long")]
        public string Content { get; set; }
    }
}