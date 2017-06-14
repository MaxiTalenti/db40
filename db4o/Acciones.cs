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
        /// <returns>Libros con más de un ejemplar</returns>
        public List<Libro> getLibrosMasDeunEjemplar()
        {
            List<Libro> Libros = new List<Libro>();
            foreach (var a in db.Query<Libro>().GroupBy(z => z.ISBN).Select(g => new { g.Key, Count = g.Count() }))
            {
                Libros.Add(db.Query<Libro>().Where(z => z.ISBN == a.Key).SingleOrDefault());
            }
            return Libros;
        }

        /// <summary>
        /// Listar el apellido y nombre de los autores y el titulo de la publicación de aquellos 
        /// autores que han realizado al menos 5 publicaciones en los últimos 5 años.
        /// </summary>
        /// <returns>Autores que han realizado al menos 5 publicaciones en los últimos 5 años</returns>
        public List<AutorPublicacion> getAutoresMasdeCincoPublicaciones5Años()
        {
            List<AutorPublicacion> autores = new List<AutorPublicacion>();
            foreach(var a in db.Query<AutorPublicacion>())
            {
                if (a.Publicaciones.Where(z => z.Año >= DateTime.Now.Year).Count() >= 5)
                    autores.Add(a);
            }
            return autores;
        }

        /*
         * Listar el nombre de los usuarios que han solicitado solo un préstamo en el último año.
         * Lista el nombre de todos los usuarios que han realizado en promedio más de 30 reservas en los últimos dos años.
         * Listar el título de las publicaciones, el año de publicación, y los autores de aquellas cuyo año de publicación sea par. 
         */
    }
}
