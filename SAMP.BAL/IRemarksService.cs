using SAMP.Models.Common;

namespace SAMP.BAL
{
    public interface IRemarksService
    {
        RemarksRes GetRemarks(RemarksReq req);
    }
}