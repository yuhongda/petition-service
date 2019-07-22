using System;
using System.Data;
using cms_webservice.IDAL;

namespace cms_webservice.BLL
{
    public class PetitionBLL
    {
        private static readonly IPetitionDAL dal = cms_webservice.DALFactory.DataAccess.CreatePetitionDAL();

        public DataTable getPetitionList()
        {
            return dal.getPetitionList();
        }
    }
}
