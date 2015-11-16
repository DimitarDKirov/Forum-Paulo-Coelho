namespace ForumSystem.Api.Models
{
    using ForumSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class CommentDataModel
    {
        public static Expression<Func<Comment, CommentDataModel>> FromComment
        {
            get
            {
                return c => new CommentDataModel
                {
                    UserName = c.User.UserName,
                    Content = c.Content,
                    CommentDate = c.CommentDate,
                    Id = c.Id
                };
            }
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CommentDate { get; set; }

        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}