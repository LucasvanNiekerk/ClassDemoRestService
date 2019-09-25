using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModelLib.model;

namespace ClassDemoRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static List<Item> items = new List<Item>()
        {
            new Item(1,"Bread","Low",33),
            new Item(2,"Bread","Middle",21),
            new Item(3,"Beer","low",70.5),
            new Item(4,"Soda","High",21.4),
            new Item(5,"Milk","Low",55.8)
        };

        public static List<Item> Items { get => items; set => items = value; }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items;
        }

        // GET: api/Items/5
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return items.Find(i => i.Id == id);
        }


        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetByName(string substring)
        {
            //ToLower for at sikre at den ikke klare over store bokstaver.
            return items.FindAll(i => i.Name.ToLower() == substring.ToLower());
        }

        [HttpGet]
        [Route("Quality/{substring}")]
        public IEnumerable<Item> GetByQuality(string substring)
        {
            //ToLower for at sikre at den ikke klare over store bokstaver.
            return items.FindAll(i => i.Quality.ToLower() == substring.ToLower());
        }

        [HttpGet]
        [Route("Search")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {
            if (filter.LowQuantity != 0 && filter.HighQuantity != 0)
            {
                return items.FindAll(i => i.Quantity > filter.LowQuantity && i.Quantity < filter.HighQuantity);
            }
            else if (filter.HighQuantity != 0)
            {
                return items.FindAll(i => i.Quantity < filter.HighQuantity);
            }
            else if (filter.LowQuantity != 0)
            {
                return items.FindAll(i => i.Quantity > filter.LowQuantity);
            }
            else
            {
                return null;
            }
        }

        // POST: api/Items
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            items.Add(value);
        }


        // PUT: api/Items/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != null)
            {
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}
