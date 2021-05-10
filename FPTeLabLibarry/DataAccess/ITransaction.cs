using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public interface ITransaction: IDisposable
    {
        void Commit();
        void RollBack();
    }
}
