using MensajeroModel.DAL;
using MensajeroModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroApp
{
    public partial class Program
    {      //1.Crear Menu
        //2. Dos metodos IngresarMensajes y MostrarMensajes
        //3. Al ingresar un mensaje definir el tipo como Aplicacion

        static IMensajesDAL dal = MensajesDALFactory.CreateDal();
        static void IngresarMensaje()
        {
            string nombre, detalle;
            do
            {
                Console.WriteLine("Ingrese nombre:");
                nombre = Console.ReadLine().Trim();

            } while (nombre == string.Empty);

            do
            {
                Console.WriteLine("Ingrese mensaje:");
                detalle = Console.ReadLine().Trim();

            } while (detalle == string.Empty || detalle.Length > 20);

            Mensaje m = new Mensaje()
            {
                Nombre = nombre,
                Detalle = detalle,
                Tipo = "Aplicacion"
            };
            lock (dal)
            {
                dal.Save(m);
            }
        }

        static void MostrarMensajes()
        {
            List<Mensaje> mensajes = dal.GetAll();
            mensajes.ForEach(m =>
            {
                Console.WriteLine("Nombre:{0} Detalle:{1} Tipo:{2}"
                    , m.Nombre, m.Detalle, m.Tipo);
            });
        }

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("1. Ingresar Mensaje");
            Console.WriteLine("2. Mostrar Mensajes");
            Console.WriteLine("0. Salir");
            string opcion = Console.ReadLine().Trim();
            switch (opcion)
            {
                case "1":
                    IngresarMensaje();
                    break;
                case "2":
                    MostrarMensajes();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Oiga yapo");
                    break;
            }
            return continuar;
        }

    }
}
