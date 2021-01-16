using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Domain.Repositories.Abstractions
{
    public interface IPersonRepository
    {
        public bool DoesPinExist(string pin);
    }
}
