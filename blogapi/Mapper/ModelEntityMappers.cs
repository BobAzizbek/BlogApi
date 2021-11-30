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
}