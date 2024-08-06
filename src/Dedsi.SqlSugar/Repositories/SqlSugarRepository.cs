using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;
using SqlSugar;

namespace Dedsi.SqlSugar.Repositories;

public class SqlSugarRepository<TEntity>(ISqlSugarClient client) : SqlSugarReadOnlyRepository<TEntity>(client), IRepository<TEntity> where TEntity : class, new()
{
    /// <inheritdoc />
    public async Task<bool> InsertAsync(List<TEntity> entities)
    {
        if (entities.Count > 10000)
        {
            await client.Fastest<TEntity>().BulkCopyAsync(entities);
        }
        else
        {
            await client.Insertable(entities).ExecuteCommandAsync();
        }

        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> InsertAsync(TEntity entity)
    {
        await client.Insertable(entity).ExecuteCommandAsync();
        return true;
    }

    /// <inheritdoc />
    public Task<int> InsertReturnIdentityAsync(TEntity entity)
    {
        return client.Insertable(entity).ExecuteReturnIdentityAsync();
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        await client.Updateable(entity).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateColumnsAsync(TEntity entity,Expression<Func<TEntity, object>> columns)
    {
        await client.Updateable(entity).UpdateColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> IgnoreColumnsAsync(TEntity entity,Expression<Func<TEntity, object>> columns)
    {
        await client.Updateable(entity).IgnoreColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateAsync(List<TEntity> entities)
    {
        await client.Updateable(entities).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateColumnsAsync(List<TEntity> entities,Expression<Func<TEntity, object>> columns)
    {
        await client.Updateable(entities).UpdateColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> IgnoreColumnsAsync(List<TEntity> entities,Expression<Func<TEntity, object>> columns)
    {
        await client.Updateable(entities).IgnoreColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        await client.Deleteable(entity).ExecuteCommandAsync();
              return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        await client.Deleteable<TEntity>().Where(expression).ExecuteCommandAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync<TPrimaryKey>(TPrimaryKey id)
    {
        await client.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteAsync<TPrimaryKey>(IEnumerable<TPrimaryKey> ids)
    {
        await client.Deleteable<TEntity>().In(ids.ToArray()).ExecuteCommandAsync();
        return true;
    }

    /// <inheritdoc />
    public Task<int> ExecuteCommandAsync(string sql, object parameters)
    {
        return client.Ado.ExecuteCommandAsync(sql,parameters);
    }
}