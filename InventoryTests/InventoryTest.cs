using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagement.Business;

namespace InventoryTests
{
    [TestClass]
    public class InventoryTest
    {

        ///////////////////Check Inventory File load
        [TestMethod]
        public void TestImport()
        {
            BusinessRules businessRules = new BusinessRules(".\\Test Data\\Inventory.xml");

            Assert.AreEqual(10, businessRules.sourceInventory.inventoryProducts.Count,"Unexpected number of records loaded");
        }

        ////////////////////TestAgedBrie
        [TestMethod]
        public void TestAgedBrieSellIn()
        {
            productAgedBrie productAgedBrie = new productAgedBrie(name:"AGED BRIE", sellIn: 1, quality: 1);
            Assert.AreEqual("0", productAgedBrie.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestAgedBrieQuality()
        {
            productAgedBrie productAgedBrie = new productAgedBrie(name: "AGED BRIE", sellIn: 1, quality: 1);
            Assert.AreEqual("2", productAgedBrie.outquality, "Unexpected Quality");
        }

        ////////////////////TestXmasCrackers
        [TestMethod]
        public void TestXmasCrackersSellIn_1()
        {
            productXmasCrackers productXmasCrackers = new productXmasCrackers(name:"CHRISTMAS CRACKERS", sellIn: -1, quality: 2);
            Assert.AreEqual("-2", productXmasCrackers.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestXmasCrackersQuality_1()
        {
            productXmasCrackers productXmasCrackers = new productXmasCrackers(name: "CHRISTMAS CRACKERS", sellIn: -1, quality: 2);
            Assert.AreEqual("0", productXmasCrackers.outquality, "Unexpected Quality");
        }

        [TestMethod]
        public void TestXmasCrackersSellIn_2()
        {
            productXmasCrackers productXmasCrackers = new productXmasCrackers(name: "CHRISTMAS CRACKERS", sellIn: 9, quality: 2);
            Assert.AreEqual("8", productXmasCrackers.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestXmasCrackersQuality_2()
        {
            productXmasCrackers productXmasCrackers = new productXmasCrackers(name: "CHRISTMAS CRACKERS", sellIn: 9, quality: 2);
            Assert.AreEqual("4", productXmasCrackers.outquality, "Unexpected Quality");
        }

        [TestMethod]
        public void TestXmasCrackersSellIn_3()   //Extra Xmas Cracker Test for 5 days or less. Quality should go UP by 3
        {
            productXmasCrackers productXmasCrackers = new productXmasCrackers(name: "CHRISTMAS CRACKERS", sellIn: 5, quality: 4);
            Assert.AreEqual("4", productXmasCrackers.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestXmasCrackersQuality_3()
        {
            productXmasCrackers productXmasCrackers = new productXmasCrackers(name: "CHRISTMAS CRACKERS", sellIn: 5, quality: 4);
            Assert.AreEqual("7", productXmasCrackers.outquality, "Unexpected Quality");
        }


        ////////////////////TestSoap
        [TestMethod]
        public void TestSoapSellIn()
        {
            productSoap productSoap = new productSoap(name: "SOAP", sellIn: 2, quality: 2);
            Assert.AreEqual("2", productSoap.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestSoapQuality()
        {
            productSoap productSoap = new productSoap(name: "SOAP", sellIn: 2, quality: 2);
            Assert.AreEqual("2", productSoap.outquality, "Unexpected Quality");
        }

        ////////////////////TestFrozenItem
        [TestMethod]
        public void TestFrozenItemSellIn_1()
        {
            productFrozenItem productFrozenItem = new productFrozenItem(name: "FROZEN ITEM", sellIn: -1, quality: 55);
            Assert.AreEqual("-2", productFrozenItem.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestFrozenItemQuality_1()
        {
            productFrozenItem productFrozenItem = new productFrozenItem(name: "FROZEN ITEM", sellIn: -1, quality: 55);
            Assert.AreEqual("50", productFrozenItem.outquality, "Unexpected Quality");
        }

        [TestMethod]
        public void TestFrozenItemSellIn_2()
        {
            productFrozenItem productFrozenItem = new productFrozenItem(name: "FROZEN ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual("1", productFrozenItem.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestFrozenItemQuality_2()
        {
            productFrozenItem productFrozenItem = new productFrozenItem(name: "FROZEN ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual("1", productFrozenItem.outquality, "Unexpected Quality");
        }

        ////////////////////TestInvalidItem
        [TestMethod]
        public void TestInvalidItemName()   //Extra test for return of NO SUCH ITEM
        {
            UnknownProduct unknownProduct = new UnknownProduct(name: "INVALID ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual("NO SUCH ITEM", unknownProduct.outName, "Unexpected Name");
        }


        [TestMethod]
        public void TestInvalidItemSellIn()
        {
            UnknownProduct unknownProduct  = new UnknownProduct(name: "INVALID ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual(string.Empty, unknownProduct.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestInvalidItemQuality()
        {
            UnknownProduct unknownProduct = new UnknownProduct(name: "INVALID ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual(string.Empty, unknownProduct.outquality, "Unexpected Quality");
        }

        ////////////////////TestFreshItem
        [TestMethod]
        public void TestFreshItemSellIn_1()
        {
            productFreshItem productFreshItem = new productFreshItem(name: "FRESH ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual("1", productFreshItem.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestFreshItemQuality_1()
        {
            productFreshItem productFreshItem = new productFreshItem(name: "FRESH ITEM", sellIn: 2, quality: 2);
            Assert.AreEqual("0", productFreshItem.outquality, "Unexpected Quality");
        }

        public void TestFreshItemSellIn_2()
        {
            productFreshItem productFreshItem = new productFreshItem(name: "FRESH ITEM", sellIn: -1, quality: 5);
            Assert.AreEqual("-2", productFreshItem.outSellin, "Unexpected Sellin");
        }

        [TestMethod]
        public void TestFreshItemQuality_2()
        {
            productFreshItem productFreshItem = new productFreshItem(name: "FRESH ITEM", sellIn: -1, quality: 5);
            Assert.AreEqual("1", productFreshItem.outquality, "Unexpected Quality");
        }


    }
}
