using Xunit;

using ModeloTransacciones;

namespace TestsModeloTransacciones
{
    public class TestEntity_ToJson
    {
        [Fact]
        public void Test1()
        {
            // Prepara
            var entities = new Moneda[] 
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJson(entities);

            // Probar
            Assert.NotNull(json);
        }
    }
}
