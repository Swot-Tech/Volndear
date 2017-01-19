using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;


namespace Volndear
{
	public class EventDatabase: SQLiteConnection
	{
		static object locker = new object();
		public EventDatabase(string path) : base(path)
		{
			CreateTable<Events>();
		}

		public int InsertNewEvent(Events item)
		{
			lock (locker)
			{

				return Insert(item);

			}
		}

		public List<Events> GetAllEvents()
		{
			lock (locker)
			{
				return Table<Events>().ToList();
			}
		}

	}
}
