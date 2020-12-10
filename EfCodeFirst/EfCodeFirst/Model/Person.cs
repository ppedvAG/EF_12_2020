using System;

namespace EfCodeFirst.Model
{
    //POCO = Plain old CLR Objects
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime GebDatum { get; set; }
    }
}
