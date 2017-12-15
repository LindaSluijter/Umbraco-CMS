﻿using System.Collections.Generic;
using System.Reflection;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;

namespace Umbraco.Tests.DI
{
    public abstract class ResolverBaseTest // fixme rename, do something!
    {
        protected TypeLoader TypeLoader { get; private set; }
        protected ProfilingLogger ProfilingLogger { get; private set; }

        [SetUp]
        public void Initialize()
        {
            ProfilingLogger = new ProfilingLogger(Mock.Of<ILogger>(), Mock.Of<IProfiler>());

            TypeLoader = new TypeLoader(NullCacheProvider.Instance,
                ProfilingLogger,
                false)
            {
                AssembliesToScan = AssembliesToScan
            };
        }

        [TearDown]
        public void TearDown()
        {
            Current.Reset();
        }

        protected virtual IEnumerable<Assembly> AssembliesToScan
            => new[]
            {
                GetType().Assembly // this assembly only
            };
    }
}
