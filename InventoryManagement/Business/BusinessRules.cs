using System;
using System.Collections.Generic;
using System.Text;
using InventoryManagement.Data;

namespace InventoryManagement.Business
{
    public class BusinessRules
    {
        private  InventoryData _sourceInventory;

        public BusinessRules(string inventoryDataPath)
        {
            _sourceInventory = new InventoryData(inventoryDataPath);
        }

        public InventoryData sourceInventory { get { return _sourceInventory; } }
        public ReportData updatedInventory()
        {
            ReportData reportData = new ReportData(sourceInventory);

            return reportData;
        }

    }

    public class ReportData
    {
        public List<ReportProduct> reportProducts = new List<ReportProduct>();

        public ReportData(InventoryData inventoryData)
        {
            foreach (InventoryProduct item in inventoryData.inventoryProducts)
            {
                switch (item.name.ToUpper())
                {
                    case "SOAP":
                        reportProducts.Add(new productSoap(name: item.name, sellIn: item.sellIn, quality: item.quality));
                        break;
                    case "CHRISTMAS CRACKERS":
                        reportProducts.Add(new productXmasCrackers(name: item.name, sellIn: item.sellIn, quality: item.quality));
                        break;
                    case "AGED BRIE":
                        reportProducts.Add(new productAgedBrie(name: item.name, sellIn: item.sellIn, quality: item.quality));
                        break;
                    case "FRESH ITEM":
                        reportProducts.Add(new productFreshItem(name: item.name, sellIn: item.sellIn, quality: item.quality));
                        break;
                    case "FROZEN ITEM":
                        reportProducts.Add(new productFrozenItem(name: item.name, sellIn: item.sellIn, quality: item.quality));
                        break;
                    default:
                        reportProducts.Add(new UnknownProduct(name: item.name, sellIn: item.sellIn, quality: item.quality));
                        break;
                }

            }

        }
    }

    public abstract class ReportProduct
    {
        private string name;
        protected int sellIn;
        protected int quality;

        public string outName { get { return outputName(); } }
        public string outSellin { get { return updateSellIn(); } }
        public string outquality { get { return updateQuality(); } }

        protected const int defaultqualityIncrement = -1;
        protected const int expiredFactor = 2;

        public ReportProduct(string name, int sellIn, int quality)
        {
            this.name = name;
            this.sellIn = sellIn;
            this.quality = quality;
        }

        protected virtual string outputName()
        {
            return name;
        }
        protected virtual string updateSellIn()
        {
            return newSellIn().ToString();
        }

        protected virtual string updateQuality()
        {
            int ret = 0;

            if (newSellIn() >= 0)
            {
                int qi = getQualityIncrement();
                ret = quality + qi;
            }
            else
            {
                int qi = getQualityIncrement();
                ret = quality + (qi * expiredFactor);
            }
            return limitQuality(ret).ToString();
        }

        protected int newSellIn()
        {
            return sellIn - 1;
        }

        protected int limitQuality(int q)
        {
            if (q > 50) return 50;
            else if (q < 0) return 0;
            else return q;
        }

        protected virtual int getQualityIncrement()
        {
            return defaultqualityIncrement;
        }


    }

    public class productAgedBrie : ReportProduct
    {
        private const int expiredBrieQualityIncrement = 1;
        public productAgedBrie(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {

        }

        protected override int getQualityIncrement()
        {
            if (newSellIn() > 0) return defaultqualityIncrement;
            else return expiredBrieQualityIncrement;
        }
    }

    public class productSoap : ReportProduct
    {
        public productSoap(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {
        }

        protected override string updateSellIn()
        {
            return sellIn.ToString();
        }
        protected override string updateQuality()
        {
            return sellIn.ToString();
        }

    }

    public class productXmasCrackers : ReportProduct
    {

        public productXmasCrackers(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {
        }

        protected override string updateQuality()
        {
            int ret = 0;

            if (newSellIn() >= 0)
            {
                int qi = getQualityIncrement();
                ret = quality + qi;
            }
            else
            {
                ret = 0; ///COMMENT THIS
            }
            return limitQuality(ret).ToString();
        }

        protected override int getQualityIncrement()
        {
            if (newSellIn() <= 0) return 0;
            else if (newSellIn() <= 5) return 3;
            else if (newSellIn() < 10) return 2;
            else return defaultqualityIncrement;
        }
    }

    public class productFreshItem : ReportProduct
    {
        private const int FreshQualityIncrement = -2;
        public productFreshItem(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {
        }

        protected override int getQualityIncrement()
        {
            return FreshQualityIncrement;
        }

    }

    public class productFrozenItem : ReportProduct
    {
        public productFrozenItem(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {
        }


    }

    public class UnknownProduct : ReportProduct
    {
        public UnknownProduct(string name, int sellIn, int quality) : base(name, sellIn, quality)
        {
        }

        protected override string outputName()
        {
            return "NO SUCH ITEM";
        }

        protected override string updateSellIn()
        {
            return string.Empty;
        }

        protected override string updateQuality()
        {
            return string.Empty;
        }
    }
}
