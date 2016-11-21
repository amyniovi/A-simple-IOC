using System;
namespace ControllerFactory
{
	public class Module
	{
		public int Thing
		{
			get;
			set;
		}
	}

	public class Person
	{
		public int Id
		{
			get;
			set;
		}
		public string Idea {

			get; set;
		}
	}

	public class ModuleBuilder
	{
		public ModuleBuilder Fun()
		{
			return this;
		}
	}

	public class PersonBuilder
	{
		private int _id;
		private string _idea;
		private ModuleBuilder module = new ModuleBuilder();

		public PersonBuilder WithPersonId(int id)
		{
			_id = id;
			return this;
		}

		public PersonBuilder WithIdea(string whatevs)
		{
			_idea = whatevs;
			return this;
		}

		public ModuleBuilder W()
		{
			return module;
		}

		public Person Build()
		{
			return new Person
			{
				Id = _id
			};
		}
	}

}
