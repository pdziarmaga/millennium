using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Millennium.Shared.BaseObjects;

namespace Millennium.Application.Repositories
{
    public interface IInMemoryRepository
    {
        Task<T> GetAsync<T>(long id, CancellationToken cancellationToken)
            where T : Entity, IEntity;

        Task SaveAsync<T>(T model, CancellationToken cancellationToken)
            where T : Entity, IEntity;

        Task DeleteAsync<T>(T model, CancellationToken cancellationToken)
            where T : Entity, IEntity;
    }

    public class InMemoryRepository : IInMemoryRepository
    {
        private readonly Dictionary<Type, IList<Entity>> _data = new Dictionary<Type, IList<Entity>>();

        public Task<T> GetAsync<T>(long id, CancellationToken cancellationToken)
            where T : Entity, IEntity
        {
            var thisTypeList = _data[typeof(T)];

            var entity = thisTypeList.Cast<IEntity>().SingleOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity of type {typeof(T)} with id {id} was not found.");
            }

            return Task.FromResult(entity as T);
        }

        public Task SaveAsync<T>(T model, CancellationToken cancellationToken)
            where T : Entity, IEntity
        {
            IList<Entity> list;

            if (_data.TryGetValue(typeof(T), out var objectsList) == false)
            {
                list = new List<Entity>();
                _data.Add(typeof(T), list);
            }
            else
            {
                list = objectsList;
            }

            if (list.Contains(model) == false)
            {
                list.Add(model);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync<T>(T model, CancellationToken cancellationToken)
            where T : Entity, IEntity
        {
            if (_data.TryGetValue(typeof(T), out var list))
            {
                var index = list.IndexOf(model);

                if (index != -1)
                {
                    list.RemoveAt(index);
                }
            }

            return Task.CompletedTask;
        }
    }
}