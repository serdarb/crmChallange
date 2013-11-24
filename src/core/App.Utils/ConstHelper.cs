using System.Globalization;

namespace App.Utils
{
    public class ConstHelper
    {
        public const string MongoCnnStr = "MongoCnnStr";
        public const string MongoDBName = "MongoDBName";

        private static CultureInfo _cultureTR;
        public static CultureInfo CultureTR
        {
            get { return _cultureTR ?? (_cultureTR = new CultureInfo("tr-TR")); }
        }

        private static CultureInfo _cultureEN;
        public static CultureInfo CultureEN
        {
            get { return _cultureEN ?? (_cultureEN = new CultureInfo("en-US")); }
        }
    }
}
