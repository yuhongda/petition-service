using System;
using System.Data;
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

        public bool CheckIsHandsUp(HandsUpRecord handsUpRecord)
        {
            return dal.CheckIsHandsUp(handsUpRecord);
        }

        public DataTable GetHandsUpRecordByUserId(string userId)
        {
            return dal.GetHandsUpRecordByUserId(userId);
        }

        public DataTable GetHandsUpRecordByPetitionId(int petitionId)
        {
            return dal.GetHandsUpRecordByPetitionId(petitionId);
        }
        

    }
}
