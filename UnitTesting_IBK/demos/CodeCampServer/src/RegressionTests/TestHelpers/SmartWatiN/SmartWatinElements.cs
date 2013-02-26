using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodeCampServer.RegressionTests.Web;
using MbUnit.Framework;
using mshtml;
using RegressionTests;
using RegressionTests.Web;
using WatiN.Core;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using AttributeConstraint=WatiN.Core.Constraints.AttributeConstraint;

namespace CodeCampServer.RegressionTests.TestHelpers.SmartWatiN
{
	public static class ElementExtensions
	{
		public static Element CheckAll(this Element element, object o)
		{
			return CheckAll(element, WebTest.With(o));
		}

		public static Element CheckAll(this Element element, IDictionary<string, object> values)
		{
			string errors = null;

			ExecutionSteps.Log.WriteLine("Check the following values:");
			foreach (var pair in values)
			{
				try
				{
					if (pair.Value is IEnumerable<string>)
					{
						var expectedValues = (IEnumerable<string>) pair.Value;
						Element[] actualElements = element.LocateAll(pair.Key);
						string compareErrors = Compare(expectedValues, actualElements);
						if (compareErrors != null)
							errors = string.Format("{0}\n{1}", errors, compareErrors);
					}
					else // string
					{
						Element e = element.Locate(pair.Key);
						string value = pair.Value.ToString();

						ExecutionSteps.Log.WriteLine(string.Format("    {0} = '{1}'", pair.Key, value));

						try
						{
							e.Is(value);
						}
						catch (Exception x)
						{
							throw new Exception(string.Format("Compare {0} {1}", pair.Key, x.Message));
						}
					}
				}
				catch (Exception e)
				{
					errors = string.Format("{0}{1}\n", errors, e.Message);
				}
			}

			if (errors != null)
			{
				errors = "Failed trying to locate the following: " + errors;
				Assert.Fail(errors + "\n" + WebTest.CurrentTest().ErrorContext());
			}

			if (UIConstants.RECORD_FIELD_ACCESS)
			{
				DateTime time = DateTime.UtcNow;
				ExecutionSteps.Log.EmbedImage(
					string.Format("{0} {1} {2}.png", time.ToLongDateString(), time.ToLongTimeString(), time.Millisecond),
					new CaptureWebPage(element.DomContainer).CaptureWebPageImage(false, false, 100));
			}

			return element;
		}

		// Compares two ArrayLists.
		private static string Compare(IEnumerable<string> expectedValues, Element[] actualValues)
		{
			string errors = null;

			int i = 0;
			foreach (string expectedValue in expectedValues)
			{
				try
				{
					actualValues[i].Is(expectedValue); //TODO Better error recording
				}
				catch (Exception e)
				{
					errors = string.Format("{0}{1}\n", errors, e.Message);
				}
				i++;
			}

			if (i != actualValues.Length)
			{
				errors = string.Format("Expected and actual values don't match. \nExpected: {0}\nActual: {1}\n{2}",
				                       expectedValues, actualValues, errors);
			}
			return errors;
		}

		public static Element BackgroundColorIs(this Element elem, string colorIs)
		{
			var htmlElement = (IHTMLElement2) elem.HTMLElement;
			object color = htmlElement.currentStyle.backgroundColor;

			Assert.AreEqual(colorIs, color, WebTest.CurrentTest().ErrorContext());

			return elem;
		}

		private static Element SetBackgroundColor(this Element elem, string color)
		{
			var htmlElement = (IHTMLElement) elem.HTMLElement;
			htmlElement.style.backgroundColor = color;
			return elem;
		}

		public static Element AttributeContains(this Element elem, string attrName, string textIs)
		{
			string value = elem.GetAttributeValue(attrName);

			Assert.IsTrue(value.Contains(textIs));

			return elem;
		}

