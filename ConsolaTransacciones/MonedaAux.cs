using System;
using System.Linq;
using DBTransacciones;
using ModeloTransacciones;
using TxService;

namespace ConsolaTransacciones
{
    internal static class MonedaAux
    {
        private static void Adicionar(TransaccionesContext dbTx)
        {
            var moneda = new Moneda();
            Console.WriteLine("Adición de Moneda");
            Console.Write("Símbolo: ");
            moneda.Simbolo = Console.ReadLine().Trim().ToUpper();

            string comando = $"Moneda Exist {{\"Simbolo\":\"{moneda.Simbolo}\"}}"; // {"Simbolo":"moneda.Simbolo"}
            string respuesta = TxFuncion.Invoke(dbTx, comando);
            if(!RespuestaEsNo(respuesta))
            {
                Console.WriteLine($"Moneda '{moneda.Simbolo}' ya existe!");
                return;
            }

            Console.Write("Nombre: ");
            moneda.Nombre = Console.ReadLine();
            Console.Write("Tipo (B-Base X-Ext): ");
            moneda.Tipo = Console.ReadLine();
            Console.Write("Tasa de Cambio: ");
            decimal tasaCambio = 0M;
            decimal.TryParse(Console.ReadLine(), out tasaCambio);
            moneda.TasaCambio = tasaCambio;
            string monedaStr = moneda.ToStringExid();

            comando = $"moneda adicionar {monedaStr}";
            respuesta = TxFuncion.Invoke(dbTx, comando);
            (int codigo,
             string data,
             string errorDescrip) respuestaT = TxFuncion.RespuestaToTuple(respuesta);
            if(respuestaT.codigo < TxFuncion.Ok)
            {
                Console.WriteLine($"Error al Adicionar Moneda: '{respuestaT.errorDescrip}'");
                return;
            }
            Moneda monedaNueva = new Moneda(respuestaT.data);
            Console.WriteLine($"Moneda '{monedaNueva.Simbolo}' adicionada. MonedaId : {monedaNueva.MonedaId}");
        }

        private static bool RespuestaEsNo(string respuesta)
        {
            (int codigo, string data, string errorDesc) respuestaTuple = TxFuncion.RespuestaToTuple(respuesta);
            return (respuestaTuple.codigo == TxFuncion.Ok && !string.IsNullOrEmpty(respuestaTuple.data)
                        && respuestaTuple.data == "NO");
        }

        private static Moneda Desplegar(TransaccionesContext dbTx, string[] args)
        {
            string simbolo = null;
            if (args.Length <= 2)
            {
                Console.WriteLine("Ingrese Símbolo de moneda");
                Console.Write("Símbolo: ");
                simbolo = Console.ReadLine().Trim().ToUpper();
            }
            else
                simbolo = args[2].Trim().ToUpper();
            var moneda = Desplegar(dbTx, simbolo);
            if (moneda == null)
                Console.WriteLine($"Moneda {simbolo} no existe!!!");
            return moneda;
        }

        private static Moneda Desplegar(TransaccionesContext dbTx, string simbolo)
        {
            var moneda = dbTx.Monedas.FirstOrDefault(m => m.Simbolo == simbolo);
            if (moneda == null)
                return null;

            Console.WriteLine("-------------------------");
            Console.WriteLine("<Moneda>");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"MonedaId: {moneda.MonedaId}");
            Console.WriteLine($"Símbolo: {moneda.Simbolo} (Clave)");
            Console.WriteLine($"Nombre: {moneda.Nombre}");
            Console.WriteLine($"Tipo: {moneda.Tipo}");
            Console.WriteLine($"TasaCambio: {moneda.TasaCambio}");
            Console.WriteLine("-------------------------");

            return moneda;
        }

        private static void Eliminar(TransaccionesContext dbTx, string[] args)
        {
            string simbolo = null;
            if (args.Length <= 2)
            {
                Console.WriteLine("Ingrese Símbolo de moneda por eliminar");
                Console.Write("Símbolo: ");
                simbolo = Console.ReadLine().Trim().ToUpper();
            }
            else
                simbolo = args[2].Trim().ToUpper();

            var moneda = Desplegar(dbTx, simbolo);
            if (moneda == null)
            {
                Console.WriteLine($"Moneda '{simbolo}' no existe!!!");
                return;
            }

            Console.Write("Confirma eliminación de esta Moneda? (S-Si N-No): ");
            string respuesta = Console.ReadLine().Trim().ToUpper();
            if (respuesta[0] == 'S')
            {
                dbTx.Monedas.Remove(moneda);
                dbTx.SaveChanges();
                Console.WriteLine($"Moneda '{moneda.Simbolo}' fue eliminada!");
            }
        }

        private static void Listar(TransaccionesContext dbTx)
        {
            var listaEntidades = dbTx.Monedas.AsEnumerable();

            Console.WriteLine("-------------------------");
            Console.WriteLine("Monedas");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("MonedaId|Símbolo|Nombre|Tipo|Tasa de Cambio");
            Console.WriteLine("---------------------------------------------");
            foreach (var e in listaEntidades)
            {
                Console.Write($"{e.MonedaId}|");
                Console.Write($"{e.Simbolo}|");
                Console.Write($"{e.Nombre}|");
                Console.Write($"{e.Tipo}|");
                Console.WriteLine(e.TasaCambio);
            }
        }

        private static void Modificar(TransaccionesContext dbTx, string[] args)
        {
            string simbolo = null;
            if (args.Length <= 2)
            {
                Console.WriteLine("Ingrese Símbolo de moneda a modificar");
                Console.Write("Símbolo: ");
                simbolo = Console.ReadLine().Trim().ToUpper();
            }
            else
                simbolo = args[2].Trim().ToUpper();

            var moneda = Desplegar(dbTx, simbolo);
            if (moneda == null)
            {
                Console.WriteLine();
                Console.WriteLine($"Moneda '{simbolo}' no existe!!!");
                return;
            }

            string linea = null;
            Console.WriteLine("Modificación de Moneda");
            Console.Write($"Nombre '{moneda.Nombre}': ");
            linea = Console.ReadLine().Trim().ToUpper();
            if (!string.IsNullOrEmpty(linea))
                moneda.Nombre = linea;
            Console.Write($"Tipo (B-Base X-Ext) '{moneda.Tipo}': ");
            linea = Console.ReadLine().Trim().ToUpper();
            if (!string.IsNullOrEmpty(linea))
                moneda.Tipo = linea;

            decimal tasaCambio = 0M;
            Console.Write($"Tasa de Cambio: '{moneda.TasaCambio}': ");
            linea = Console.ReadLine().Trim().ToUpper();
            if (!string.IsNullOrEmpty(linea))
            {
                if (decimal.TryParse(linea, out tasaCambio))
                    moneda.TasaCambio = tasaCambio;
            }

            dbTx.SaveChanges();
        }

        public static void TrabajarCon(TransaccionesContext dbTx, string[] args)
        {
            char operacion = args[1].Trim().ToUpper()[0];
            if (operacion == 'A')
            {
                Adicionar(dbTx);
            }
            else if (operacion == 'D')
            {
                Desplegar(dbTx, args);
            }
            else if (operacion == 'L')
            {
                Listar(dbTx);
            }
            else if (operacion == 'M')
            {
                Modificar(dbTx, args);
            }
            else if (operacion == 'X')
            {
                Eliminar(dbTx, args);
            }
        }
    }
}
