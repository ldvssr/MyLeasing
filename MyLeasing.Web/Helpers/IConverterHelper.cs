using System;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public interface IConverterHelper
    {
        Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew);

        OwnerViewModel ToOwnerViewModel(Owner owner);

        public Lessee ToLessee(LesseeViewModel model, Guid imageId, bool isNew);

        public LesseeViewModel ToLesseeViewModel(Lessee lessee);
    }
}