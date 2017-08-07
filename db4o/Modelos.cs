using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using System.Threading.Tasks;

namespace db4o
{

    /*
     * Una Biblioteca posee publicaciones que pueden ser Artículos, Revistas o Libros.
     * Una Revista está compuesta de Artículos.
     * Tanto los Libros como Artículos pueden tener uno o varios autores.
     * De cada publicación, la biblioteca puede tener 1 o más ejemplares.
     * Un Usuario de la biblioteca puede pedir en préstamo a lo sumo 3 ejemplares de forma simultánea.
     */

    // Usuarios de la biblioteca
    public class Usuario
    {
        public string Nombre { get; set; }
    }

    public class Prestamos
    {
        public Usuario Usuario { get; set; }
        public Ejemplar Ejemplar { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
    }

    public class Ejemplar
    {
        public Revista Revista { get; set; }
        public Libro Libro { get; set; }
        public Articulo Articulo { get; set; }
        public bool enBiblioteca { get; set; }
    }

    /// <summary>
    /// Una Biblioteca posee publicaciones que pueden ser Artículos, Revistas o Libros.
    /// </summary>
    public class Biblioteca
    {
        public List<Publicacion> Publicaciones { get; set; }
    }
    public class Publicacion
    {
        IObjectContainer db;

        public string Titulo { get; set; }
        public int Año { get; set; }

        public List<Ejemplar> ejemplaresDisponibles()
        {
            return db.Query<Ejemplar>().Where(Z => Z.enBiblioteca).ToList();
        }
    }
    public class Articulo : Publicacion
    {
        public List<AutorPublicacion> Autores { get; set; }
    }

    /// <summary>
    /// Una Revista está compuesta de Artículos.
    /// </summary>
    public class Revista : Publicacion
    {
        public int ISSN { get; set; }

        public List<Articulo> Articulos { get; set; }
    }
    public class Libro : Publicacion
    {
        public int ISBN { get; set; }
        public List<AutorPublicacion> Autores { get; set; }
    }
    public class AutorPublicacion
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
