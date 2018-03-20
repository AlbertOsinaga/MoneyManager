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
        public void Test_Moneda_Add()
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
            string strMonedaOut = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda add {strMonedaOut}");
            }

            // Prueba
            string[] partes = respuesta.Split(' ');
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(200, codigo);
        }
    }
}