		public static Element Is(this Element elem, string textIs)
		{
			Assert.AreEqual(textIs.Trim(), elem._Text().Trim());
			if (UIConstants.RECORD_FIELD_ACCESS) elem.SetBackgroundColor("red");
			return elem;
		}

		public static Element HasNot(this Element elem, string findBy)
		{
			Assert.IsNull(elem.Locate(findBy, false),
			              string.Format("Should not find {0} in {1}.", findBy, elem));

			return elem;
		}

		public static bool _TextIs(this Element elem, string textIs)
		{
			if ("".Equals(textIs))
				textIs = null;

			string text = elem._Text();

			return (text == null && textIs == null) || text.Equals(textIs);
		}

		public static string _Text(this Element elem)
		{
			string text = "";

			if (!string.IsNullOrEmpty(elem.Text))
			{
				text = elem.Text;
			}
			else if (!string.IsNullOrEmpty(elem.TextAfter))
			{
				text = elem.TextAfter;
			}

			return text;
		}

		public static Element Locate(this IElementsContainer container, string name)
		{
			return Locate(container, name, true);
		}

		public static Element Locate(this IElementsContainer container, string name, bool failFast)
		{
			Element[] results = container.LocateAll(name);

			if (failFast && results.Length == 0)
				Assert.Fail(string.Format("\r\nCould not locate '{0}'.\r\n{1}",
				                          name, WebTest.CurrentTest().ErrorContext()));

			return results.FirstOrDefault();
		}

		/// 
		/// Heirarchy of searches:
		/// Form Id
		/// Form Class
		/// Link Id
		/// Link "rel"
		/// Link Class
		public static Element[] LocateAll(this IElementsContainer container, string name)
		{
			var result = new List<Element>();

			//element id
			ElementCollection elems = container.Elements.Filter(Find.ById(name));
			if (!elems.IsEmpty())
			{
				foreach (Element e in elems.GetElements<Element>())
				{
					result.Add(e);
				}
			}

			//element class
			elems = container.Elements.Filter(FindByClassName(name));
			if (!elems.IsEmpty())
			{
				foreach (Element e in elems.GetElements<Element>())
				{
					result.Add(e);
				}
			}

			//link id
			LinkCollection links = container.Links.Filter(Find.ById(name));
			if (!links.IsEmpty())
			{
				foreach (Element e in links.GetElements<Element>())
				{
					result.Add(e);
				}
			}

			//link class
			links = container.Links.Filter(FindByClassName(name));
			if (!links.IsEmpty())
			{
				foreach (Element e in links.GetElements<Element>())
				{
					result.Add(e);
				}
			}


			return result.ToArray();
		}

		public static Element LocateLink(this IElementsContainer container, string transition)
		{
			//form id
			FormCollection forms = container.Forms.Filter(Find.ById(transition));
			if (!forms.IsEmpty())
				return forms[0];

			//form class
			forms = container.Forms.Filter(FindByClassName(transition));
			if (!forms.IsEmpty())
				return forms[0];

			//link id
			LinkCollection links = container.Links.Filter(Find.ById(transition));
			if (!links.IsEmpty())
				return links[0];

			//link "rel"
			links = container.Links.Filter(FindByRel(transition));
			if (!links.IsEmpty())
				return links[0];

			//link class
			links = container.Links.Filter(FindByClassName(transition));
			if (!links.IsEmpty())
				return links[0];

			return null;
		}

		public static Element Locate(this Element arg, string findBy)
		{
			var container = (IElementsContainer) arg;

			return container.Locate(findBy);
		}

		public static Element Locate(this Element arg, string findBy, bool failFast)
		{
			var container = (IElementsContainer) arg;

			return container.Locate(findBy, failFast);
		}

