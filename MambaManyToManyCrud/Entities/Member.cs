namespace MambaManyToManyCrud.Entities
{
    public class Member:BaseEntity
    {
        public string FullName {  get; set; }       
        public string LinkUrl { get; set; }
        public string ImageUrl {  get; set; }
        public List<MemberProfession> MemberProfessions { get; set; }

    }
}
