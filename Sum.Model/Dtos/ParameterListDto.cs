namespace Sum.Model.Dtos
{
    public class ParameterListDto
    {
        public int Id { get; set; }
        public int? ParameterTypeId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}