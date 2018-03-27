using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ModeloTransacciones
{
    public abstract class Entity
    {
        public abstract void FromJson(string jentity);              // Fields en jentity se asignan, los otros valores default
        public abstract void ParseJson(string jentity);             // Fields en jentity se asignan, los otros se ignoran  
        public abstract string ToJson(string jfields = null);       // Con Id , Include fields (null van todos con Id)
        public abstract string ToJsonNoid(string jfields = null);   // No Id , Include fields (null van todos No Id) 
        public abstract string ToJsonX(string jfields = null);      // Con Id , Exclude fields (null van todos con Id)
        public abstract string ToJsonXnoid(string jfields = null);  // No Id , Exclude fields (null van todos No Id)

        public static string[] JsonToArray(string jentities)
        {
            List<string> strEntities = new List<string>();
            StringReader sr = new StringReader(jentities);
            JsonTextReader jReader = new JsonTextReader(sr);
            string jentity = string.Empty;
            int indexStart = -1;
            int indexEnd = -1;
            while(jReader.Read())
            {
                if(jReader.TokenType == JsonToken.StartObject)
                {
                    indexStart = jReader.LinePosition - 1;
                }
                else if (jReader.TokenType == JsonToken.EndObject)
                {
                    indexEnd = jReader.LinePosition;
                    jentity = jentities.Substring(indexStart, indexEnd - indexStart);
                    strEntities.Add(jentity);
                    indexStart = -1;
                    indexEnd = -1;
                    jentity = string.Empty;
                }
            }
            jReader.Close();
            sr.Close();

            return strEntities.ToArray();
        }

        public static string ToJson(Entity[] entidades, string jfields = null, string sufijo = null)
        {
            StringBuilder json = new StringBuilder();
            StringWriter sw = new StringWriter(json);
            JsonTextWriter jw = new JsonTextWriter(sw);
            jw.WriteStartArray();
            for (int i = 0; i < entidades.Length; i++)
            {
                if(sufijo == null)
                    jw.WriteRaw(entidades[i].ToJson(jfields));
                else if(sufijo == "Noid")
                    jw.WriteRaw(entidades[i].ToJsonNoid(jfields));
                else if (sufijo == "X")
                    jw.WriteRaw(entidades[i].ToJsonX(jfields));
                else if (sufijo == "Xnoid")
                    jw.WriteRaw(entidades[i].ToJsonXnoid(jfields));
                else
                    jw.WriteRaw(entidades[i].ToJson(jfields));

                if (i < entidades.Length - 1)
                    jw.WriteRaw(",");
            }
            jw.WriteEndArray();
            jw.Close();
            sw.Close();
            return json.ToString();
        }

        public static string ToJsonNoid(Entity[] entidades, string fields = null)
        {
            return Entity.ToJson(entidades, fields, "Noid");
        }

        public static string ToJsonX(Entity[] entidades, string fields = null)
        {
            return Entity.ToJson(entidades, fields, "X");
        }

        public static string ToJsonXnoid(Entity[] entidades, string fields = null)
        {
            return Entity.ToJson(entidades, fields, "Xnoid");
        }
    }
}
