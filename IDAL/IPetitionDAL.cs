using System;
using System.Collections.Generic;
using System.Data;
using cms_webservice.Model;

namespace cms_webservice.IDAL
{
    public interface IPetitionDAL
    {
        bool InsertPetition(List<Petition> petition);
        DataTable getPetitionList();
    }
}
