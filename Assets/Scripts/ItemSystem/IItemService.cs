using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ItemSystem
{
    public interface IItemService : IService
    {
        IItem GetItem(string itemId);
        //void UseItem<TItem>(IItem item);
        //void GetTypeItem()

    }

    
}
