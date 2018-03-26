using Xunit;
using ModeloTransacciones;

namespace TestsModeloTransacciones
{
    public class TestMoneda_ToJson
    {
        [Fact]
        public void Test1()
        {
            // Prepara
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };

            // Ejecuta
            string jMoneda = moneda.ToJson();

            // Preparar 
            Assert.Equal("{\"MonedaId\":1,\"Simbolo\":\"BS\",\"Nombre\":\"Bolivianos\",\"Tipo\":\"B\",\"TasaCambio\":1.00}", jMoneda);
        }

        [Fact]
        public void Test2()
        {
            // Prepara
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "$US",
                Nombre = "Dólares Americanos",
                Tipo = "X",
                TasaCambio = 6.8519M
            };

            // Ejecuta
            string jMoneda = moneda.ToJsonNoid();

            // Preparar 
            Assert.Equal("{\"Simbolo\":\"$US\",\"Nombre\":\"Dólares Americanos\",\"Tipo\":\"X\",\"TasaCambio\":6.8519}", jMoneda);
        }

        [Fact]
        public void Test3()
        {
            // Prepara
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };

            // Ejecuta
            string jMoneda = moneda.ToJson("[\"Id\",\"Simbolo\",\"Nombre\",\"Tipo\"]");

            // Preparar 
            Assert.Equal("{\"MonedaId\":1,\"Simbolo\":\"BS\",\"Nombre\":\"Bolivianos\",\"Tipo\":\"B\"}", jMoneda);
        }

        [Fact]
        public void Test4()
        {
            // Prepara
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };

            // Ejecuta
            string jMoneda = moneda.ToJsonNoid("[\"Simbolo\",\"TasaCambio\",\"Tipo\"]");

            // Preparar 
            Assert.Equal("{\"Simbolo\":\"BS\",\"TasaCambio\":1.00,\"Tipo\":\"B\"}", jMoneda);
        }

        [Fact]
        public void Test5()
        {
            // Prepara
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "BS",
                Nombre = "Bolivianos",
                Tipo = "B",
                TasaCambio = 1.00M
            };

            // Ejecuta
            string jMoneda = moneda.ToJsonX("[\"Id\",\"TasaCambio\"]");

            // Preparar 
            Assert.Equal("{\"Simbolo\":\"BS\",\"Nombre\":\"Bolivianos\",\"Tipo\":\"B\"}", jMoneda);
        }
    }
}
