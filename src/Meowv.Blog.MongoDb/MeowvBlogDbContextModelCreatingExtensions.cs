﻿using Meowv.Blog.Domain.Blog;
using Meowv.Blog.Domain.Hots;
using Meowv.Blog.Domain.Sayings;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Meowv.Blog
{
    public static class MeowvBlogDbContextModelCreatingExtensions
    {
        public static void Configure(this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Post>(b =>
            {
                b.CollectionName = MeowvBlogDbConsts.CollectionNames.Post;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Category>(b =>
            {
                b.CollectionName = MeowvBlogDbConsts.CollectionNames.Category;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Tag>(b =>
            {
                b.CollectionName = MeowvBlogDbConsts.CollectionNames.Tag;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<FriendLink>(b =>
            {
                b.CollectionName = MeowvBlogDbConsts.CollectionNames.FriendLink;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Hot>(b =>
            {
                b.CollectionName = MeowvBlogDbConsts.CollectionNames.Hot;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Saying>(b =>
            {
                b.CollectionName = MeowvBlogDbConsts.CollectionNames.Saying;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });
        }
    }
}