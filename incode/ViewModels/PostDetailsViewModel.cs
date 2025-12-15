using incode.Models;
using System.Collections.Generic;


namespace incode.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public bool IsLikedByCurrentUser { get; set; } // 判斷當前使用者是否按過讚
        public int LikeCount { get; set; }

        // 用來接收新留言的輸入
        public string NewReplyContent { get; set; }
    }
}