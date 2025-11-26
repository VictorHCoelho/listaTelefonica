using ListaTelefonica.Data;
using ListaTelefonica.Models;
using ListaTelefonica.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ListaTelefonica.Repositories;

public class ContatoRepository : IContatoRepository
{
    public AgendaDbContext _dbContext { get; set; }

    public ContatoRepository(AgendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Contato> GetAll()
    {
        return _dbContext.Contatos;
    }

    public Contato? GetById(int id)
    {
        return _dbContext.Contatos.Find(id);
    }

    public Contato? GetByName(string name)
    {
        return _dbContext.Contatos.FirstOrDefault(c => c.Nome.ToLower() == name.ToLower());
    }

    public Contato Create(Contato todo)
    {
        _dbContext.Contatos.Add(todo);
        _dbContext.SaveChanges();
        return todo;
    }

    public Contato Update(Contato existing, Contato newContact)
    {
        existing.Nome = newContact.Nome;
        existing.Telefone = newContact.Telefone;

        _dbContext.Contatos.Update(existing);
        _dbContext.SaveChanges();

        return existing;
    }

    public void Delete(int id)
    {
        var contato = _dbContext.Contatos.Find(id);

        if (contato != null)
        {
            _dbContext.Contatos.Remove(contato);
            _dbContext.SaveChanges();
        }
    }
}
