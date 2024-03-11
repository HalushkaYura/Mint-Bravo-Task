using System.Data.SqlClient;

namespace Bramka.Server.Constans
{
    public class DataBaseConstants
    {
        #region Users

        public const string CreateUser = "sp_Users_CreateUser";
        public const string UpdateUser = "sp_Users_UpdateUser";
        public const string DeleteUser = "sp_Users_DeleteUser";
        public const string GetAllUsers = "sp_Users_GetAllUsers";
        public const string GetUserById = "sp_Users_GetUserById";
        public const string GetUserByEmail = "sp_Users_GetUserByEmail";
        public const string GetUserByRefreshToken = "sp_Users_GetUserByRefreshToken";
        public const string GetLastUser = "sp_Users_GetLastUser";
        public const string CheckExistEmail = "sp_Users_CheckExistEmail";
        public const string UpdateRefreshToken = "sp_Users_UpdateRefreshToken";

        #endregion

        #region QrCode

        public const string CreateQrCode = "sp_QrCode_CreateQrCode";
        public const string UpdateQrCode = "sp_QrCode_UpdateQrCode";
        public const string DeleteQrCode = "sp_QrCode_DeleteQrCode";
        public const string GetAllQrCodes = "sp_QrCode_GetAllQrCodes";
        public const string GetQrCodeById = "sp_QrCode_GetQrCodeById";
        public const string GetLastQrCode = "sp_QrCode_GetLastQrCode";
        public const string UpdateQrCodeUseDate = "sp_QrCode_UpdateUseDate";
        public const string GetAllQrCodeByUserId = "sp_QrCode_GetQrCodeByUserId";
        public const string QrCodeExist = "sp_QrCode_CheckUserHasKey";

        #endregion

        #region Role

        public const string CreateRole = "sp_Role_CreateRole";
        public const string UpdateRole = "sp_Role_UpdateRole";
        public const string DeleteRole = "sp_Role_DeleteRole";
        public const string GetAllRoles = "sp_Role_GetAllRoles";
        public const string GetRoleById = "sp_Role_GetRoleById";
        public const string GetLastRole = "sp_Role_GetLastRole";
        public const string GetRoleIdByName = "sp_Role_GetRoleIdByName";

        #endregion

        #region Log

        public const string CreateLog = "sp_Log_CreateLog";
        public const string UpdateLog = "sp_Log_UpdateLog";
        public const string DeleteLog = "sp_Log_DeleteLog";
        public const string GetAllLogs = "sp_Log_GetAllLogs";
        public const string GetLogById = "sp_Log_GetLogById";
        public const string GetLastLog = "sp_Log_GetLastLog";
        public const string GetAllLogByUserId = "sp_Log_GetLogByUserId";


        #endregion

        #region VerificationCodes

        public const string CreateVerificationCode = "sp_VerificationCodes_CreateCode";
        public const string GetCodeByUserId = "sp_VerificationCodes_GetCodeByUserId";
        public const string UpdateVerificationCode = "sp_VerificationCodes_UpdateCode";
        public const string DeleteVerificationCode = "sp_VerificationCodes_DeleteCode";
        public const string ConfirmEmail = "sp_VerificationCodes_ConfirmEmail";
        #endregion

    }
}
