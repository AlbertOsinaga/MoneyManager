﻿using System;
using DBTransacciones;

namespace ConsolaTransacciones
{
    internal static class ConsolaAux
    {
        public static void Run(TransaccionesContext dbTx, string[] args)
        {
            // Inicio
            Console.WriteLine();
            Console.WriteLine("Bienvenido a Transacciones!");
            Console.WriteLine();

            // Instrucciones
            // Funcion (L-Lista D-Desplegar A-Adicionar M-Modificar E-Eliminar)
            Console.WriteLine("Ingrese comando (Entidad Funcion)");

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
            Console.WriteLine("Gracias por utilizar Transacciones!");
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
                if (argsComando[0].Substring(0, 3).Trim().ToUpper() == "MON")
                    MonedaAux.TrabajarCon(dbTx, argsComando);
            }
        }
    }
}