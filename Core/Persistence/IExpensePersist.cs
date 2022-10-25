using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence
{
    public interface IExpensePersist
    {
        IExpense Load(ulong id_);
        void Save(IExpense expense_);
      
    }
}
