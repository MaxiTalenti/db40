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
            try
            {
                if (db.Query<Usuario>().Where(z => z.Nombre == usu.Nombre).Count() > 0)
                    Console.WriteLine("Ya existe un usuario con ese nombre");
                else
                {
                    db.Store(usu);
                    db.Commit();
                    Console.WriteLine(String.Format("El usuario {0} se creo correctamente", usu.Nombre));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("El usuario {0} no pudo crearse", usu.Nombre);
                Console.WriteLine(e);
            }
        }

        public List<UsuarioModel> ListarUsuarios()
        {
            List<UsuarioModel> lista = new List<UsuarioModel>();
            foreach (var a in db.Query<Usuario>())
            {
                lista.Add(new UsuarioModel()
                {
                    Id = db.Ext().GetID(a),
                    Nombre = a.Nombre
                });
            }
            return lista;
        }

        public void CrearAutor(AutorPublicacion autor)
        {
            try
            {
                if (db.Query<AutorPublicacion>().Where(z => z.Nombre == autor.Nombre)
                    .Where(z => z.Apellido == autor.Apellido)
                    .Count() > 0)
                    Console.WriteLine("El autor {0} {1} ya existe");
                else
                {
                    db.Store(autor);
                    db.Commit();
                    Console.WriteLine(String.Format("El autor {0} {1} se creo correctamente",
                        autor.Nombre, autor.Apellido));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("El autor {0} {1} no pudo crearse", autor.Nombre, autor.Apellido);
                Console.WriteLine(e);
            }
        }

        public List<AutorPublicacionModel> ListarAutores()
        {
            List<AutorPublicacionModel> lista = new List<AutorPublicacionModel>();
            foreach (var a in db.Query<AutorPublicacion>())
            {
                lista.Add(new AutorPublicacionModel()
                {
                    Id = db.Ext().GetID(a),
                    Nombre = a.Nombre,
                    Apellido = a.Apellido
                });
            }
            return lista;
        }

        public AutorPublicacion BuscarAutor(int id)
        {
            return (AutorPublicacion)db.Ext().GetByID(id);
        }

        public void CrearLibro(Libro libro)
        {
            try
            {
                db.Store(libro);
                db.Commit();
                Console.WriteLine(String.Format("Libro: {0}",libro.Titulo));
                Console.WriteLine(String.Format("ISBN: {0}", libro.ISBN.ToString()));
                Console.WriteLine(String.Format("Año: {0}", libro.Año.ToString()));
                foreach (var a in libro.Autores)
                {
                    Console.WriteLine("Autor: {0}, {1}", a.Nombre, a.Apellido);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hubo un error, no pudo crearse el libro");
                Console.WriteLine(e);
            }
        }

        public List<LibrosModel> BuscarLibros()
        {
            List<LibrosModel> libros = new List<LibrosModel>();
            foreach(var a in db.Query<Libro>())
            {
                List<AutorPublicacionModel> autores = new List<AutorPublicacionModel>();
                foreach(var autor in a.Autores)
                {
                    autores.Add(new AutorPublicacionModel()
                    {
                        Id = db.Ext().GetID(autor),
                        Nombre = autor.Nombre,
                        Apellido = autor.Apellido
                    });
                }
                libros.Add(new LibrosModel()
                {
                    Id = db.Ext().GetID(a),
                    Titulo = a.Titulo,
                    Autores = autores,
                    Año = a.Año,
                    ISBN = a.ISBN
                });
            }
            return libros;
        }

        public void CrearArticulo(Articulo articulo)
        {
            try
            {
                db.Store(articulo);
                db.Commit();
                Console.WriteLine(String.Format("Libro: {0}", articulo.Titulo));
                Console.WriteLine(String.Format("Año: {0}", articulo.Año.ToString()));
                foreach (var a in articulo.Autores)
                {
                    Console.WriteLine("Autor: {0}, {1}", a.Nombre, a.Apellido);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hubo un error, no pudo crearse el libro");
                Console.WriteLine(e);
            }
        }

        public List<ArticuloModel> BuscarArticulo()
        {
            List<ArticuloModel> articulos = new List<ArticuloModel>();
            foreach (var a in db.Query<Articulo>())
            {
                List<AutorPublicacionModel> autores = new List<AutorPublicacionModel>();
                foreach (var autor in a.Autores)
                {
                    autores.Add(new AutorPublicacionModel()
                    {
                        Id = db.Ext().GetID(autor),
                        Nombre = autor.Nombre,
                        Apellido = autor.Apellido
                    });
                }
                articulos.Add(new ArticuloModel()
                {
                    Id = db.Ext().GetID(a),
                    Titulo = a.Titulo,
                    Autores = autores,
                    Año = a.Año
                });
            }
            return articulos;
        }

        public Articulo BuscarArticulo(int id)
        {
            return (Articulo)db.Ext().GetByID(id);
        }

        public void CrearRevista(Revista revista)
        {
            try
            {
                db.Store(revista);
                db.Commit();
                Console.WriteLine("Título: {0}", revista.Titulo);
                Console.WriteLine("Año: {0}", revista.Año);
                foreach (var a in revista.Articulos)
                    Console.WriteLine("Título revista: {0}", a.Titulo);
            }
            catch(Exception e)
            {
                Console.WriteLine("Hubo un error, no pudo crearse la revista");
                Console.WriteLine(e);
            }
        }

        public List<RevistaModel> BuscarRevista()
        {
            List<RevistaModel> revistas = new List<RevistaModel>();
            foreach (var a in db.Query<Revista>())
            {
                List<ArticuloModel> articulos = new List<ArticuloModel>();
                foreach (var articulo in a.Articulos)
                {
                    ArticuloModel artm = new ArticuloModel();
                    artm.Id = db.Ext().GetID(articulo);
                    foreach (var b in articulo.Autores)
                    {
                        artm.Autores.Add(new AutorPublicacionModel()
                        {
                            Id = db.Ext().GetID(b),
                            Nombre = b.Nombre,
                            Apellido = b.Apellido
                        });
                    }
                }
                revistas.Add(new RevistaModel()
                {
                    Id = db.Ext().GetID(a),
                    Titulo = a.Titulo,
                    ISSN = a.ISSN,
                    Año = a.Año,
                    Articulos = articulos
                });
            }
            return revistas;
        }

        public void CrearEjemplar(object Publicacion)
        {
            if (Publicacion is Libro || Publicacion is Revista || Publicacion is Articulo)
            {
                Ejemplar ej = new Ejemplar();
                ej.Publicacion = Publicacion;
                ej.enBiblioteca = true;
                db.Store(ej);
                db.Commit();
                Console.WriteLine("El ejemplar ha sido añadido");
            }
            else
                Console.WriteLine("El objeto no pertenece a una publicación");
        }

        /// <summary>
        /// Listar el título de todos los libros de los cuales se tiene más de un ejemplar.
        /// </summary>
        /// <returns>Títulos de libros con más de un ejemplar</returns>
        public List<string> getTituloLibrosMasDeunEjemplar()
        {
            Console.WriteLine("Títulos de libros de más de un ejemplar");
            List<string> Titulos = new List<string>();
            foreach (var a in db.Query<Ejemplar>()
                .Where(z => z.Publicacion is Libro)
                .GroupBy(z => z.Publicacion)
                .Select(g => new { g.Key, Count = g.Count() })
                .Where(z => z.Count > 1))
            {
                Libro libro = new Libro();
                if (a.Key is Libro)
                {
                    libro = (Libro)a.Key;
                    Titulos.Add(libro.Titulo);
                }
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
            return db.Query<Prestamos>().Where(z => z.inicio.Year == DateTime.Now.Year)
                .Select(z => z.Usuario.Nombre)
                .Distinct().ToList();
        }

        // Lista el nombre de todos los usuarios que han realizado en promedio más de 30 reservas en los últimos dos años.
        public List<string> getUsuariosMasDe30Reservas()
        {
            return db.Query<Prestamos>().Where(z => z.inicio >= (DateTime.Now.AddYears(-2)))
                .GroupBy(z => z.Usuario.Nombre)
                .Select(g => new { g.Key, Count = g.Count() })
                .Where(z => z.Count > 30)
                .Select(z => z.Key).ToList();
        }

        // Listar el título de las publicaciones, el año de publicación,
        // y los autores de aquellas cuyo año de publicación sea par. 
        public List<Tuple<Publicacion, List<AutorPublicacion>>> getPublicacionesconAutorAñoPar()
        {
            List<Tuple<Publicacion, List<AutorPublicacion>>> tupla = new List<Tuple<Publicacion, List<AutorPublicacion>>>();
            
            foreach (var a in db.Query<Articulo>().Where(z => z.Año % 2 == 0).Select(x => new { x.Titulo, x.Año, x.Autores }))
            {
                Publicacion pub = new Publicacion()
                {
                    Titulo = a.Titulo,
                    Año = a.Año
                };
                List<AutorPublicacion> autores = new List<AutorPublicacion>();
                tupla.Add(new Tuple<Publicacion, List<AutorPublicacion>>(pub, a.Autores));
            }

            foreach (var a in db.Query<Libro>().Where(z => z.Año % 2 == 0).Select(x => new { x.Titulo, x.Año, x.Autores }))
            {
                Publicacion pub = new Publicacion()
                {
                    Titulo = a.Titulo,
                    Año = a.Año
                };
                List<AutorPublicacion> autores = new List<AutorPublicacion>();
                tupla.Add(new Tuple<Publicacion, List<AutorPublicacion>>(pub, a.Autores));
            }

            return tupla;
        }

         
    }

    public class AutoresYPublicaciones
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TituloPublicacion { get; set; }
    }

    public class UsuarioModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }

    public class AutorPublicacionModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class LibrosModel : Publicacion
    {
        public long Id { get; set; }
        public int ISBN { get; set; }
        public List<AutorPublicacionModel> Autores { get; set; }
    }

    public class ArticuloModel : Publicacion
    {
        public long Id { get; set; }
        public List<AutorPublicacionModel> Autores { get; set; }
    }

    public class RevistaModel : Publicacion
    {
        public long Id { get; set; }
        public int ISSN { get; set; }

        public List<ArticuloModel> Articulos { get; set; }
    }
}
