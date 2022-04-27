﻿namespace HealthInstitution.MVVM.Models.Entities
{
    public class Allergen
    {
        private int _id;
        private string _name;

        public int Id { get => _id; set { _id = value; } }
        public string Name { get => _name; set { _name = value; } }

        public Allergen()
        {

        }

        public Allergen(int id, string name)
        {
            _id = id;
            _name = name;
        }

    }
}