﻿using Fluxor.UnitTests.DependencyInjectionTests.ReducerDiscoveryTests.DiscoverReducersWithActionInMethodSignatureTests.SupportFiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Fluxor.UnitTests.DependencyInjectionTests.ReducerDiscoveryTests.DiscoverReducersWithActionInMethodSignatureTests
{
	public class DiscoverReducersWithActionInMethodSignatureTests
	{
		private readonly IServiceProvider ServiceProvider;
		private readonly IStore Store;
		private readonly IState<TestState> State;

		[Fact]
		public void WhenActionIsDispatched_ThenReducerWithActionInMethodSignatureIsExecuted()
		{
			Assert.Equal(0, State.Value.Counter);
			Store.Dispatch(new TestAction());
			// 2 Reducers
			// 1 assembly scanned (generic descendant)
			// + 1 type scanned (closed generic)
			Assert.Equal(2, State.Value.Counter);
		}

		public DiscoverReducersWithActionInMethodSignatureTests()
		{
			var services = new ServiceCollection();
			services.AddFluxor(x => x
				.ScanAssemblies(GetType().Assembly)
				.ScanTypes(typeof(TypesThatShouldOnlyBeScannedExplicitly.ExplicitlyScannedReducers))
				.AddMiddleware<IsolatedTests>());

			ServiceProvider = services.BuildServiceProvider();
			Store = ServiceProvider.GetRequiredService<IStore>();
			State = ServiceProvider.GetRequiredService<IState<TestState>>();

			Store.InitializeAsync().Wait();
		}
	}
}
