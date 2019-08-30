using System;
using System.Collections.Generic;
using System.Data;
using cms_webservice.Model;

namespace cms_webservice.IDAL
{
    public interface IPetitionDAL
    {
        bool InsertPetition(Petition petition, List<Pic> pics);
        DataTable getPetitionList();
        DataTable getPetitionById(int petitionId);
        bool UpdatePetition(Petition petition, List<Pic> pics);
        DataTable getPicsByPetitionId();
    }
}
