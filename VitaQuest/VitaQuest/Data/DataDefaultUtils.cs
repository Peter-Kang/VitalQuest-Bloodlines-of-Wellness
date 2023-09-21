using Microsoft.IdentityModel.Tokens;

namespace VitaQuest.Data
{
    public class DataDefaultUtils
    {
        public static string StringDefault( in string value, in string defaultValue)
        {
            if (!value.IsNullOrEmpty())
            {
                return value;
            }
            return defaultValue;
        }

    }
}
