using System;
using cms_webservice.IDAL;
using cms_webservice.Model;

namespace cms_webservice.BLL
{
    public class HandsUpRecordBLL
    {
        private static readonly IHandsUpRecordDAL dal = cms_webservice.DALFactory.DataAccess.CreateHandsUpRecordDAL();

        public bool InsertHandsUpRecord(HandsUpRecord handsUpRecord)
        {
            return dal.InsertHandsUpRecord(handsUpRecord);
        }

        public bool DeleteHandsUpRecord(HandsUpRecord handsUpRecord)
        {
            return dal.DeleteHandsUpRecord(handsUpRecord);
        }
    }
}
