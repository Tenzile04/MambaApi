﻿namespace MambaManyToManyCrud.Entities
{
    public class Profession:BaseEntity
    {
        public string Name {  get; set; }
        public List<MemberProfession> MemberProfessions { get; set; }
    }
}
