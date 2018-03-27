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
            string jsonstr = "[{\"MonedaId\":1,\"Simbolo\":\"UNO\",\"Nombre\":\"UNO UNO\",\"Tipo\":\"B\",\"TasaCambio\":1.25}," + 
                "{\"MonedaId\":2,\"Simbolo\":\"DOS\",\"Nombre\":\"DOS DOS\",\"Tipo\":\"X\",\"TasaCambio\":1.2534}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test2()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJsonNoid(entities);

            // Probar
            string jsonstr = "[{\"Simbolo\":\"UNO\",\"Nombre\":\"UNO UNO\",\"Tipo\":\"B\",\"TasaCambio\":1.25}," +
                "{\"Simbolo\":\"DOS\",\"Nombre\":\"DOS DOS\",\"Tipo\":\"X\",\"TasaCambio\":1.2534}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test3()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJsonNoid(entities, "[\"Simbolo\",\"Tipo\"]");

            // Probar
            string jsonstr = "[{\"Simbolo\":\"UNO\",\"Tipo\":\"B\"}," +
                                "{\"Simbolo\":\"DOS\",\"Tipo\":\"X\"}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test4()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJson(entities, "[\"Simbolo\",\"Tipo\"]");

            // Probar
            string jsonstr = "[{\"MonedaId\":1,\"Simbolo\":\"UNO\",\"Tipo\":\"B\"}," +
                                "{\"MonedaId\":2,\"Simbolo\":\"DOS\",\"Tipo\":\"X\"}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test5()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJsonX(entities);

            // Probar
            string jsonstr = "[{\"MonedaId\":1,\"Simbolo\":\"UNO\",\"Nombre\":\"UNO UNO\",\"Tipo\":\"B\",\"TasaCambio\":1.25}," +
                "{\"MonedaId\":2,\"Simbolo\":\"DOS\",\"Nombre\":\"DOS DOS\",\"Tipo\":\"X\",\"TasaCambio\":1.2534}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test6()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJsonXnoid(entities);

            // Probar
            string jsonstr = "[{\"Simbolo\":\"UNO\",\"Nombre\":\"UNO UNO\",\"Tipo\":\"B\",\"TasaCambio\":1.25}," +
                "{\"Simbolo\":\"DOS\",\"Nombre\":\"DOS DOS\",\"Tipo\":\"X\",\"TasaCambio\":1.2534}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test7()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJsonXnoid(entities, "[\"Simbolo\",\"Tipo\"]");

            // Probar
            string jsonstr = "[{\"Nombre\":\"UNO UNO\",\"TasaCambio\":1.25}," +
                                "{\"Nombre\":\"DOS DOS\",\"TasaCambio\":1.2534}]";
            Assert.Equal(jsonstr, json);
        }

        [Fact]
        public void Test8()
        {
            // Prepara
            var entities = new Moneda[]
            {
                new Moneda { MonedaId = 1, Simbolo = "UNO", Nombre = "UNO UNO", Tipo = "B", TasaCambio = 1.25M },
                new Moneda { MonedaId = 2, Simbolo = "DOS", Nombre = "DOS DOS", Tipo = "X", TasaCambio = 1.2534M }
            };

            // Ejecutar
            string json = Entity.ToJsonX(entities, "[\"Simbolo\",\"Tipo\"]");

            // Probar
            string jsonstr = "[{\"MonedaId\":1,\"Nombre\":\"UNO UNO\",\"TasaCambio\":1.25}," +
                "{\"MonedaId\":2,\"Nombre\":\"DOS DOS\",\"TasaCambio\":1.2534}]";
            Assert.Equal(jsonstr, json);
        }
    }
}
