﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountLogic.BankAccounts;

namespace BankAccountLogic
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}
