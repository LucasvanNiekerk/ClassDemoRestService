﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.model
{
    public class Item
    {
        private int _id;
        private string _name;
        private string _quality;
        private double _quantity;

        public Item()
        {
            
        }

        public Item(int id, string name, string quality, double quantity)
        {
            Id = id;
            Name = name;
            Quality = quality;
            Quantity = quantity;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Quality { get => _quality; set => _quality = value; }
        public double Quantity { get => _quantity; set => _quantity = value; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Quality)}: {Quality}, {nameof(Quantity)}: {Quantity}";
        }
    }
}
