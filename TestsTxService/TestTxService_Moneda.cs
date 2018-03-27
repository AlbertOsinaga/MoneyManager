using System;
using System.Dynamic;
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

                // Moneda para Add
                Moneda monedaAdd = new Moneda { Simbolo = "BSAdd" };
                string jMonedaAdd = monedaAdd.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda delete {jMonedaAdd}"); } catch (Exception) { }

                // Moneda para Del
                Moneda monedaDel = new Moneda
                {
                    Simbolo = "BSDel",
                    Nombre = "Bolivianos",
                    Tipo = "B",
                    TasaCambio = 1.00M
                };
                string jMonedaDel = monedaDel.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaDel}"); } catch (Exception) { }

                // Moneda para Exist
                Moneda monedaExi = new Moneda
                {
                    Simbolo = "BSExi",
                    Nombre = "Bolivianos",
                    Tipo = "B",
                    TasaCambio = 1.00M
                };
                string jMonedaExi = monedaExi.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaExi}"); } catch (Exception) { }

                // Moneda para Get
                Moneda monedaGet = new Moneda { Simbolo = "BSGet" };
                string jMonedaGet = monedaGet.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaGet}"); } catch (Exception) { }

                // Moneda para Update
                Moneda monedaUpd = new Moneda
                {
                    Simbolo = "BSUpd",
                    Nombre = "Moneda Boliviana",
                    Tipo = "X",
                    TasaCambio = 6.9580M
                };
                string jMonedaUpd = monedaUpd.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaUpd}"); } catch (Exception) { }

                // Moneda para All
                Moneda monedaAll = new Moneda
                {
                    Simbolo = "BSAll1",
                    Nombre = "Moneda Alienigena",
                    Tipo = "X",
                    TasaCambio = 6.9580M
                };
                string jMonedaAll = monedaAll.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaAll}"); } catch (Exception) { }
                monedaAll = new Moneda
                {
                    Simbolo = "BSAll2",
                    Nombre = "Moneda Alienigena",
                    Tipo = "X",
                    TasaCambio = 6.9580M
                };
                jMonedaAll = monedaAll.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaAll}"); } catch (Exception) { }
                monedaAll = new Moneda
                {
                    Simbolo = "BSAll3",
                    Nombre = "Moneda Alienigena",
                    Tipo = "X",
                    TasaCambio = 6.9580M
                };
                jMonedaAll = monedaAll.ToJsonNoid();
                try { TxFuncion.Invoke(dbTx, $"moneda add {jMonedaAll}"); } catch (Exception) { }
            }
        }

        [Fact]
        public void Test_Moneda_Add()
        {
            // Prepara
            Moneda monedaIn = new Moneda
            {
                Simbolo = "BSAdd",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };
            string jMonedaIn = monedaIn.ToJsonNoid();

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
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            string jdata = partes[1];
            Moneda monedaOut = new Moneda(jdata);
            Assert.True(monedaOut != null);
            Assert.True(monedaOut.MonedaId > 0);
            Assert.Equal("BSAdd", monedaOut.Simbolo);
        }

        [Fact]
        public void Test_Moneda_All()
        {
            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda all");
            }

            // Prueba
            string[] partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            string data = partes[1];
            Moneda[] monedas = Moneda.FromJsonArray(data);
            Assert.NotNull(monedas);
            Assert.True(monedas.Length >= 3);
        }
 
        [Fact]
        public void Test_Moneda_Delete()
        {
            // Prepara
            Moneda monedaIn = new Moneda { Simbolo = "BSDel" };
            string jMonedaIn = monedaIn.ToJsonNoid();

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
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            string data = partes[1];
            Assert.Equal("Moneda 'BSDel' borrada.", data);
        }

        [Fact]
        public void Test_Moneda_Exist()
        {
            // Prepara
            Moneda monedaIn = new Moneda { Simbolo = "BSExi" };
            string jMonedaIn = monedaIn.ToJsonNoid();

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
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            string data = partes[1];
            Assert.Equal("SI", data);
        }

        [Fact]
        public void Test_Moneda_Get()
        {
            // Prepara
            Moneda monedaIn = new Moneda { Simbolo = "BSGet" };
            string jMonedaIn = monedaIn.ToJsonNoid();

            // Ejecuta
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda get {jMonedaIn}");
            }

            // Prueba
            string[] partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            string data = partes[1];
            Moneda monedaGet = new Moneda(data.ToString());
            Assert.NotNull(monedaGet);

            // Prepara
            monedaIn = new Moneda { MonedaId = monedaGet.MonedaId };
            jMonedaIn = monedaIn.ToString();

            // ejecuta
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda get {jMonedaIn}");
            }

            // Prueba
            partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out codigo);
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            data = partes[1];
            Moneda monedaGetId = new Moneda(data);
            Assert.NotNull(monedaGetId);
            Assert.Equal(monedaGet.ToString(), monedaGetId.ToString());
        }
 
        [Fact]
        public void Test_Moneda_Update()
        {
            // Prepara
            Moneda monedaIn = new Moneda { Simbolo = "BSUpd" };
            string jMonedaIn = monedaIn.ToJsonNoid();
            string respuesta = null;
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda get {jMonedaIn}");
            }
            string[] partes = respuesta.Split(TxFuncion.RespuestaSep);
            string jMonedaUpd = partes[1];
            Moneda monedaUpd = new Moneda(jMonedaUpd);
            monedaUpd.Nombre = "Dolares Americanos";
            monedaUpd.TasaCambio = 2.3456M;

            // Ejecuta
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                respuesta = TxFuncion.Invoke(dbTx, $"moneda update {jMonedaUpd}");
            }

            // Prueba
            partes = respuesta.Split(TxFuncion.RespuestaSep);
            Assert.True(partes.Length > 0);
            int.TryParse(partes[0], out int codigo);
            Assert.Equal(TxFuncion.Ok, codigo);
            Assert.True(partes.Length > 1);
            string jMonedaResp = partes[1];
            Assert.Equal(jMonedaUpd, jMonedaResp);
        }
    }
}
