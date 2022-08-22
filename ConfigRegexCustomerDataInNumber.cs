using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CleanQueryFromCustomerData
{
    public class ConfigRegexCustomerDataInNumber
    {
        #region Default Data
        private const string ID_CHECK = "^[0-9]{9}$";
        private const string CITIZENSHIP_NUMBER = "/^[1-9]\\d{5}(18|19|20)\\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\\d{3}[0-9Xx]$/";
        private const string CREDIT_CARD_CHECK = "^(1298|1267|4512|4567|8901|8933)([\\-\\s]?[0-9]{4}){3}$";
        private const string SSN_CHECK = "^(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$";
        #endregion
        public static string CONFIG_FNAME = "configRegexCustomerDataInNumber.xml";

        public static ConfigData GetConfigData()
        {
            if (!File.Exists(CONFIG_FNAME)) // create config file with default values
            {
                using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                    ConfigData sxml = new ConfigData();
                    xs.Serialize(fs, sxml);
                    return sxml;
                }
            }
            else // read configuration from file
            {
                ConfigData sc = new ConfigData();
                using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                    try
                    {
                        sc = (ConfigData)xs.Deserialize(fs);
                    }
                    catch (Exception ex)
                    {
                        while (ex != null)
                        {
                            return null;
                        }
                    }
                    return sc;
                }
            }
        }

        public static bool SaveConfigData(ConfigData config)
        {
            if (!File.Exists(CONFIG_FNAME)) return false; // don't do anything if file doesn't exist

            using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                xs.Serialize(fs, config);
                return true;
            }
        }

        // this class holds configuration data
        public class ConfigData
        {
            public string idCheck;
            public string citizenshipNumberCheck;
            public string creditCardCheack;
            public string ssnCheck;

            public ConfigData()
            {
                idCheck = ID_CHECK;
                citizenshipNumberCheck = CITIZENSHIP_NUMBER;
                creditCardCheack = CREDIT_CARD_CHECK;
                ssnCheck = SSN_CHECK;
            }
        }
    }
}

