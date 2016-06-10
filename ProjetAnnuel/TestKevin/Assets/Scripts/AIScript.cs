using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class AIScript : MonoBehaviour {

    const string FILE_NAME = "xmlFile.xml";

	// Use this for initialization
	void Start () 
    {
        CreateXMLFile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateXMLFile()
    {
        if (!File.Exists(FILE_NAME))
        {
            // Tell user that file isn't there.
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(FILE_NAME, settings);

            writer.WriteStartDocument();

                writer.WriteStartElement("Datas");

                    writer.WriteStartElement("GameA");
                    writer.WriteElementString("Data", "A");
                    writer.WriteEndElement();

                    writer.WriteStartElement("GameB");
                    writer.WriteElementString("Data", "B");
                    writer.WriteEndElement();

                    writer.WriteStartElement("GameC");
                    writer.WriteElementString("Data", "C");
                    writer.WriteEndElement();

                writer.WriteEndElement();


            writer.WriteEndDocument();

            writer.Close();
        }
        else
        {
            // The file exists, so now go party on it.
            XmlDocument doc = new XmlDocument();
            doc.Load(FILE_NAME);

            //XmlNode root = doc.DocumentElement;

            XmlNode node = doc.SelectSingleNode("/Datas/GameB");

            if(node != null)
            {
                XmlNode nodeTmp = node.SelectSingleNode("Position");

                if (nodeTmp == null)
                {
                    XmlElement child = doc.CreateElement("Position");
                    XmlElement childX = doc.CreateElement("X");
                    XmlElement childY = doc.CreateElement("Y");
                    childX.InnerText = "50";
                    childY.InnerText = "4";

                    child.AppendChild(childX);
                    child.AppendChild(childY);

                    node.AppendChild(child);

                    doc.Save(FILE_NAME);
                }
            }
        }
    }

    void ReadXMLFile()
    {

    }

    void WriteXMLFile()
    {

    }
}
