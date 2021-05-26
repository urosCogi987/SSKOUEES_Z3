using PredmetniZadatak3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml;

namespace PredmetniZadatak3.Controllers
{
    public class XMLParser
    {
        public static void LoadSubstations(HashSet<SubstationEntity> substationEntities, string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            XmlNodeList nodeList;
            nodeList = xmlDocument.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");

            foreach (XmlNode node in nodeList)
            {
                SubstationEntity subEntity = new SubstationEntity();
                
                subEntity.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                subEntity.Name = node.SelectSingleNode("Name").InnerText;
                subEntity.X = double.Parse(node.SelectSingleNode("X").InnerText);
                subEntity.Y = double.Parse(node.SelectSingleNode("Y").InnerText);
                subEntity.TypeEntity = TypeEntity.Substation;
                subEntity.ToolTip = "Substation:\nID: " + subEntity.Id + "\nName: " + subEntity.Name;
                
                substationEntities.Add(subEntity);
            }
        }

        public static void LoadNodes(HashSet<NodeEntity> nodeEntities, string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            XmlNodeList nodeList;
            nodeList = xmlDocument.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");

            foreach (XmlNode node in nodeList)
            {
                NodeEntity nodeEntity = new NodeEntity();

                nodeEntity.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                nodeEntity.Name = node.SelectSingleNode("Name").InnerText;                
                nodeEntity.X = double.Parse(node.SelectSingleNode("X").InnerText);
                nodeEntity.Y = double.Parse(node.SelectSingleNode("Y").InnerText);                
                nodeEntity.TypeEntity = TypeEntity.Node;
                nodeEntity.ToolTip = "Node:\nID: " + nodeEntity.Id + "\nName: " + nodeEntity.Name;
                
                nodeEntities.Add(nodeEntity);
            }
        }

        public static void LoadSwitches(HashSet<SwitchEntity> switchEntities, string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            XmlNodeList nodeList;
            nodeList = xmlDocument.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");

            foreach (XmlNode node in nodeList)
            {
                SwitchEntity swEntity = new SwitchEntity();

                swEntity.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                swEntity.Name = node.SelectSingleNode("Name").InnerText;
                swEntity.Status = node.SelectSingleNode("Status").InnerText;
                swEntity.X = double.Parse(node.SelectSingleNode("X").InnerText);
                swEntity.Y = double.Parse(node.SelectSingleNode("Y").InnerText);
                swEntity.TypeEntity = TypeEntity.Switch;
                swEntity.ToolTip = "Switch:\nID: " + swEntity.Id + "\nName: " + swEntity.Name + "\nStatus: " + swEntity.Status;
                
                switchEntities.Add(swEntity);
            }
        }

        public static void LoadLines(HashSet<LineEntity> lineEntities, string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            XmlNodeList nodeList;
            nodeList = xmlDocument.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

            foreach (XmlNode node in nodeList)
            {
                LineEntity lineEntity = new LineEntity();

                lineEntity.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                lineEntity.Name = node.SelectSingleNode("Name").InnerText;
                if (node.SelectSingleNode("IsUnderground").InnerText.Equals("true"))
                {
                    lineEntity.IsUnderground = true;
                }
                else
                {
                    lineEntity.IsUnderground = false;
                }
                lineEntity.R = float.Parse(node.SelectSingleNode("R").InnerText);
                lineEntity.ConductorMaterial = node.SelectSingleNode("ConductorMaterial").InnerText;
                lineEntity.LineType = node.SelectSingleNode("LineType").InnerText;
                lineEntity.ThermalConstantHeat = long.Parse(node.SelectSingleNode("ThermalConstantHeat").InnerText);
                lineEntity.FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText);
                lineEntity.SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText);

                lineEntity.Vertices = new List<Point3D>();
                double x = 0;
                double y = 0;                    

                foreach(XmlNode childNode in node.SelectSingleNode("Vertices"))
                {                    
                    x = double.Parse(childNode.SelectSingleNode("X").InnerText);
                    y = double.Parse(childNode.SelectSingleNode("Y").InnerText);
                    lineEntity.Vertices.Add(new Point3D(x, y, 1));
                }

                lineEntity.ConvertedVertices = new List<Point3D>();

                lineEntities.Add(lineEntity);
            }
        }
    }
}
