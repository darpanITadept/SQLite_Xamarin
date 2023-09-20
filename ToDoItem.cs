using System;
using SQLite;

namespace SQLiteDemo
{
	public class ToDoItem
	{
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }

		// poperties
		public string Title { get; set; }
		public bool IsHighPriority { get; set; }

		//mandatory: When working with SQLite, you must provide and empty, no-argument constructor
		public ToDoItem(){}

		//this the constructor that will be used to create new items
		public ToDoItem(string title, bool isHighPriority)
		{
			this.Title = title;
			this.IsHighPriority = isHighPriority;
		}

        public override string ToString()
        {
            return $"Id:{this.Id}, Title:{this.Title},High Priority:{this.IsHighPriority}";
        }
    }
}