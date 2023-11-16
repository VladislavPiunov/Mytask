namespace Calendar.API.Dtos
{
    public class BoardDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public List<string> Stages { get; set; }
        public List<string> Users { get; set; }

    }
}