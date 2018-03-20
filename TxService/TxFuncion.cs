using System.Linq;
using DBTransacciones;
using ModeloTransacciones;

namespace TxService
{
    public static class TxFuncion
    {
        public static int Ok = 200;

        public static string Invoke(TransaccionesContext dbTx, string comando)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            if(string.IsNullOrEmpty(comando))
            {
                respuestaTuple.Error = "TxFuncion. Comando nulo o blanco!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            string[] args = comando.Split(' ');
            if(args.Length < 2)
            {
                respuestaTuple.Error = "TxFuncion.Comando debe tener entidad y acción!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            //
            // Validación de Entidades
            //
            string entidad = args[0].Trim().ToLower();
            switch(entidad)
            {
                case "moneda":
                    return TxMoneda.Invoke(dbTx, args);
                default:
                    respuestaTuple.Error = $"TxFuncion. Entidad desconocida: '{args[0]}'!";
                    return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }

        public static string RespuestaToString((int Codigo, string Dato, string Error) respuesta)
        {
            return (respuesta.Codigo.ToString() + " " +
                    respuesta.Dato + " " +
                   respuesta.Error);
        }
       
        public static (int, string, string) RespuestaToTuple(string respuesta)
        {
            int codigo = -1;
            string dato = null;
            string error = null;

            if (string.IsNullOrEmpty(respuesta))
                return (codigo, dato, error);
            string[] args = respuesta.Split(" ");
            int.TryParse(args[0], out codigo);
            if(codigo < 0)
            {
                dato = args[0];
                if (args.Length > 1)
                    error = args[1];
            }
            else
            {
                if (args.Length > 1)
                    dato = args[1];
                if (args.Length > 2)
                    error = args[2];
            }

            return (codigo, dato, error);
        }
    }
}
