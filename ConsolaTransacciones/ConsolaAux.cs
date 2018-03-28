using System;
using DBTransacciones;

namespace ConsolaTransacciones
{
    internal static class ConsolaAux
    {
        public static void Run(TransaccionesContext dbTx, string[] args)
        {
            // Inicio
            Console.WriteLine();
            Console.WriteLine("Bienvenido a MoneyManager-Transacciones!");
            Console.WriteLine("-----------------------------------------------------------------------");

            // Instrucciones
            // Funcion (N-Nueva B-Buscar D-DesplegarLista M-Modificar X-Eliminar)
            Console.WriteLine("Ingrese comando: 'Entidad' 'Funcion'");
            Console.WriteLine("Entidades: Moneda");
            Console.WriteLine("Funciones: D-Desplegar L-Listar M-Modificar N-Nueva X-Eliminar");
            Console.WriteLine("Ejemplo: Moneda N (para adicionar una moneda nueva)");
            Console.WriteLine("'F': Para salir");
            Console.WriteLine("-----------------------------------------------------------------------");

            // Loop de proceso
            while (true)
            {
                string comando = RecogeComando();
                if (string.IsNullOrEmpty(comando) || 
                        comando.Trim().ToUpper() == "F" ||
                        comando.Trim().ToUpper() == "Q" || 
                        comando.Trim().ToUpper() == "*")
                    break;
                RuteaComando(dbTx, comando);
            }

            // Cierre
            Console.WriteLine("Gracias por utilizar Money Manager - Transacciones!");
        }

        private static string RecogeComando()
        {
            string comando = string.Empty;
            Console.Write(">");
            comando = Console.ReadLine();
            return comando;
        }

        private static void RuteaComando(TransaccionesContext dbTx, string comando)
        {
            string[] argsComando = comando.Split(' ');
            if (argsComando.Length >= 2 && argsComando[0].Length >= 3)
            {
                string entidad = argsComando[0]; 
                if (entidad.Substring(0, 3).Trim().ToUpper() == "MON")
                    MonedaAux.TrabajarCon(dbTx, argsComando);
                else
                {
                    Console.WriteLine($"Entidad '{entidad}' desconocida!");
                    return;
                }
            }
        }
    }
}