		public static Element Locate(this Element arg, string findBy, IDictionary<string, object> withParams)
		{
			Assert.AreEqual(1, withParams.Count(), "Only single parameter supported today");

			string nestedFindBy = withParams.First().Key;
			object contains = withParams.First().Value;
			string msg = string.Format("Find '{0}' with {1} = '{2}'", findBy, nestedFindBy, contains);

			ExecutionSteps.Log.WriteLine(msg);

			foreach (Element result in arg.LocateAll(findBy))
			{
				foreach (Element nested in result.LocateAll(nestedFindBy))
					if (nested._TextIs(contains.ToString()))
					{
						return result;
					}
			}
			Assert.Fail("Unable to {0}", msg);
			return null;
		}

		public static Element[] LocateAll(this Element arg, string findBy)
		{
			if (arg is IElementsContainer)
			{
				var container = (IElementsContainer) arg;
				return container.LocateAll(findBy);
			}
			else
			{
				return new Element[0];
			}
		}

		public static Element Locate(this IElementsContainer arg, AttributeConstraint findBy)
		{
			Element result = arg.Element(findBy);
			if (result.Exists)
				return result;

			throw new WatiNException(string.Format("Cannot locate element that matches contraint: {0}", findBy));
		}

		public static Element Locate(this Element arg, AttributeConstraint findBy)
		{
			var container = (IElementsContainer) arg;

			return container.Locate(findBy);
		}

		public static Element LocateFromEnclosing(this Element arg, string findBy)
		{
			if (arg == null)
			{
				throw new ArgumentNullException("arg", string.Format("Element is null.  Cannot find by {0}", findBy));
			}

			Element pointer = arg.Parent;

			while (pointer != null)
			{
				Element result = pointer.Locate(findBy);

				if (result != null)
					return result;

				try
				{
					pointer = pointer.Parent;
				}
				catch (WatiNException)
				{
					pointer = null;
				}
			}

			return null;
		}

		public static void Follow(this IElementsContainer arg, string transition)
		{
			arg.Follow(transition, null, null);
		}

		public static void Follow(this Element arg, string transition, Until until)
		{
			var container = (IElementsContainer) arg;

			container.Follow(transition, until);
		}

		public static void Follow(this IElementsContainer arg, string transition, Until until)
		{
			arg.Follow(transition, null, until);
		}

		public static void Follow(this IElementsContainer arg, string transition, IDictionary<string, object> withParams,
		                          Until until)
		{
			arg.Follow(transition, withParams, until, 4);
		}

		public static void Follow(this IElementsContainer arg, string transition, IDictionary<string, object> withParams,
		                          Until until, int countDown)
		{
			Assert.IsTrue(countDown > 0, "Didn't match transition:" + transition + " looking for:" + until.s);

			Element elem;
			if (transition == null)
				elem = (Element) arg;
			else
				elem = arg.LocateLink(transition);

			if (elem == null)
				Assert.Fail(string.Format("Didn't find element for transition: {0}", transition));

			if (withParams != null)
				using (ExecutionSteps.Log.BeginSection("Apply Parameters:"))
				{
					ApplyParams(elem, withParams);
				}

			DomContainer domContainer = DomContainer(arg);

			Transition(elem);

			if (until == null || domContainer.Locate(until.s) != null)
				return;

			domContainer.Follow(transition, withParams, until, countDown - 1);
		}

		public static Element[] IsDescendingByDateWithCurrentForEndDate(this Element[] elements,
		                                                                string currentSortingIdentifier,
		                                                                string nonCurrentSortingIdentifier, string dateFormat)
		{
			var enddate = "enddate";
			Element[] currentRecords = elements.Where(enddate).IsEqualTo("Current");
			Element[] otherRecords = elements.Where(enddate).IsNotEqualTo("Current");

			Element[] sortedCurrentRecords = currentRecords.SortBy(currentSortingIdentifier).Ascending();
			Element[] sortedOtherRecords =
				otherRecords.SortBy(nonCurrentSortingIdentifier, x => DateTime.ParseExact(x, dateFormat, CultureInfo.CurrentCulture))
					.Descending();

			Element[] finalRecords = sortedCurrentRecords.Concat(sortedOtherRecords).ToArray();

//			CollectionAssert.AreEqual(finalRecords, elements); //TODO  !!Must re-implement
			return elements;
		}


