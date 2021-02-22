using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace InventoryManagement.Data
{
    public class InventoryData
    {
        public List<InventoryProduct> inventoryProducts;


        public InventoryData(string inventoryDataPath) //Import inventory XML from supplied path
        {
            XmlSerializer ser = new XmlSerializer(typeof(Inventory));
            using (XmlReader reader = XmlReader.Create(inventoryDataPath))
            {
                inventoryProducts = ((Inventory)ser.Deserialize(reader)).Items.ToList();
            }
        }

    }
}
