using ppedv.GiftManager.Model.Contracts;
using System;

namespace ppedv.GiftManager.Logic
{
    public class Core
    {
        public IRepository Repository { get; private set; }

        public Core(IRepository repo) //di in here 
        {
            this.Repository = repo;
        }

        public Core() : this(new Data.EF.EfRepository())
        { }
    }
}
