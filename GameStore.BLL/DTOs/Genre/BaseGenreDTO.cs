namespace GameStore.BLL.DTOs.Genre
{
    public class BaseGenreDTO
    {
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}
