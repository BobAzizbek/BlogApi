namespace blogapi.Mapper;
public static class ModelEntityMappers
{
    public static Post ToEntity(this PostModel post)
    {
        return new Post(
            headerImageId: post.HeaderImageId,
            title: post.Title,
            description: post.Description,
            content: post.Content,
            comments: null,
            medias: null);
    }
    public static Comment ToEntity(this CommentModel comment)
        => new Comment(
            author: comment.Author,
            content: comment.Content,
            state: comment.State.ToModelEntityECommentState(),
            postId: comment.PostId
        );

    public static Entities.ECommentState ToModelEntityECommentState(this Models.ECommentState state)
        => state switch
        {
            Models.ECommentState.Approved => Entities.ECommentState.Approved,
            Models.ECommentState.Pending => Entities.ECommentState.Pending,
            Models.ECommentState.Rejected => Entities.ECommentState.Rejected
        };
}