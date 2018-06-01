using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Project2.Models
{
    public class Xml
    {
        public void GenerateXml(UserModel u)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement header = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, header);

            XmlElement elUser = doc.CreateElement("user");
            doc.AppendChild(elUser);

            //Login
            XmlElement elLogin = doc.CreateElement("Login");
            elLogin.InnerText = u.Login;
            elUser.AppendChild(elLogin);

            //Password
            XmlElement elPassword = doc.CreateElement("Password");
            XmlAttribute elPasswordAttribute = doc.CreateAttribute("hash");
            elPasswordAttribute.Value = "SHA1";
            elPassword.InnerText = u.Password;
            elPassword.Attributes.Append(elPasswordAttribute);
            elUser.AppendChild(elPassword);

            //Description
            //XmlElement elDescription = doc.CreateElement("Description");
            //elDescription.InnerText = "@Description";
            //elRoot.AppendChild(elDescription);

            //Regex
            XmlElement elRegex = doc.CreateElement("Regex");
            XmlAttribute elRegexAttribute = doc.CreateAttribute("Description");
            elRegexAttribute.Value = u.Description;
            elRegex.InnerText = u.Reg;
            elRegex.Attributes.Append(elRegexAttribute);
            elUser.AppendChild(elRegex);

            //Settings
            XmlElement elSettings = doc.CreateElement("Settings");
            elUser.AppendChild(elSettings);

            //MinLength
            if (!u.MinLength.ToString().Equals("0"))
            {
                XmlElement elMinLength = doc.CreateElement("MinLength");
                elMinLength.InnerText = u.MinLength.ToString();
                elSettings.AppendChild(elMinLength);
            }


            //MaxLength
            if (!u.MaxLength.ToString().Equals("0") )
            {
                XmlElement elMaxLength = doc.CreateElement("MaxLength");
                elMaxLength.InnerText = u.MaxLength.ToString();
                elSettings.AppendChild(elMaxLength);
            }
            //MinUpperCase
            if (!u.MinUppercase.ToString().Equals("0") )
            {
                XmlElement elMinUppercase = doc.CreateElement("MinUppercase");
                elMinUppercase.InnerText = u.MinUppercase.ToString();
                elSettings.AppendChild(elMinUppercase);
            }

            //MinLowercase
            if (!u.MinLowercase.ToString().Equals("0") )
            {
                XmlElement elMinLowercase = doc.CreateElement("MinLowercase");
                elMinLowercase.InnerText = u.MinLowercase.ToString();
                elSettings.AppendChild(elMinLowercase);
            }

            //MinSpecialSigns
            if (!u.MinSpecialSigns.ToString().Equals("0") )
            {
                XmlElement elMinSpecialSigns = doc.CreateElement("MinSpecialSigns");
                elMinSpecialSigns.InnerText = u.MinSpecialSigns.ToString();
                elSettings.AppendChild(elMinSpecialSigns);
            }
            //MinDigits
            if (!u.MinDigits.ToString().Equals("0") )
            {
                XmlElement elMinDigits = doc.CreateElement("MinDigits");
                elMinDigits.InnerText = u.MinDigits.ToString();
                elSettings.AppendChild(elMinDigits);
            }
            doc.Save("tet.xml");
        }
    }
}