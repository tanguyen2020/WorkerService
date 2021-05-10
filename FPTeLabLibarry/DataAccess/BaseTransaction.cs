using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public class BaseTransaction : ITransaction
    {
        DbTransaction _transaction;
        public BaseTransaction(DbTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollBack()
        {
            _transaction?.Rollback();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
