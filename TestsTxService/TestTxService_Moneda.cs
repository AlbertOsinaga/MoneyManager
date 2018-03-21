using System;
using Xunit;
using DBTransacciones;
using ModeloTransacciones;
using TxService;

namespace TestsTxService
{
    public class TestTxService_Moneda
    {
        // Constructor
        public TestTxService_Moneda()
        {
            using (var dbTx = new TransaccionesContext())
            {
                dbTx.Database.EnsureCreated();
            }
        }

        [Fact]
        public void Test_Moneda_Add()
        {
            // Prepara
            Moneda monedaDel = new Moneda
            {
                Simbolo = "BS"
            };
            string jMonedaDel = monedaDel.ToStringExid();
            using(var dbTx = new TransaccionesContext())
            {
                try { TxFuncion.Invoke(dbTx, $"moneda delete {jMonedaDel}"); } catch (Exception) {}
            }

            Moneda monedaIn = new Moneda
            {
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };
            string jMonedaIn = monedaIn.ToStringExid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda add {jMonedaIn}");
            }

            // Prueba
            string[] partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(200, codigo);
            Assert.True(partes.Length > 1);
            string jdata = partes[1];
            Moneda monedaOut = new Moneda(jdata);
            Assert.True(monedaOut != null);
            Assert.True(monedaOut.MonedaId > 0);
        }

        [Fact]
        public void Test_Moneda_Delete()
        {
            // Prepara
            Moneda monedaAdd = new Moneda
            {
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };
            string strMonedaAdd = monedaAdd.ToStringExid();
            using (var dbTx = new TransaccionesContext())
            {
                try { TxFuncion.Invoke(dbTx, $"moneda add {monedaAdd}"); }
                catch (Exception) { }
            }

            Moneda monedaIn = new Moneda
            {
                Simbolo = "BS",
            };
            string jMonedaIn = monedaIn.ToStringExid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda delete {jMonedaIn}");
            }

            // Prueba
            string[] partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(200, codigo);
            Assert.True(partes.Length > 1);
            string data = partes[1];
            Assert.Equal("Moneda 'BS' borrada.", data);
        }

        [Fact]
        public void Test_Moneda_Exist()
        {
            // Prepara
            Moneda monedaAdd = new Moneda
            {
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };
            string strMonedaAdd = monedaAdd.ToStringExid();
            using (var dbTx = new TransaccionesContext())
            {
                try { TxFuncion.Invoke(dbTx, $"moneda add {monedaAdd}"); }
                catch (Exception) {}
            }

            Moneda monedaIn = new Moneda
            { 
                Simbolo = "BS"
            };
            string jMonedaIn = monedaIn.ToStringExid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda exist {jMonedaIn}");
            }

            // Prueba
            string[] partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(200, codigo);
            Assert.True(partes.Length > 1);
            string data = partes[1];
            Assert.Equal("SI", data);
        }
    }
}
