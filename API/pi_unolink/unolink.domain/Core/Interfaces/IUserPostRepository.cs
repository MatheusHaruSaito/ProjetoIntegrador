using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unolink.domain.Models;

namespace unolink.domain.Core.Interfaces
{
    public interface IUserPostRepository: IRepository<UserPost, Guid>
    {
        Task<List<UserPost>> GetAll();
        Task<bool> UseTriggerActive(Guid id);
        Task<PostComment> Comment(Guid postId,Guid userId, string text);
        Task<bool> VoteComment(Guid commentId, Guid userId);
        Task<bool> Vote(Guid postId, Guid userId);
        Task<int> VoteCount(Guid postId);
        Task<int> CommentVoteCount(Guid commentId);

        Task<List<(Guid PostId, int Count)>> GetVotesCountByPostIdsAsync(List<Guid> postIds);
        Task<List<(Guid commentId, int Count)>> GetCommentVotesCountByCommentIdsAsync(List<Guid> commentIds);
        Task<List<(Guid PostId, int Count)>> GetCommentCountByPostIdsAsync(List<Guid> postIds);

    }
}
