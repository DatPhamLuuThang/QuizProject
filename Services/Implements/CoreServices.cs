using System.Data.Common;
using DataBase;
using DataBase.Base;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;

namespace Services.Implements;

public class CoreServices : ICoreServices
{
    private readonly QuizDbContext _dbContext;

    public CoreServices(QuizDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Set<TEntity>(bool asNoTracking = true) where TEntity : BaseEntity
    {
        return asNoTracking ? _dbContext.Set<TEntity>().AsNoTracking() : _dbContext.Set<TEntity>();
    }

    public async Task<ResultResponse> AddAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity
    {
        ConfigBaseEntity(data,OperationStage.Create, userId);
       
        await DbSet<TEntity>().AddAsync(data);
        return await SaveChangesAsync();
    }

    public async Task<ResultResponse> AddAsync<TEntity>(IEnumerable<TEntity> data, Guid? userId = null)
        where TEntity : BaseEntity
    {
        var baseEntities = data.ToList();
        
        foreach (var entity in baseEntities)
        {
            ConfigBaseEntity(entity, OperationStage.Create, userId);
        }
        
        await DbSet<TEntity>().AddRangeAsync(baseEntities);
        return await SaveChangesAsync();
    }

    public async Task<ResultResponse> UpdateAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity
    {
        ConfigBaseEntity(data,OperationStage.Update, userId);
        
        DbSet<TEntity>().Update(data);
        return await SaveChangesAsync();
    }

    public async Task<ResultResponse> UpdateAsync<TEntity>(IEnumerable<TEntity> data, Guid? userId = null)
        where TEntity : BaseEntity
    {
        var baseEntities = data.ToList();
        
        foreach (var entity in baseEntities)
        {
            ConfigBaseEntity(entity, OperationStage.Update, userId);
        }
        
        DbSet<TEntity>().UpdateRange(baseEntities);
        return await SaveChangesAsync();
    }

    public async Task<ResultResponse> DeleteAsync<TEntity>(TEntity data, Guid? userId = null) where TEntity : BaseEntity
    {
        ConfigBaseEntity(data, OperationStage.Delete, userId);
        return await UpdateAsync(data);
    }

    public async Task<ResultResponse> DeleteAsync<TEntity>(List<TEntity> data, Guid? userId = null)
        where TEntity : BaseEntity
    {
        foreach (var entity in data)
        {
            ConfigBaseEntity(entity, OperationStage.Delete, userId);
        }

        return await UpdateAsync(data);
    }

    public async Task<ResultResponse> HardDeleteAsync<TEntity>(TEntity data, Guid? userId = null)
        where TEntity : BaseEntity
    {
        DbSet<TEntity>().Remove(data);
        return await SaveChangesAsync();
    }

    public async Task<ResultResponse> HardDeleteAsync<TEntity>(IEnumerable<TEntity> data, Guid? userId = null)
        where TEntity : BaseEntity
    {
        DbSet<TEntity>().RemoveRange(data);
        return await SaveChangesAsync();
    }

    #region Private Functions

    private DbSet<TEntity> DbSet<TEntity>() where TEntity : BaseEntity
    {
        return _dbContext.Set<TEntity>();
    }

    private static void ConfigBaseEntity<TEntity>(TEntity data, OperationStage stage, Guid? userId = null) where TEntity : BaseEntity
    {
        var now = DateTime.UtcNow;
        userId ??= Guid.Empty;
        
        data.ModifiedAt = now;
        data.ModifiedBy = userId.Value;

        if (stage == OperationStage.Update)
        {
            return;
        }
        
        switch (stage)
        {
            case OperationStage.Create:
                data.CreatedAt = now;
                data.CreatedBy = userId.Value;
                break;
            case OperationStage.Delete:
                data.IsDeleted = true;
                break;
        }
    }


    private async Task<ResultResponse> SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            return ResultResponse.Success();
        }
        catch (DbException e)
        {
            return ResultResponse.Error(e.Message);
        }
    }

    #endregion
}

public enum OperationStage
{
    Create,
    Update,
    Delete
}