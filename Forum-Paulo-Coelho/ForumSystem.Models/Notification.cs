namespace ForumSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Message { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
