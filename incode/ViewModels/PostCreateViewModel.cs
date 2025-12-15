using System.ComponentModel.DataAnnotations;

namespace incode.ViewModels
{
    public class PostCreateViewModel
    {
        [Required(ErrorMessage = "標題是必填的")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "內容是必填的")]
        public string PostsContent { get; set; }

        public bool IsCommission { get; set; }
    }
}