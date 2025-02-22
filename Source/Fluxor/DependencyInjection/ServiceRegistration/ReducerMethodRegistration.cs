﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluxor.DependencyInjection.ServiceRegistration
{
	internal static class ReducerMethodRegistration
	{
		public static void Register(
			IServiceCollection services,
			ReducerMethodInfo[] reducerMethodInfos)
		{
			IEnumerable<Type> hostClassTypes =
				reducerMethodInfos
					.Select(x => x.HostClassType)
					.Where(t => !t.IsAbstract)
					.Distinct();

			foreach (Type hostClassType in hostClassTypes)
				services.AddScoped(hostClassType);
		}
	}
}
