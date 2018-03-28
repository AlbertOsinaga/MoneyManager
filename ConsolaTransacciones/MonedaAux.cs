using System;
using System.Linq;
using DBTransacciones;
using ModeloTransacciones;
using TxService;

namespace ConsolaTransacciones
{
    internal static class MonedaAux
    {
        private static void DesplegarEntidad(TransaccionesContext dbTx, string[] args)
        {
            // Consigue Simbolo
            string simbolo = ReadSimbolo(dbTx, "Buscar Moneda", nuevo: false);
            if (simbolo == null)
                return;

            // Busca y Verifica existencia de Moneda
            Moneda moneda = BuscaEntidad(dbTx, simbolo);
            if(moneda == null)
                return;

            // Despliega Moneda
            DespliegaEntidad(moneda);
        }

        private static void ListarEntidades(TransaccionesContext dbTx)
        {
            // Invoca Servicio
            (int codigo, string data, string error) respuesta = TxFuncion.Invoque(dbTx, "moneda all");

            // Verifica servicio Ok
            if (respuesta.codigo < TxFuncion.Ok)
            {
                Console.WriteLine($"Error al invocar: 'moneda all'. {respuesta.codigo} - {respuesta.error}!");
                return;
            }

            // Verifica existencia de dato
            if (string.IsNullOrEmpty(respuesta.data))
            {
                Console.WriteLine($"No hay monedas para listar!!!");
                return;
            }

            // Despliega lista de Monedas
            Moneda[] monedas = Moneda.FromJsonArray(respuesta.data);
            DespliegaEntidades(monedas);
        }

        private static void EliminarEntidad(TransaccionesContext dbTx, string[] args)
        {
            // Consigue Simbolo
            string simbolo = ReadSimbolo(dbTx, "Buscar Moneda", nuevo: false);
            if (simbolo == null)
                return;

            // Busca Moneda
            Moneda moneda = BuscaEntidad(dbTx, simbolo);
            if (moneda == null)
                return;

            // Despliega Moneda a eliminar
            DespliegaEntidad(moneda);

            Console.Write("Confirma eliminación de esta Moneda? (S-Si N-No): ");
            string confirma = Console.ReadLine().Trim().ToUpper();
            if (confirma[0] != 'S')
                return;

            // Invoca servicio
            string jmoneda = moneda.ToJsonNoid();
            (int codigo, string data, string error) respuesta = TxFuncion.Invoque(dbTx, $"moneda delete {jmoneda}");

            // Verifica Servicio Ok
            if (respuesta.codigo < TxFuncion.Ok)
            {
                Console.WriteLine($"Error la invocar: 'moneda delete {jmoneda}'. {respuesta.codigo} - {respuesta.error}");
                return;
            }

            Console.WriteLine($"Moneda '{simbolo}' fue eliminada.");
        }

        private static void ModificarEntidad(TransaccionesContext dbTx, string[] args)
        {
            // Consigue Simbolo
            string simbolo = ReadSimbolo(dbTx, "Buscar Moneda", nuevo: false);
            if (simbolo == null)
                return;

            // Busca Moneda
            Moneda moneda = BuscaEntidad(dbTx, simbolo);
            if (moneda == null)
                return;

            // Despliega Moneda a Modificar
            DespliegaEntidad(moneda);

            // Consigue modificación de moneda
            ReadUpdData(ref moneda);

            // Invoca Servicio
            string jmoneda = moneda.ToJson();
            (int codigo, string data, string error) respuesta = TxFuncion.Invoque(dbTx, $"moneda update {jmoneda}");

            // Verifica Servicio Ok
            if (respuesta.codigo < TxFuncion.Ok)
            {
                Console.WriteLine($"Error la invocar: 'moneda update {jmoneda}'. {respuesta.codigo} - {respuesta.error}");
                return;
            }

            Console.WriteLine($"Moneda '{simbolo}' fue modificada.");
        }

        private static void NuevaEntidad(TransaccionesContext dbTx)
        {
            // Consigue Simbolo
            string simbolo = ReadSimbolo(dbTx, "Nueva Moneda", nuevo: true);
            if (simbolo == null)
                return;
            Moneda moneda = new Moneda();
            moneda.Simbolo = simbolo;

            // Consigue Data (Nombre, Tipo, TasaCambio)
            ReadNewData(ref moneda);

            // Invoca servicio "moneda add"
            string jmoneda = moneda.ToJsonNoid();
            (int codigo, string data, string error) respuesta = TxFuncion.Invoque(dbTx, $"moneda add {jmoneda}");

            // Valida respuesta
            if (respuesta.codigo < TxFuncion.Ok)
            {
                Console.WriteLine($"Error al invocar: 'moneda add {jmoneda}'. {respuesta.error} - {respuesta.error}");
                return;
            }

            // Despliega respuesta
            Moneda monedaNueva = new Moneda(respuesta.data);
            Console.WriteLine($"Moneda '{monedaNueva.Simbolo}' adicionada. MonedaId : {monedaNueva.MonedaId}");
        }

        public static void TrabajarCon(TransaccionesContext dbTx, string[] args)
        {
            char operacion = args[1].Trim().ToUpper()[0];
            if (operacion == 'D')
            {
                DesplegarEntidad(dbTx, args);
            }
            else if (operacion == 'L')
            {
                ListarEntidades(dbTx);
            }
            else if (operacion == 'M')
            {
                ModificarEntidad(dbTx, args);
            }
            else if (operacion == 'N')
            {
                NuevaEntidad(dbTx);
            }
            else if (operacion == 'X')
            {
                EliminarEntidad(dbTx, args);
            }
        }

