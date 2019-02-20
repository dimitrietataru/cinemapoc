namespace Core.Models.NoSql
{
    public class Seat
    {
		public Seat()
		{
		}

		public Seat(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}
		
        public int Row { get; set; }
        public int Column { get; set; }
		public bool IsAvailable { get; set; }
	}
}
