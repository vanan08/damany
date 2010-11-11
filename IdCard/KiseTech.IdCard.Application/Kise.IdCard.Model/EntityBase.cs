using System;
using DevExpress.Xpo;

namespace Kise.IdCard.Model
{

    public class EntityBase : XPObject
    {
        public EntityBase()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
            CreationDate = DateTime.Now;
        }

        public EntityBase(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
            CreationDate = DateTime.Now;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        public DateTime CreationDate { get; set; }
    }

}