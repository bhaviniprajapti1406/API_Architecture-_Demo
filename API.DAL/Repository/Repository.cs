using API.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T: class 
    {
        private UnitOfWork _unitOfWork;
        private DbSet<T> entities;

        public Repository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            entities = _unitOfWork.Set<T>();
        }
         
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _unitOfWork.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _unitOfWork.Set<T>().ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            await using var dbContextTransaction = await _unitOfWork.Database.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Set<T>().AddAsync(entity);
                var saveChangesAsync = await _unitOfWork.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                return saveChangesAsync;
            }
            catch
            {
                await dbContextTransaction.RollbackAsync();
                throw;
            }

        }

        public async Task UpdateAsync(T entity)
        {
            await using IDbContextTransaction transaction = await _unitOfWork.Database.BeginTransactionAsync();
            try
            {
                _unitOfWork.Set<T>().Update(entity);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task DeleteAsync(T entity)
        {
            await using IDbContextTransaction transaction = await _unitOfWork.Database.BeginTransactionAsync();
            try
            {
                _unitOfWork.Set<T>().Remove(entity);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
