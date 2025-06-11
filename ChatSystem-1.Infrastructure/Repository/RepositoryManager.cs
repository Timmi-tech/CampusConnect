using ChatSystem_1.Domain.Contracts;

namespace ChatSystem_1.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext? _repositoryContext;
        private readonly Lazy<IUserProfileRepository>?_userProfileRepository;
        private readonly Lazy<IPostRepository>? _postRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userProfileRepository = new Lazy<IUserProfileRepository>(() => new UserProfileRepository(repositoryContext));
            _postRepository = new Lazy<IPostRepository>(() => new PostRepository(repositoryContext));
        }

        public IUserProfileRepository User => _userProfileRepository.Value;
        public IPostRepository Post => _postRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.
        SaveChangesAsync();
    }
}