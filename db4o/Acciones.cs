using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4objects.Db4o;

namespace db4o
{
    public class Acciones
    {
        IObjectContainer db;
        public Acciones(IObjectContainer database)
        {
            db = database;
        }
        public void CrearUsuario(Usuario usu)
        {
            db.Store(usu);
            db.Commit();
        }

        /// <summary>
        /// Listar el título de todos los libros de los cuales se tiene más de un ejemplar.
        /// </summary>
        /// <returns>Títulos de libros con más de un ejemplar</returns>
        public List<string> getTituloLibrosMasDeunEjemplar()
        {
            List<string> Titulos = new List<string>();
            foreach (var a in db.Query<Ejemplar>()
                .Where(z => z.Libro != null)
                .GroupBy(z => z.Libro.Titulo)
                .Select(g => new { g.Key, Count = g.Count() })
                .Where(z => z.Count > 1))
            {
                Titulos.Add(db.Query<Libro>().Where(z => z.Titulo == a.Key).SingleOrDefault().Titulo);
            }
            return Titulos;
        }

        /// <summary>
        /// Listar el apellido y nombre de los autores y el titulo de la publicación de aquellos 
        /// autores que han realizado al menos 5 publicaciones en los últimos 5 años.
        /// </summary>
        /// <returns>Autores que han realizado al menos 5 publicaciones en los últimos 5 años</returns>
        public List<AutoresYPublicaciones> getAutoresMasdeCincoPublicaciones5Años()
        {
            List<AutoresYPublicaciones> autores = new List<AutoresYPublicaciones>();

            // Recorre todos los autores
            foreach (var autor in db.Query<AutorPublicacion>())
            {
                foreach (var articulo in db.Query<Articulo>().Where(z => z.Autores.Contains(autor)))
                {
                    if ((articulo.Año - DateTime.Now.Year) <= 5)
                    {
                        AutoresYPublicaciones aut = new AutoresYPublicaciones();
                        aut.Nombre = autor.Nombre;
                        aut.Apellido = autor.Apellido;
                        aut.TituloPublicacion = articulo.Titulo;
                        autores.Add(aut);
                    }
                }

                foreach (var libro in db.Query<Libro>().Where(z => z.Autores.Contains(autor)))
                {
                    if ((libro.Año - DateTime.Now.Year) <= 5)
                    {
                        AutoresYPublicaciones aut = new AutoresYPublicaciones();
                        aut.Nombre = autor.Nombre;
                        aut.Apellido = autor.Apellido;
                        aut.TituloPublicacion = libro.Titulo;
                        autores.Add(aut);
                    }
                }

                if (autores.Where(z => z.Nombre == autor.Nombre && z.Apellido == autor.Apellido).Count() < 5)
                    autores.RemoveAll(z => z.Nombre == autor.Nombre && z.Apellido == autor.Apellido);
            }

            return autores;
        }

        /// <summary>
        /// Listar el nombre de los usuarios que han solicitado solo un préstamo en el último año.
        /// </summary>
        /// <returns>Usuarios que solo solicitaron un préstamos en el último año</returns>
        public List<string> getUsuariosUnPrestamo()
        {
            db.Query<>
        }

        /*
         * Lista el nombre de todos los usuarios que han realizado en promedio más de 30 reservas en los últimos dos años.
         * Listar el título de las publicaciones, el año de publicación, y los autores de aquellas cuyo año de publicación sea par. 
         */
    }

    public class AutoresYPublicaciones
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TituloPublicacion { get; set; }
    }

}
