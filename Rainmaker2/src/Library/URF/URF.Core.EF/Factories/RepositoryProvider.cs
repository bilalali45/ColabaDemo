#region

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;

#endregion

namespace URF.Core.EF.Factories
{
    /// <summary>
    ///     Provides an <see cref="IRepository{TEntity}" /> for a client request.
    /// </summary>
    /// <remarks>
    ///     Caches repositories of a given type so that repositories are only created once per provider. Create a new provider
    ///     per client request.
    /// </remarks>
    public class RepositoryProvider : IRepositoryProvider
    {
        /// <summary>
        ///     The <see cref="RepositoryFactories" /> with which to create a new repository.
        /// </summary>
        /// <remarks>
        ///     Should be initialized by constructor injection
        /// </remarks>
        private readonly RepositoryFactories _repositoryFactories;

        public RepositoryProvider(RepositoryFactories repositoryFactories)
        {
            _repositoryFactories = repositoryFactories;
            Repositories = new Dictionary<Type, dynamic>();
        }

        /// <summary>
        ///     Get the dictionary of repository objects, keyed by repository type.
        /// </summary>
        /// <remarks>
        ///     Caller must know how to cast the repository object to a useful type.
        ///     <p>
        ///         This is an extension point. You can register fully made repositories here
        ///         and they will be used instead of the ones this provider would otherwise create.
        ///     </p>
        /// </remarks>
        protected Dictionary<Type, dynamic> Repositories { get; private set; }

        /// <summary>
        ///     Get and set the <see cref="Ef6.DataContext" /> with which to initialize a repository if one must be created.
        /// </summary>
        public DbContext DataContext { get; set; }

        public IUnitOfWork<DbContext> UnitOfWork { get; set; }

        public dynamic GetCustomRepository<T>()
        {
            return GetFactory(typeof(T))(); // get factory and invoke it in 1 fell swoop
        }

        public dynamic GetCustomRepository(Type type)
        {
            return GetFactory(type)(); // get factory and invoke it in 1 fell swoop
        }

        /// <summary>
        ///     Get or create-and-cache the default <see cref="IRepository{T}" /> for an entity of type T.
        /// </summary>
        /// <typeparam name="T">
        ///     Root entity type of the <see cref="IRepository{T}" />.
        /// </typeparam>
        /// <remarks>
        ///     If can't find repository in cache, use a factory to create one.
        /// </remarks>
        public ITrackableRepository<T> GetRepositoryForEntityType<T>() where T : class, ITrackable
        {
            return GetRepository<ITrackableRepository<T>>(_repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        /// <summary>
        ///     Get or create-and-cache a repository of type T.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the repository, typically a custom repository interface.
        /// </typeparam>
        /// <param name="factory">
        ///     An optional repository creation function that takes a DbContext argument
        ///     and returns a repository of T. Used if the repository must be created and
        ///     caller wants to specify the specific factory to use rather than one
        ///     of the injected <see cref="RepositoryFactories" />.
        /// </param>
        /// <remarks>
        ///     Looks for the requested repository in its cache, returning if found.
        ///     If not found, tries to make one using <see cref="MakeRepository{T}" />.
        /// </remarks>
        public virtual T GetRepository<T>(Func<DbContext, IUnitOfWork<DbContext>, dynamic> factory = null) where T : class
        {
            // Look for T dictionary cache under typeof(T).
            dynamic cachedRepository;

            if (Repositories.TryGetValue(typeof(T), out cachedRepository))
            {
                return cachedRepository;
            }

            // Not found or null; make one, add to dictionary cache, and return it.
            return MakeRepository<T>(factory, DataContext, UnitOfWork);
        }

        /// <summary>
        ///     Set the repository for type T that this provider should return.
        /// </summary>
        /// <remarks>
        ///     Plug in a custom repository if you don't want this provider to create one.
        ///     Useful in testing and when developing without a backend
        ///     implementation of the object returned by a repository of type T.
        /// </remarks>
        public void SetRepository<T>(T repository)
        {
            Repositories[typeof(T)] = repository;
        }

        private Func<dynamic> GetFactory(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            Func<dynamic> repository;

            if (_repositoryFactories.CustomRepositoryFactories.TryGetValue(type, out repository))
            {
                return repository;
            }

            throw new NotSupportedException(string.Format("There is no custom repository for the type {0}", type));
        }

        /// <summary>Make a repository of type T.</summary>
        /// <typeparam name="T">Type of repository to make.</typeparam>
        /// <param name="dbContext">
        ///     The <see cref="Ef6.DataContext" /> with which to initialize the repository.
        /// </param>
        /// <param name="factory">
        ///     Factory with <see cref="Ef6.DataContext" /> argument. Used to make the repository.
        ///     If null, gets factory from <see cref="_repositoryFactories" />.
        /// </param>
        /// <param name="unitOfWorkAsync">
        ///     The <see cref="IUnitOfWorkAsync" /> which is passed to the constructor of the <see cref="IRepository{T}" />.
        /// </param>
        /// <returns></returns>
        protected virtual T MakeRepository<T>(Func<DbContext, IUnitOfWork<DbContext>, dynamic> factory, DbContext dbContext, IUnitOfWork<DbContext> unitOfWorkAsync)
        {
            var repositoryFactory = factory ?? _repositoryFactories.GetRepositoryFactory<T>();

            if (repositoryFactory == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
            }

            var repository = repositoryFactory(dbContext, unitOfWorkAsync);
            Repositories[typeof(T)] = repository;

            return repository;
        }
    }
}
