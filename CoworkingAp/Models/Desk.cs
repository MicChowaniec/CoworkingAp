namespace CoworkingAp.Models
{
    public class Desk
    {
            public  int Id { get; set; }
            public int Number { get; set; }

        public DateTime reservation { get; set; } = new DateTime(9999, 12, 31, 0, 0, 0);
            public int roomId { get; set; }
    }
}
