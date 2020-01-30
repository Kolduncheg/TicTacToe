using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class XmlManager : MonoBehaviour
{
    public static XmlManager instance;

    string filePath;
    XmlDocument xmlDocument;
    XmlNode root;
    XmlNode settings;
    XmlAttribute xmlattr = null;
    Dictionary<string, string> xmlParams;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        filePath = Application.streamingAssetsPath + "/Levels/" + "config.xml";
    }

    public void SaveLevelConfig(int Rows, int Cols)
    {
        xmlDocument = new XmlDocument();
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

        xmlParams = new Dictionary<string, string>();

        root = xmlDocument.CreateElement("level");
        xmlDocument.AppendChild(root);

        settings = xmlDocument.CreateElement("settings");
        xmlParams["rows"] = Rows.ToString();
        xmlParams["cols"] = Cols.ToString();

        xmlattr = null;

        foreach (KeyValuePair<string, string> settingsParam in xmlParams)
        {
            xmlattr = xmlDocument.CreateAttribute(settingsParam.Key);
            xmlattr.Value = settingsParam.Value;
            settings.Attributes.Append(xmlattr);
        }

        root.AppendChild(settings);

        xmlDocument.Save(filePath);
    }

    public Vector2Int LoadLevelConfig()
    {
        Vector2Int fieldSize = new Vector2Int(3, 3);

        if (File.Exists(filePath))
        {
            xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(filePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return fieldSize;
            }

            root = xmlDocument.SelectSingleNode("level");
            settings = root.SelectSingleNode("settings");

            foreach (XmlNode xmlNode in settings.Attributes)
            {
                switch (xmlNode.Name)
                {
                    case "rows":
                        fieldSize.x = int.Parse(xmlNode.Value);
                        break;
                    case "cols":
                        fieldSize.y = int.Parse(xmlNode.Value);
                        break;
                    default:
                        break;
                }
            }
            return fieldSize;
        }
        else
        {
            return fieldSize;
        }
    }

}
