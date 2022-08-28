using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Tests.Config
{
    public static class TestsExtensions
    {
        public static decimal ApenasNumeros(this string value)
        {
            return Convert.ToDecimal(new string(value.Where(char.IsDigit).ToArray()));
        }
    }
}
