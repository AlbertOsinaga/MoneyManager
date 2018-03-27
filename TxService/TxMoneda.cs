using System.Linq;
using Newtonsoft.Json;
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

            string accion = args.Length >= 2 ?args[1].Trim().ToLower() : null;
            string jData = args.Length >= 3 ? args[2] : null;
            switch(accion)
            {
                case "add":
                    return TxMoneda.Add(dbTx, jData);
                case "all":
                    return TxMoneda.All(dbTx);
                case "delete":
                    return TxMoneda.Delete(dbTx, jData);
                case "exist":
                    return TxMoneda.Exist(dbTx, jData);
                case "get":
                    return TxMoneda.Get(dbTx, jData);
                case "update":
                    return TxMoneda.Update(dbTx, jData);
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

            try
            {
                Moneda monedaIn = new Moneda(jData);
                Moneda monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);
                if(monedaOut != null)
                {
                    respuestaTuple.Error = $"TxMoneda.Add. Moneda '{monedaIn.Simbolo}' ya existe'!";
                    return TxFuncion.RespuestaToString(respuestaTuple);
                }
                dbTx.Add(monedaIn);
                dbTx.SaveChanges();
                monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);
                if (monedaOut != null)
                {
                    respuestaTuple.Codigo = TxFuncion.Ok;
                    respuestaTuple.Dato = monedaOut.ToString();
                }
                else
                {
                    respuestaTuple.Codigo = TxFuncion.ServerError;
                    respuestaTuple.Error = $"Moneda: '{monedaIn.Simbolo}' no pudo recuperarse después de ser insertada!";
                }
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = TxFuncion.Excepcion;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }

        public static string All(TransaccionesContext dbTx)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            try
            {
                var monedas = dbTx.Monedas.ToArray();
                string jmonedas = Entity.ToJson(monedas);
                respuestaTuple.Codigo = TxFuncion.Ok;
                respuestaTuple.Dato = jmonedas;
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = TxFuncion.Excepcion;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }

        public static string Delete(TransaccionesContext dbTx, string jData)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            if (string.IsNullOrEmpty(jData))
            {
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda delete ({\"Simbolo\":\"simbolo\"})'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            try
            {
                Moneda monedaIn = new Moneda(jData);
                Moneda monedaOut = null;
                if(monedaIn.MonedaId > 0)
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.MonedaId == monedaIn.MonedaId);
                else
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);

                if (monedaOut == null)
                {
                    respuestaTuple.Error = $"TxMoneda.Delete. Moneda '{monedaIn.Simbolo}' no existe'!";
                    return TxFuncion.RespuestaToString(respuestaTuple);
                }
                dbTx.Remove(monedaOut);
                dbTx.SaveChanges();
                monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);
                if (monedaOut == null)
                {
                    respuestaTuple.Codigo = TxFuncion.Ok;
                    respuestaTuple.Dato = $"Moneda '{monedaIn.Simbolo}' borrada.";
                }
                else
                {
                    respuestaTuple.Codigo = TxFuncion.ServerError;
                    respuestaTuple.Dato = $"Moneda '{monedaIn.Simbolo}' no pudo ser borrada!";
                }
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = TxFuncion.Excepcion;
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
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda exist ({\"Simbolo\":\"simbolo\"})'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            try
            {
                Moneda monedaIn = new Moneda(jData);
                Moneda monedaOut = null;
                if (monedaIn.MonedaId > 0)
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.MonedaId == monedaIn.MonedaId);
                else
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);
                respuestaTuple.Codigo = TxFuncion.Ok;
                respuestaTuple.Dato = monedaOut != null ? "SI" : "NO";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = TxFuncion.Excepcion;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }
    
        public static string Get(TransaccionesContext dbTx, string jData)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            if (string.IsNullOrEmpty(jData))
            {
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda get ({\"Simbolo\":\"simbolo\"} ó {\"MonedaId\":\"monedaId\"})'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            try
            {
                Moneda monedaIn = new Moneda(jData);
                Moneda monedaOut = null;
                if(monedaIn.MonedaId > 0)
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.MonedaId == monedaIn.MonedaId);
                else if ( !string.IsNullOrEmpty(monedaIn.Simbolo) )
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);

                if (monedaOut != null)
                {
                    respuestaTuple.Codigo = TxFuncion.Ok;
                    respuestaTuple.Dato = monedaOut != null ? monedaOut.ToString() : string.Empty;
                }
                else
                {
                    respuestaTuple.Codigo = TxFuncion.EntidadNoExiste;
                    respuestaTuple.Dato = string.Empty;
                }
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = TxFuncion.Excepcion;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }

        public static string Update(TransaccionesContext dbTx, string jData)
        {
            (int Codigo, string Dato, string Error) respuestaTuple;
            respuestaTuple.Codigo = 0;
            respuestaTuple.Dato = string.Empty;
            respuestaTuple.Error = string.Empty;

            if ( string.IsNullOrEmpty(jData) )
            {
                respuestaTuple.Error = "TxMoneda. Comando no tiene data: 'moneda update ({\"Simbolo\":\"simbolo\"})'!";
                return TxFuncion.RespuestaToString(respuestaTuple);
            }

            try
            {
                Moneda monedaIn = new Moneda(jData);
                Moneda monedaOut = null;
                if (monedaIn.MonedaId > 0)
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.MonedaId == monedaIn.MonedaId);
                else
                    monedaOut = dbTx.Monedas.SingleOrDefault(x => x.Simbolo == monedaIn.Simbolo);

                monedaOut.Nombre = monedaIn.Nombre;
                monedaOut.Simbolo = monedaIn.Simbolo;
                monedaOut.TasaCambio = monedaIn.TasaCambio;
                monedaOut.Tipo = monedaIn.Tipo;

                if(monedaOut == null)
                {
                    if(monedaIn.MonedaId > 0)
                        respuestaTuple.Error = $"TxMoneda.Update. Moneda '{monedaIn.MonedaId}' no existe'!";
                    else
                        respuestaTuple.Error = $"TxMoneda.Update. Moneda '{monedaIn.Simbolo}' no existe'!";
                    return TxFuncion.RespuestaToString(respuestaTuple);
                }

                dbTx.Monedas.Update(monedaOut);
                dbTx.SaveChanges();

                respuestaTuple.Codigo = TxFuncion.Ok;
                respuestaTuple.Dato = monedaOut.ToString();
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
            catch (System.Exception ex)
            {
                respuestaTuple.Codigo = TxFuncion.Excepcion;
                respuestaTuple.Error = ex.Message + ("|" + ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                return TxFuncion.RespuestaToString(respuestaTuple);
            }
        }
    }
}
