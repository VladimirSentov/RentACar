using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    // Run the method.
                    invocation.Proceed();
                    // Define the operation as completed.
                    transactionScope.Complete();
                }
                catch (Exception )
                {
                    // Rolls back the action if any exception is caught.
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}