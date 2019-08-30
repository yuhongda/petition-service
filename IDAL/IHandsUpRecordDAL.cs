using System;
using cms_webservice.Model;

namespace cms_webservice.IDAL
{
    public interface IHandsUpRecordDAL
    {
        bool InsertHandsUpRecord(HandsUpRecord handsUpRecord);
        bool DeleteHandsUpRecord(HandsUpRecord handsUpRecord);
    }
}
