using UnityEngine.Android;

namespace Services.Save
{
    public class PermissionService : IPermissionService
    {
        public void PermissionReques()
        {
            bool isRead = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);
            bool isWrite = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);

            if (!(isRead && isWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
        }
    }
}
