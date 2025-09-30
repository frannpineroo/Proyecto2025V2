<<<<<<< HEAD
﻿using Proyecto2025.BD.Datos;
using Proyecto2025.Shared.DTO;
=======
﻿
using Proyecto2025.BD.Datos;
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
<<<<<<< HEAD
        Task<int> Insert(E entidad);
        Task<List<E>> SelectById();
        Task<bool> existe(int id);
        Task<bool> Update(int id, E entidad);
        Task<bool> Delete(long id);
        Task<List<ListaChatDTO>> SelectListaChat();
    }

=======
        Task<long> Insert(E entidad);
        Task<List<E>> Select();
        Task<E?> SelectById(long id);
        //Task Update(E entidad);
        Task<bool> Update(long id, E entidad);
    }
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
}