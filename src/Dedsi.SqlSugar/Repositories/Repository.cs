using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;
using SqlSugar;

namespace Dedsi.SqlSugar.Repositories;

public class Repository<TEntity>(ISqlSugarClient sqlSugarClient) 
    : ReadOnlyRepository<TEntity>(sqlSugarClient),
        IRepository<TEntity> where TEntity : class, new()
{
    /// <inheritdoc />
    public async Task<bool> InsertAsync(List<TEntity> entities)
    {
        if (entities.Count > 10000)
        {
            await sqlSugarClient.Fastest<TEntity>().BulkCopyAsync(entities);
        }
        else
        {
            await sqlSugarClient.Insertable(entities).ExecuteCommandAsync();
        }

        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> InsertAsync(TEntity entity)
    {
        await sqlSugarClient.Insertable(entity).ExecuteCommandAsync();
        return true;
    }

    /// <inheritdoc />
    public Task<int> InsertReturnIdentityAsync(TEntity entity)
    {
        return sqlSugarClient.Insertable(entity).ExecuteReturnIdentityAsync();
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        await sqlSugarClient.Updateable(entity).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateColumnsAsync(TEntity entity,Expression<Func<TEntity, object>> columns)
    {
        await sqlSugarClient.Updateable(entity).UpdateColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> IgnoreColumnsAsync(TEntity entity,Expression<Func<TEntity, object>> columns)
    {
        await sqlSugarClient.Updateable(entity).IgnoreColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateAsync(List<TEntity> entities)
    {
        await sqlSugarClient.Updateable(entities).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> UpdateColumnsAsync(List<TEntity> entities,Expression<Func<TEntity, object>> columns)
    {
        await sqlSugarClient.Updateable(entities).UpdateColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> IgnoreColumnsAsync(List<TEntity> entities,Expression<Func<TEntity, object>> columns)
    {
        await sqlSugarClient.Updateable(entities).IgnoreColumns(columns).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        await sqlSugarClient.Deleteable(entity).ExecuteCommandAsync();
              return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        await sqlSugarClient.Deleteable<TEntity>().Where(expression).ExecuteCommandAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync<TPrimaryKey>(TPrimaryKey id)
    {
        await sqlSugarClient.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteAsync<TPrimaryKey>(IEnumerable<TPrimaryKey> ids)
    {
        await sqlSugarClient.Deleteable<TEntity>().In(ids.ToArray()).ExecuteCommandAsync();
        return true;
    }

    /// <inheritdoc />
    public Task<int> ExecuteCommandAsync(string sql, object parameters)
    {
        return sqlSugarClient.Ado.ExecuteCommandAsync(sql,parameters);
    }
}