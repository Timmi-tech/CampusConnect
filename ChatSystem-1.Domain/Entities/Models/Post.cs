using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChatSystem_1.Domain.Entities.Models
{
    public class Post
    {
        [Key]
        [Column("PostId")]
        public int Id { get; set; }
        public string Caption { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to the User entity
        public string UserId { get; set; } = string.Empty;

        // Navigation property to the User entity
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        // List of images associated with the post
        public ICollection<PostImage> PostImages { get; set; } = new List<PostImage>();
    }
    public class PostImage
    {
        [Key]
        [Column("PostImageId")]
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // Foreign key to the Post entity
        public int PostId { get; set; }
        // Navigation property to the Post entity
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;
    }

}