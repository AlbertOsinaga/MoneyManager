using System.Linq;
using DBTransacciones;
using ModeloTransacciones;

namespace TxService
{
    internal static class TxMoneda
    {
        public static string Invoke(TransaccionesContext dbTx, string[] args)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            string accion = args.Length >= 2 ?args[1].Substring(0, 3).Trim().ToLower() : null;
            string jData = args.Length >= 3 ? args[2] : null;
            switch(accion)
            {
                case "add":
                    return TxMoneda.Add(dbTx, jData);
                case "exi":
                    return TxMoneda.Exist(dbTx, jData);
                default:
                    respuestaTuple.Error = $"TxMoneda. Entidad desconocida: '{args[0]}'!";
                    return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }

        public static string Add(TransaccionesContext dbTx, string jData)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            if ( string.IsNullOrEmpty(jData) )
            {
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda add (moneda_data)'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }


            if (string.IsNullOrEmpty(jData))
            {
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda exist (símbolo)'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            try
            {
                Moneda monedaIn = new Moneda(jData);
                dbTx.Add(monedaIn);
                dbTx.SaveChanges();
                Moneda monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);
                if (monedaOut != null)
                {
                    respuestaTuple.Codigo = 200;
                    respuestaTuple.Dato = monedaOut.ToString();
                }
                else
                {
                    respuestaTuple.Codigo = 0;
                    respuestaTuple.Error = $"Moneda: '{monedaIn.Simbolo}' no pudo recuperarse después de ser salvada!";
                }
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = 100;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }

        public static string Exist(TransaccionesContext dbTx, string jData)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            if ( string.IsNullOrEmpty(jData) )
            {
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda exist (símbolo)'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            try
            {
                Moneda moneda = new Moneda(jData);
                Moneda monedax = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == moneda.Simbolo);
                respuestaTuple.Codigo = 200;
                respuestaTuple.Dato = monedax != null ? "SI" : "NO";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = 100;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }
    }
}
