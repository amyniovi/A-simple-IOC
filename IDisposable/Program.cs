﻿using System;
using System.Data.SqlClient;


namespace DisposableStuff
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//In a virtual method invocation, the run-time type of the instance for which that invocation takes place
			//determines the actual method implementation to invoke.
			//In a non-virtual method invocation, the compile-time type of the instance is the determining factor.
			DatabaseState baseState = new DatabaseState();
			baseState.Dispose();
			DatabaseState state = new SharedDatabaseState();
			state.Dispose();
			SharedDatabaseState derivedState = new SharedDatabaseState();
			derivedState.Dispose(false);


			using (var dbS = new DatabaseState())
			{
				dbS.QueryDb();
			}
			Console.WriteLine("Hello World!");
		}


		public class DatabaseState : IDisposable
		{
			SqlConnection _sqlConnection = null;
			bool _disposed = true;

			public void QueryDb() {
				if (_disposed)
					throw new ObjectDisposedException("DatabaseState");
				
				 _sqlConnection = new SqlConnection();

			
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			public virtual void Dispose(bool disposing)
			{
				if (_disposed)
					return;
				if (disposing)
				{
					if (_sqlConnection != null)
					{
						_sqlConnection.Dispose();
						_sqlConnection = null;
					}
					Console.WriteLine("True dispose");
					_disposed = true;
				}


			
			}

			
		}

		public class SharedDatabaseState : DatabaseState
		{
			public override void Dispose(bool disposing)
			{
				Console.WriteLine("doing nothing");
			}
		
		}
	}
}