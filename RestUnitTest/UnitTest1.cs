using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib;
using ClassDemoRestService.Controllers;
using ModelLib.model;
using Newtonsoft.Json;

namespace RestUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private ItemsController _itemsController;
        // List from itemController
        private static List<Item> _items;

        [TestInitialize]
        public void Initialize()
        {
            _itemsController = new ItemsController();
            _items = new List<Item>()
            {
                new Item(9000,"Bread","Low",33),
                new Item(9001,"Bread","Middle",21),
                new Item(9002,"Beer","low",70.5),
                new Item(9003,"Soda","High",0.21),
                new Item(9004,"Beer","low",70.15),
                new Item(9005,"Soda","High",121.4),
                new Item(9006,"Milk","Low",55.8)
            }; ;
        }

        [TestMethod]
        public void TestItemController()
        {
            // Clear list for testing
            ItemsController.Items = new List<Item>();

            // Post a couple of pre determined items
            foreach (var item in _items)
            {
                _itemsController.Post(item);
            }

            // Get all items
            var getAllItems = _itemsController.Get().ToList();

            // Check if the Posted list and the list we got with Get are equal.
            for (int i = 0; i < getAllItems.Count; i++)
            {
                Assert.AreEqual(_items[i], getAllItems[i]);
            }

            // Create item and Post it
            Item postItem = new Item(9010, "Bread", "Low", 33);
            _itemsController.Post(postItem);

            // Add item to pre determined list for later use
            _items.Add(postItem);

            // Get item just posted
            Item getItem = _itemsController.Get(9010);

            //Check if they are equal
            Assert.AreEqual(postItem, getItem);

            // Get all with name "Bread"
            var getByName = _itemsController.GetByName("Bread").ToList();
            // Find all with name "Bread" in pre determined list
            var getByNameList = _items.FindAll(i => i.Name == "Bread");

            // Compare the two lists
            for (int i = 0; i < getByName.Count; i++)
            {
                Assert.AreEqual(getByNameList[i], getByName[i]);
            }

            // Get all with quality "low" (The method uses .lower so it will get all with "low" but AreEqual can't compare "low" and "Low")
            var getByQuality = _itemsController.GetByQuality("low").ToList();
            // Find all with quality "low" in pre determined list
            var getByQualityList = _items.FindAll(i => i.Quality.ToLower() == "low");

            for (int i = 0; i < getByQuality.Count; i++)
            {
                Assert.AreEqual(getByQualityList[i], getByQuality[i]);
            }

            // Get all where value is atleast 50
            var getFilterLow = _itemsController.GetWithFilter(new FilterItem(50, 0)).ToList();
            // You know it by now...
            var getFilterLowList = _items.FindAll(i => i.Quantity > 50);

            // Get all where value is at maximum 50
            var getFilterHigh = _itemsController.GetWithFilter(new FilterItem(0, 50)).ToList();
            // You know it by now...
            var getFilterHighList = _items.FindAll(i => i.Quantity < 50);

            // Get all between 30 and 120
            var getFilterBoth = _itemsController.GetWithFilter(new FilterItem(30, 120)).ToList();
            // You know it by now...
            var getFilterBothList = _items.FindAll(i => i.Quantity > 30 && i.Quantity < 120);

            // Compare all three variants
            for (int i = 0; i < getFilterLow.Count; i++)
            {
                Assert.AreEqual(getFilterLowList[i], getFilterLow[i]);
            }
            for (int i = 0; i < getFilterHigh.Count; i++)
            {
                Assert.AreEqual(getFilterHighList[i], getFilterHigh[i]);
            }
            for (int i = 0; i < getFilterBoth.Count; i++)
            {
                Assert.AreEqual(getFilterBothList[i], getFilterBoth[i]);
            }

            // Create Item to Put and Put it
            Item putItem = new Item(9010, "Cheese", "Low", 33);
            _itemsController.Put(9010, putItem);

            // Get item just put
            Item getPutItem = _itemsController.Get(9010);

            // double float precision problem when comparing to objects containing double/float etc.
            Assert.AreEqual(JsonConvert.SerializeObject(putItem), JsonConvert.SerializeObject(getPutItem));

            _itemsController.Delete(9010);

            Item itemDeleted = _itemsController.Get(9010);

            Assert.IsNull(itemDeleted);
        }
    }
}
