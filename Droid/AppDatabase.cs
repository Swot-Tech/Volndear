using System;
using System.IO;

namespace Volndear
{
	public class AppDatabase
	{
		public static CitizenRegistrationDatabase citizenRegistrationDatabase;

		public AppDatabase(string sqliteDBName)
		{
			CreateDatabase(sqliteDBName);
			databaseInitialization(sqliteDBName);
		}
		public static void CreateDatabase(
			String filename)
		{
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			//---destination path for file in the Documents 
			// folder---
			var destinationPath =
				System.IO.Path.Combine(documentsPath, filename);

			//---path of source file---

			var sourcePath =
				System.IO.Path.Combine(System.Environment.CurrentDirectory,
					filename);
			//---print for verfications---
			Console.WriteLine(destinationPath);
			Console.WriteLine(sourcePath);

			try
			{
				//---copy only if file does not exist---
				if (!File.Exists(destinationPath))
				{
					File.Copy(sourcePath, destinationPath, true);
					Console.WriteLine("overwritten");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private void databaseInitialization(string sqliteFilename)
		{
			string documentsPath = System.Environment.CurrentDirectory;

			documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = System.IO.Path.Combine(documentsPath, sqliteFilename);

			citizenRegistrationDatabase = new CitizenRegistrationDatabase(path);

		}
	}
}
