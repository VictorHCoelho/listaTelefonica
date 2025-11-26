using ListaTelefonica.Models;
using System.Collections.Generic;

namespace ListaTelefonica.Repositories.Interfaces;

public interface IContatoRepository
{
    IEnumerable<Contato> GetAll();
    Contato? GetById(int id);
    Contato? GetByName(string name);
    Contato Create(Contato todo);
    Contato Update(Contato existing, Contato newContact);
    void Delete(int id);
}
