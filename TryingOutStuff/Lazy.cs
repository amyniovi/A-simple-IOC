using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TryingOutStuff
{
	class MainClass
	{

		public static void Main(string[] args)
		{
			

			//thread safe Lazy

			var safe = new Lazy<Config>(() =>
			{
				var MyThreadConfig = new Config() { StagingDb = Thread.CurrentThread.ManagedThreadId + "/staging/" };
				return MyThreadConfig;

			}, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication

									   );

			var semisafe = new Lazy<Config>(() =>
			{
				var MyThreadConfig = new Config() { StagingDb = Thread.CurrentThread.ManagedThreadId + "/staging/" };
				return MyThreadConfig;

			}, System.Threading.LazyThreadSafetyMode.PublicationOnly

									   );

			var nonsafe = new Lazy<Config>(() =>
		{
			var MyThreadConfig = new Config() { StagingDb = Thread.CurrentThread.ManagedThreadId + "/staging/" };
			return MyThreadConfig;

		}, System.Threading.LazyThreadSafetyMode.None

								   );

			//RUN EACH OF THOSE 3 LINES TO DETERMINE HOW THREAD OR NON THREAD SAFE LAZY WORKS
			//CreateMultipleThreadsThatAccessLazyObj(safe);//1
			CreateMultipleThreadsThatAccessLazyObj(semisafe);//2
			//CreateMultipleThreadsThatAccessLazyObj(nonsafe);//3

			Console.ReadLine();
		}

		private static void CreateMultipleThreadsThatAccessLazyObj(Lazy<Config> lazy)
		{
			var tasks = new List<Task<string>>();
			for (int i = 0; i < 10; i++)
			{

			tasks.Add(	Task.Factory.StartNew(() =>
			   {


				   if (lazy.IsValueCreated)
					   return (lazy.Value as Config).StagingDb;
				   else {
					   var ourlazy = lazy.Value;
					   if (ourlazy != null)
							return (ourlazy as Config).StagingDb;
					   return "could not define";
					
					}
				   
			   }

				                             ));
				
			}
			try
			{
				
				//Task.WaitAll(tasks.ToArray());
				foreach (var task in tasks)
				{

					Console.WriteLine(task.Result);
				}
			}
			catch (AggregateException ae)
			{
				var e = ae.Flatten();
				Console.WriteLine("------------------------------");
				foreach (Exception ex in e.InnerExceptions)
					Console.WriteLine(ex.Message);
				Console.WriteLine("------------------------------");

			}

		}
	}

	public class Config
	{
		public Config()
		{
			Console.WriteLine("constructor of Config called");
		}


		public string StagingDb = "/staging/";
		public static string ResourceUri = "tempApi/customers";
	}



}
