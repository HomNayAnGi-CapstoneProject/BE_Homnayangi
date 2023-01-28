using System;
using System.Collections.Generic;
using System.Linq;

namespace BE_Homnayangi.Modules.Utils
{
    public static class StringUtils
    {
        public static string CompressContents(ICollection<string> listContents)
        {
            try
            {
                var result = "";
                listContents.ToList().ForEach(c =>  result += ";"+c);
                return result;
            }
            catch
            {
                throw new Exception("Compress contents fail");
            }
        }
        public static ICollection<string> ExtractContents(string listContents)
        {
            try
            {
                var result = listContents.Split(";").ToList();
                return result;
            }
            catch
            {
                throw new Exception("Extract contents fail");
            }
        }
    }
}

