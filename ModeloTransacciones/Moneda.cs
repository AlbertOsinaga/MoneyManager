using System;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;

namespace ModeloTransacciones
{
    public partial class Moneda
    {
        public int MonedaId { get; set; }
        public string Simbolo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }   // B-Base X-Ext
        public decimal TasaCambio { get; set; }
	}

    public partial class Moneda
    {
        public Moneda() {}
        public Moneda(string monedaStr) : this()
        {
            FromString(monedaStr);
        }

        public void FromString(string monedaStr)
        {
            Moneda moneda = JsonConvert.DeserializeObject<Moneda>(monedaStr);
            this.MonedaId = moneda.MonedaId;
            this.Simbolo = moneda.Simbolo;
            this.Nombre = moneda.Nombre;
            this.Tipo = moneda.Tipo;
            this.TasaCambio = moneda.TasaCambio;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string ToStringExid()
        {
            return ToStringEx("'id'");
        }

        public string ToString(string fields)
        {
            StringReader sreader = new StringReader(fields);
            JsonTextReader jreader = new JsonTextReader(sreader);

            dynamic dynMoneda = new ExpandoObject();
            while (jreader.Read())
            {
                if (jreader.TokenType == JsonToken.String)
                {
                    string field = jreader.Value as String;
                    switch (field.Trim().ToLower())
                    {
                        case "id":
                        case "monedaid":
                            dynMoneda.MonedaId = MonedaId;
                            break;
                        case "simbolo":
                            dynMoneda.Simbolo = Simbolo;
                            break;
                        case "nombre":
                            dynMoneda.Nombre = Nombre;
                            break;
                        case "tipo":
                            dynMoneda.Tipo = Tipo;
                            break;
                        case "tasacambio":
                            dynMoneda.TasaCambio = TasaCambio;
                            break;
                        default:
                            break;
                    }
                }
            }
            return JsonConvert.SerializeObject(dynMoneda);
        }

        public string ToStringEx(string fields)
        {
            StringReader sreader = new StringReader(fields);
            JsonTextReader jreader = new JsonTextReader(sreader);

            dynamic fieldsMoneda = new ExpandoObject();
            fieldsMoneda.MonedaId = true;
            fieldsMoneda.Simbolo = true;
            fieldsMoneda.Nombre = true;
            fieldsMoneda.Tipo = true;
            fieldsMoneda.TasaCambio = true;
            while (jreader.Read())
            {
                if (jreader.TokenType == JsonToken.String)
                {
                    string field = jreader.Value as String;
                    switch (field.Trim().ToLower())
                    {
                        case "id":
                        case "monedaid":
                            fieldsMoneda.MonedaId = false;
                            break;
                        case "simbolo":
                            fieldsMoneda.Simbolo = false;
                            break;
                        case "nombre":
                            fieldsMoneda.Nombre = false;
                            break;
                        case "tipo":
                            fieldsMoneda.Tipo = false;
                            break;
                        case "tasacambio":
                            fieldsMoneda.TasaCambio = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            dynamic dynMoneda = new ExpandoObject();
            if (fieldsMoneda.MonedaId)
                dynMoneda.MonedaId = MonedaId;
            if (fieldsMoneda.Simbolo)
                dynMoneda.Simbolo = Simbolo;
            if (fieldsMoneda.Nombre)
                dynMoneda.Nombre = Nombre;
            if (fieldsMoneda.Tipo)
                dynMoneda.Tipo = Tipo;
            if (fieldsMoneda.TasaCambio)
                dynMoneda.TasaCambio = TasaCambio;
            
            return JsonConvert.SerializeObject(dynMoneda);
        }
    }
}
