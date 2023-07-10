using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using app.Models.Financa;

namespace app.Repositorios.Base
{
    public interface IRepositorioBase<T> : IDisposable where T : class
    {
        IList<T> PesquisarTodos();
        T PesquisarPorId(Expression<Func<T, bool>> predicate);
        void Inserir(T entity);
        void InserirVarios(List<T> entities);
        void Atualizar(T entity);
        void AtualizarVarios(List<T> entities);
        void Excluir(T entity);
        void ExcluirVarios(List<T> entities);
        void SaveChanges();
    }
}