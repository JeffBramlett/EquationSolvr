using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Unit.Tests
{
    static class Helpers
    {
        public static string Serialize<T>(T item)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            string json = JsonConvert.SerializeObject(item, settings);

            return json;
        }
    }
}
