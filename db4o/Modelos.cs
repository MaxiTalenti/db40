using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db4o
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public List<Ejemplar> Ejemplares { get; set; }
    }

    public class Ejemplar
    {
        public int Id { get; set; }
        public bool enBiblioteca { get; set; }
    }

    public class Biblioteca
    {
        public List<Publicacion> Publicaciones { get; set; }
    }
    public class Publicacion
    {
        public string Titulo { get; set; }
        public int Año { get; set; }
        public TipoPublicaciones Tipo { get; set; }
    }
    public class Articulo
    {
        public List<AutorPublicacion> Autores { get; set; }
    }
    public class Revista
    {
        public int ISSN { get; set; }

        public List<Articulo> Articulos { get; set; }
    }
    public class Libro
    {
        public int ISBN { get; set; }
        public List<AutorPublicacion> Autores { get; set; }
    }
    public class AutorPublicacion
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Publicacion> Publicaciones { get; set; }
    }
    public enum TipoPublicaciones
    {
        Articulos = 1,
        Revistas = 2,
        Libros = 3
    }
}
