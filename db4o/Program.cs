using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db4o
{
    class Program
    {
        static void Main(string[] args)
        {
            // use a instance of a class to write
            Program myNewClass = new Program();
            myNewClass.Inicializar();
        }

        public void Inicializar()
        {
            Db4oHandler handler = new Db4oHandler();
            handler.openDatabase(Environment.CurrentDirectory + "//BaseDeDatos");

            Console.WriteLine("Se inicializó la base, seleccione la accción a ejecutar:");
            // Imprimir acciones

            Console.WriteLine("1 -> Crear un usuario");
            Console.WriteLine("2 -> Ver todos los usuarios");
            Console.WriteLine("3 -> Crear un autor");
            Console.WriteLine("4 -> Ver todos los autores");
            Console.WriteLine("5 -> Crear un libro");
            Console.WriteLine("6 -> Ver todos los libros");
            Console.WriteLine("7 -> Crear un artículo");
            Console.WriteLine("8 -> Ver todos los artículos");

            Console.WriteLine("9 -> Crear una revista");
            Console.WriteLine("10 -> Ver todas las revistas");

            Acciones acciones = new Acciones(handler.getDb());
            bool Salir = true;
            while (Salir)
            {
                Console.WriteLine("");
                Console.WriteLine("Seleccione alguna acción a realizar");
                Console.WriteLine("");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Introduzca el nombre del Usuario");
                        acciones.CrearUsuario(new Usuario()
                        {
                            Nombre = Console.ReadLine()
                        });
                        break;
                    case "2":
                        foreach (var a in acciones.ListarUsuarios())
                            ImprimirObjeto(a);
                        break;
                    case "3":
                        Console.WriteLine("Ingrese el nombre del autor");
                        AutorPublicacion autor = new AutorPublicacion();
                        autor.Nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el apellido del autor");
                        autor.Apellido = Console.ReadLine();
                        acciones.CrearAutor(autor);
                        break;
                    case "4":
                        foreach (var a in acciones.ListarAutores())
                            ImprimirObjeto(a);
                        break;
                    case "5":
                        Libro libro = new Libro();
                        Console.WriteLine("Ingrese el ISBN (entero)");
                        libro.ISBN = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el título");
                        libro.Titulo = Console.ReadLine();
                        Console.WriteLine("Ingrese el año (entero)");
                        libro.Año = int.Parse(Console.ReadLine());
                        Console.WriteLine("Escriba el id de al menos un autor (separado por ,)");
                        foreach (var a in acciones.ListarAutores())
                            ImprimirObjeto(a);

                        string autores = Console.ReadLine();
                        List<AutorPublicacion> listas = new List<AutorPublicacion>();
                        foreach (var a in autores.Split(','))
                            listas.Add(acciones.BuscarAutor(Int32.Parse(a)));
                        libro.Autores = listas;

                        acciones.CrearLibro(libro);
                        break;
                    case "6":
                        foreach (var a in acciones.BuscarLibros())
                        {
                            ImprimirObjeto(a);
                            foreach (var b in a.Autores)
                                ImprimirObjeto(b);
                        }
                        break;
                    case "7":
                        Articulo articulo = new Articulo();
                        Console.WriteLine("Escribe el título del artículo");
                        articulo.Titulo = Console.ReadLine();
                        Console.WriteLine("Escribe el año del artículo");
                        articulo.Año = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Escriba el id de al menos un autor (separado por ,)");
                        foreach (var a in acciones.ListarAutores())
                            ImprimirObjeto(a);

                        string autores1 = Console.ReadLine();
                        List<AutorPublicacion> listas1 = new List<AutorPublicacion>();
                        foreach (var a in autores1.Split(','))
                            listas1.Add(acciones.BuscarAutor(Int32.Parse(a)));
                        articulo.Autores = listas1;

                        acciones.CrearArticulo(articulo);
                        break;
                    case "8":
                        foreach (var a in acciones.BuscarArticulo())
                        {
                            ImprimirObjeto(a);
                            foreach (var b in a.Autores)
                                ImprimirObjeto(b);
                        }
                        break;
                    default:
                        Salir = false;
                        break;
                }
            }

            Console.WriteLine("Presione cualquier tecla para cerrar");
            Console.ReadLine();

            // Cerrar la conexión a la base.
            handler.close();
        }

        public void ImprimirObjeto(object obj)
        {
            Console.WriteLine("# Objeto: {0}", obj.GetType().ToString());
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                if (!descriptor.GetValue(obj).GetType().IsGenericType)
                {
                    object value = descriptor.GetValue(obj);
                    Console.WriteLine("{0} -> {1}", name, value);
                }              
            }
        }
    }
}
