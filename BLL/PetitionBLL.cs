using System;
using System.Data;
using System.Collections.Generic;
using cms_webservice.IDAL;
using cms_webservice.Model;
using cms_webservice.DAL;
using System.Data.SqlClient;

namespace cms_webservice.BLL
{
    public class PetitionBLL
    {
        private static readonly IPetitionDAL dal = cms_webservice.DALFactory.DataAccess.CreatePetitionDAL();

        public PageMoudle getPetitionList(int pageSize, List<SortField> sortFields, int reviewStatus)
        {
            SqlParameter[] sp = {
                new SqlParameter("@reviewStatus", reviewStatus)
            };
            PageMoudle petitionPageModle = new PageMoudle(string.Format(PetitionDAL.sqlGetPetitionList, "@reviewStatus"), sp);
            petitionPageModle.SortField = sortFields;
            petitionPageModle.PageSize = pageSize;
            return petitionPageModle;
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
