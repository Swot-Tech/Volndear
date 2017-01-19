using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Volndear
{
	public class CitizenRegistrationDatabase : SQLiteConnection
	{
		static object locker = new object();

		public CitizenRegistrationDatabase(string path) : base(path)
		{
			// create the tables
			CreateTable<Citizen>();
		}

		public int InsertCitizen(Citizen item)
		{
			lock (locker)
			{

				return Insert(item);

			}
		}

		public Citizen GetCitizen()
		{
			lock (locker)
			{
				return Table<Citizen>().FirstOrDefault();
			}
		}


		public List<Citizen> GetAllCitizen()
		{
			lock (locker)
			{
				return Table<Citizen>().ToList();
			}
		}


	}
}
