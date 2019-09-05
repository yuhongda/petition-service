using System;
using System.Data;
using System.Collections.Generic;
using cms_webservice.IDAL;
using cms_webservice.Model;

namespace cms_webservice.BLL
{
    public class PetitionBLL
    {
        private static readonly IPetitionDAL dal = cms_webservice.DALFactory.DataAccess.CreatePetitionDAL();

        public DataTable getPetitionList()
        {
            return dal.getPetitionList();
        }

        public int InsertPetition(Petition petition, List<Pic> pics)
        {
            return dal.InsertPetition(petition, pics);
        }

        public DataTable getPetitionById(int petitionId)
        {
            return dal.getPetitionById(petitionId);
        }

        public bool UpdatePetition(Petition petition, List<Pic> pics)
        {
            return dal.UpdatePetition(petition, pics);
        }

        public DataTable getPicsByPetitionId()
        {
            return dal.getPicsByPetitionId();
        }
        
    }
}
