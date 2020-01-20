namespace Plugin.Plumber.Component.Sample.Pipelines.IInitializeEnvironmentPipeline
{
    using Bogus;
    using Plugin.Plumber.Component.Sample.Entities;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.ManagedLists;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [PipelineDisplayName("Change to <Project>Constants.Pipelines.Blocks.<Block Name>")]
    public class InitializeEnvironmentBlock : PipelineBlock<string, string, CommercePipelineExecutionContext>
    {
        private const int numberOfWishlistToGenerate = 100;

        protected CommerceCommander Commander { get; set; }

        public InitializeEnvironmentBlock(CommerceCommander commander)
            : base(null)
        {
            this.Commander = commander;
        }

        public override Task<string> Run(string arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: The argument can not be null");

            var fakeWishlist = new Faker<Wishlist>()
                .CustomInstantiator(f =>
                {
                    var guid = Guid.NewGuid();
                    return new Wishlist()
                    {
                        Id = $"{CommerceEntity.IdPrefix<Wishlist>()}{guid}",
                        FriendlyId = guid.ToString()
                    };
                })
                .RuleFor(wl => wl.DisplayName, f => f.Lorem.Sentence())
                .RuleFor(wl => wl.Name, f => f.Lorem.Sentence(3));

            for(int i = 0; i < numberOfWishlistToGenerate; i++)
            {
                var wishlist = fakeWishlist.Generate();                

                wishlist.SetComponent(new ListMembershipsComponent { Memberships = new List<string> { CommerceEntity.ListName<Wishlist>() } });

                wishlist.Lines.Add(new Components.WishlistLineComponent() { ItemId = "Habitat_Master|6042567|56042568" });
                wishlist.Lines.Add(new Components.WishlistLineComponent() { ItemId = "Habitat_Master|6042896|56042896" });

                Commander.PersistEntity(context.CommerceContext, wishlist);
            }

            return Task.FromResult(arg);
        }
    }
}
