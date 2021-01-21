using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Domain.Repositories.Abstractions
{
    public interface IUniqueString
    {
        public bool IsStringUnique(string inputString);
    }
}
