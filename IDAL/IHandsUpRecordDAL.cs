using System;
using System.Data;
using cms_webservice.Model;

namespace cms_webservice.IDAL
{
    public interface IHandsUpRecordDAL
    {
        bool InsertHandsUpRecord(HandsUpRecord handsUpRecord);
        bool DeleteHandsUpRecord(HandsUpRecord handsUpRecord);
        bool CheckIsHandsUp(HandsUpRecord handsUpRecord);
        DataTable GetHandsUpRecordByUserId(string userId);
        DataTable GetHandsUpRecordByPetitionId(int petitionId);
    }
}
