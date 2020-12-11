using System;

namespace ppedv.GiftManager.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
    }




}
