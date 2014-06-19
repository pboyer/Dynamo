﻿using System;
using System.Reflection;

using NUnit.Framework;
using RevitServices.Transactions;

namespace DSRevitNodesTests
{
    /// <summary>
    /// Base class for units tests of Revit nodes.
    /// 
    /// </summary>
    public abstract class RevitNodeTestBase
    {
        public RevitNodeTestBase()
        {
            AssemblyResolver.Setup();
        }

        [SetUp]
        public virtual void SetupTransactionManager()
        {
            // create the transaction manager object
            TransactionManager.SetupManager(new AutomaticTransactionStrategy());

            // Tests do not run from idle thread.
            TransactionManager.Instance.DoAssertInIdleThread = false;
        }

        [TearDown]
        public virtual void ShutDownTransactionManager()
        {
            // Automatic transaction strategy requires that we 
            // close the transaction if it hasn't been closed by 
            // by the end of an evaluation. It is possible to 
            // run the test framework without running Dynamo, so
            // we ensure that the transaction is closed here.
            TransactionManager.Instance.ForceCloseTransaction();
        }
    }
}
