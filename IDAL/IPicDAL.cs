using System;
namespace cms_webservice.IDAL
{
    public interface IPicDAL
    {
        bool upload(int petitionId, byte[] pic);
    }
}
