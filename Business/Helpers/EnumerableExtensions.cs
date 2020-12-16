using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> AsDepthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
		{
			yield return head;
			foreach (var node in childrenFunc(head))
			{
				foreach (var child in AsDepthFirstEnumerable(node, childrenFunc))
				{
					yield return child;
				}
			}
		}

		public static IEnumerable<T> AsDepthFirstEnumerable<T>(this T head, Func<T, ICollection<T>> childrenFunc)
		{
			yield return head;
			foreach (var node in childrenFunc(head))
			{
				foreach (var child in AsDepthFirstEnumerable(node, childrenFunc))
				{
					yield return child;
				}
			}
		}

		public static IEnumerable<T> AsBreadthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
		{
			yield return head;
			var last = head;
			foreach (var node in AsBreadthFirstEnumerable(head, childrenFunc))
			{
				foreach (var child in childrenFunc(node))
				{
					yield return child;
					last = child;
				}
				if (last.Equals(node)) yield break;
			}
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> method)
		{
			foreach (var item in items)
				method(item);
			return items;
		}

	}
}
