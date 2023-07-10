using System;
using System.Collections.Generic;
using System.Linq;
using app.Contexto;
using app.Models.Financa;
using app.Repositorios.Abstratos;

namespace app.Repositorios.Concretos
{
    public class EFCategoria : EFRepositorio<Categoria>, ICategoria
    {
        public EFCategoria(DemoDbContext context) : base(context)
        {
        }

        public IList<Categoria> PesquisarPorNome(string nome)
        {
            return _context.Categorias.Where(x => x.Nome.Equals(nome)).ToList();
        }
    }
}