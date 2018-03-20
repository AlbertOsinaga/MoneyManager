using System;
using Xunit;
using DBTransacciones;
using ModeloTransacciones;
using TxService;

namespace TestsTxService
{
    public class TestTxService_Moneda
    {
        [Fact]
        public void Test_Moneda_00_Add()
        {
            // Prepara
            Moneda monedaIn = new Moneda
            {
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };
            string strMonedaIn = monedaIn.ToStringExid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                dbTx.Database.EnsureCreated();
                respuesta = TxFuncion.Invoke(dbTx, $"moneda add {strMonedaIn}");
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
        public void Test_Moneda_01_Exist()
        {
            // Prepara
            Moneda monedaIn = new Moneda
            {
                Simbolo = "BS",
            };
            string strMonedaIn = monedaIn.ToStringExid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda exist {strMonedaIn}");
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

        [Fact]
        public void Test_Moneda_02_Delete()
        {
            // Prepara
            Moneda monedaIn = new Moneda
            {
                Simbolo = "BS",
            };
            string strMonedaIn = monedaIn.ToStringExid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda delete {strMonedaIn}");
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
    }
}