		public static Where Where(this Element[] collection, string identifier)
		{
			return new Where(collection, identifier);
		}

		public static SortBy SortBy(this Element[] collection, string identifier)
		{
			return new SortBy(collection, identifier);
		}

		public static SortBy SortBy(this Element[] collection, string identifier, Func<string, IComparable> comparer)
		{
			return new SortBy(collection, identifier, comparer);
		}

		private static Element[] GetElements<T>(this IEnumerable collection) where T : Element
		{
			var list = new List<Element>();
			foreach (object o in collection)
			{
				list.Add((T) o);
			}

			return list.ToArray();
		}

		public static DomContainer DomContainer(IElementsContainer forContainer)
		{
			return ((Element) forContainer).DomContainer;
		}


		private static void ApplyParams(Element link, IDictionary<string, object> withParams)
		{
			if (link is Form)
			{
				var form = (Form) link;

				foreach (TextField textfield in form.TextFields)
				{
					string name = textfield.Name;
					if (withParams.ContainsKey(name))
					{
						var value = (string) withParams[name];
						ExecutionSteps.Log.WriteLine(string.Format("Set {0} to {1}", name, value));
						textfield.Value = value;
					}
				}

				foreach (CheckBox checkbox in form.CheckBoxes)
				{
					string name = GetName(checkbox);
					if (withParams.ContainsKey(name))
					{
						var value = (bool) withParams[name];
						ExecutionSteps.Log.WriteLine(string.Format("Set {0} to {1}", name, value));
						checkbox.Checked = value;
					}
				}

				foreach (SelectList selectList in form.SelectLists)
				{
					string name = GetName(selectList);
					if (withParams.ContainsKey(name))
					{
						var value = (string) withParams[name];
						ExecutionSteps.Log.WriteLine(string.Format("Set {0} to {1}", name, value));
						selectList.Select(value);
					}
				}
			}
		}

		public static string Describe(this Element elem)
		{
			string type = elem.ClassName;

			string text = elem._Text();
			string textS = "";
			if (!text.Equals(""))
				textS = string.Format("({0})", text);
			if (textS.Length > 15)
				textS = textS.Substring(0, 15);

			string id = elem.Id;
			string idS = "";
			if (id != null && !id.Equals(""))
				idS = string.Format("[{0}]", id);

			return string.Format("{0}{1}{2}", type, idS, textS);
		}

		private static string GetName(Element element)
		{
			object htmlElement = element.HTMLElement;
			if (htmlElement is IHTMLInputElement)
			{
				var inputElement = (IHTMLInputElement) htmlElement;
				return inputElement.name;
			}
			else
			{
				var selectElement = (IHTMLSelectElement) htmlElement;
				return selectElement.name;
			}
		}

		private static void Transition(Element elem)
		{
			if (elem is Form)
			{
				var form = (Form) elem;

				ExecutionSteps.Log.WriteLine(string.Format("Submit {0}", elem.Describe()));
				form.Submit();
			}

			if (elem is Link)
			{
				var link = (Link) elem;

				if (link.Url != null && !link.Url.Equals(elem.DomContainer.Url))
				{
					ExecutionSteps.Log.WriteLine(string.Format("Click '{0}'", elem.Text));
					link.Click();
				}
			}
		}

		// search for 'name' in each value of the 'attrName' NMToken
		//
		// NMTOKENS are a space separated list of names.
		// Example: FindByNMTokens("rel", "bar") would match <a rel="foo bar"/>
		public static AttributeConstraint FindByNMTokens(string attrName, string name)
		{
			return new AttributeConstraint(attrName, new NamesCompare(name));
		}

		public static AttributeConstraint FindByClassName(string name)
		{
			return FindByNMTokens("classname", name);
		}

		public static AttributeConstraint FindByRel(string name)
		{
			return FindByNMTokens("rel", name);
		}
	}
}