        //
        // Metodos auxiliares
        //

        private static Moneda BuscaEntidad(TransaccionesContext dbTx, string simbolo)
        {
            // Invoca servicio
            Moneda moneda = new Moneda();
            moneda.Simbolo = simbolo;
            string jmoneda = moneda.ToJsonNoid();
            (int codigo, string data, string error) respuesta = TxFuncion.Invoque(dbTx, $"moneda get {jmoneda}");

            // Verifica Servicio Ok
            if (respuesta.codigo < TxFuncion.Ok)
            {
                Console.WriteLine($"Error al invocar: 'moneda get {jmoneda}'. {respuesta.codigo} - {respuesta.error}");
                return null;
            }

            // Verifica existencia de dato
            if (string.IsNullOrEmpty(respuesta.data))
            {
                Console.WriteLine($"Moneda {simbolo} no existe!!");
                return null;
            }

            Moneda monedaOut = new Moneda(respuesta.data);
            if (monedaOut.MonedaId == 0 || string.IsNullOrEmpty(monedaOut.Simbolo))
            {
                Console.WriteLine($"Moneda {simbolo} no existe!");
                return null;
            }

            return monedaOut;
        }

        private static void DespliegaEntidad(Moneda moneda)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("<Moneda>");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"MonedaId: {moneda.MonedaId}");
            Console.WriteLine($"Símbolo: {moneda.Simbolo} (Clave)");
            Console.WriteLine($"Nombre: {moneda.Nombre}");
            Console.WriteLine($"Tipo: {moneda.Tipo}");
            Console.WriteLine($"TasaCambio: {moneda.TasaCambio}");
            Console.WriteLine("-------------------------");
        }

        private static void DespliegaEntidades(Moneda[] monedas)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Monedas");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("MonedaId|Símbolo|Nombre|Tipo|Tasa de Cambio");
            Console.WriteLine("---------------------------------------------");
            if(monedas.Length == 0)
            {
                Console.WriteLine("No hay monedas para desplegar");
                return;
            }

            foreach (var m in monedas)
            {
                Console.Write($"{m.MonedaId}|");
                Console.Write($"{m.Simbolo}|");
                Console.Write($"{m.Nombre}|");
                Console.Write($"{m.Tipo}|");
                Console.WriteLine(m.TasaCambio);
            }
        }

        private static void ReadNewData(ref Moneda moneda)
        {
            Console.Write("Nombre: ");
            moneda.Nombre = Console.ReadLine();
            Console.Write("Tipo (B-Base X-Ext): ");
            moneda.Tipo = Console.ReadLine().Trim().ToUpper().Substring(0, 1);
            Console.Write("Tasa de Cambio: ");
            decimal tasaCambio = 0M;
            decimal.TryParse(Console.ReadLine(), out tasaCambio);
            moneda.TasaCambio = tasaCambio;
        }

        private static string ReadSimbolo(TransaccionesContext dbTx, string title, bool nuevo = true)
        {
            Console.WriteLine(title);
            Console.Write("Símbolo: ");
            string simbolo = Console.ReadLine().Trim().ToUpper();

            string comando = $"moneda exist {{\"Simbolo\":\"{simbolo}\"}}";
            (int codigo, string data, string error) respuesta = TxFuncion.Invoque(dbTx, comando);
            string existe = respuesta.data; // si | no
            if (respuesta.codigo < TxFuncion.Ok || string.IsNullOrEmpty(existe))
            {
                Console.WriteLine($"Error de Servicio: 'moneda exist {simbolo}'. {respuesta.codigo} - {respuesta.error}!");
                return null;
            }
            if (nuevo)
            {
                if (existe.Trim().ToLower() == "s")
                {
                    Console.WriteLine($"Moneda '{simbolo}' ya existe!");
                    return null;
                }
            }
            else
            {
                if (existe.Trim().ToLower() == "n")
                {
                    Console.WriteLine($"Moneda '{simbolo}' no existe!");
                    return null;
                }
            }

            return simbolo;
        }

        private static void ReadUpdData(ref Moneda moneda)
        {
            Moneda monedaOut = new Moneda();

            string linea = null;
            Console.WriteLine("Modificación de Moneda");

            Console.Write($"Símbolo '{moneda.Simbolo}': ");
            linea = Console.ReadLine().Trim().ToUpper();
            if (!string.IsNullOrEmpty(linea))
                moneda.Simbolo = linea;

            Console.Write($"Nombre '{moneda.Nombre}': ");
            linea = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(linea))
                moneda.Nombre = linea;

            Console.Write($"Tipo (B-Base X-Ext) '{moneda.Tipo}': ");
            linea = Console.ReadLine().Trim().ToUpper();
            if (!string.IsNullOrEmpty(linea))
                moneda.Tipo = linea;

            decimal tasaCambio = 0M;
            Console.Write($"Tasa de Cambio: '{moneda.TasaCambio}': ");
            linea = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(linea))
            {
                if (decimal.TryParse(linea, out tasaCambio))
                    moneda.TasaCambio = tasaCambio;
            }
        }
    }
}
