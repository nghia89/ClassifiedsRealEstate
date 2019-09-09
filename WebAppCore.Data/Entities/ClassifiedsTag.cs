using System.ComponentModel.DataAnnotations.Schema;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
    [Table("ClassifiedsTags")]
    public class ClassifiedsTag : DomainEntity<int>
    {
        public int ClassifiedsId { set; get; }

        public string TagId { set; get; }

        [ForeignKey("ClassifiedsId")]
        public virtual Classifieds Classifieds { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}