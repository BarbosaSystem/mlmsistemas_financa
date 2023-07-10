using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using app.Contexto;
using app.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace app.Repositorios.Concretos
{
    public class EFRepositorio<T> : IRepositorioBase<T> where T : class
    {
        protected readonly DemoDbContext _context;
        protected DbSet<T> DbSet;
        public EFRepositorio(DemoDbContext context)
        {
            _context = context;
            this.DbSet = _context.Set<T>();
        }
        public void Atualizar(T entity)
        {
            /* _context.Set<T>().Update(entity); */
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void AtualizarVarios(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Excluir(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void ExcluirVarios(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Inserir(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void InserirVarios(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public T PesquisarPorId(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public IList<T> PesquisarTodos()
        {
            return _context.Set<T>().AsNoTracking().ToList<T>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}