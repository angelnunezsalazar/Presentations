using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public class ThreadStaticInstanceScoper<T> : InstanceScoperBase<T>
	{
		[ThreadStatic]
		private static readonly IDictionary _dictionary = new Dictionary<string, T>();

		protected override IDictionary GetDictionary()
		{
			return _dictionary;
		}
	}
}