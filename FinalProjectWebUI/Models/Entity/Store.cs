namespace FinalProjectWebUI.Models.Entity
{
    public class Store:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<SiteInfo>? SiteInfos { get; set; }
    }
}
