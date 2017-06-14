using System;
using System.Collections.Generic;
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

            bool Salir = false;
            while (Salir)
            {
                switch (Console.ReadLine())
                {
                    case "1":

                    break;
                    default:
                        Salir = true;
                    break;
                }
            }

            Acciones acciones = new Acciones(handler.getDb());
            acciones.CrearUsuario(new Usuario()
                {
                    Nombre = "Maximiliano"
                }
            );
            foreach (var a in handler.getDb().Query<Usuario>())
            {
                Console.WriteLine(a.Nombre);
            }
            
            // Cerrar la conexión a la base.
            handler.close();
        }
    }
}
