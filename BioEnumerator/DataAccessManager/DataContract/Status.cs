
using System.Collections.Generic;

namespace BioEnumerator.DataAccessManager.DataContract
{
  public struct Status
  {
    public bool IsSuccessful;
    public long ReturnedId;
    public int UserId;
    public string ReturnedValue;
    public Message Message;
    public StationInfo StationInfo;
  }

  public struct SyncUserStatus
  {
      public bool IsSuccessful;
      public Message Message;
      public List<User> UserInfos;
      public List<UserProfile> UserProfileInfos;
  }

}
