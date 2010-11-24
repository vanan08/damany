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
        }

        public EntityBase(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            CreationDate = DateTime.Now;

        }


        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { SetPropertyValue("CreationDate", ref _creationDate, value); }
        }
    }

}