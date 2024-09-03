namespace WebAPISimpleCode.Services
{
    public class AuthService(AuthRepository authRepository)
    {
        /// <summary>
        /// 創建帳號
        /// </summary>
        /// <param name="username">User名稱</param>
        /// <param name="password">User密碼</param>
        /// <param name="email">User Email</param>
        /// <returns></returns>
        public async Task<bool> CreateAccountAsync(string username, string password, string email)
        {
            // 生成 Salt
            string salt = PasswordHelper.GenerateSalt();

            // 雜湊密碼
            string hashedPassword = PasswordHelper.HashPassword(password, salt);

            // 創建帳號實例
            var account = new Account
            {
                Username = username,
                PasswordHash = hashedPassword,
                Salt = salt,
                Email = email,
                IsActive = false, // 預設帳號未啟用
                CreatedAt = DateTime.UtcNow
            };

            // 調用 AuthRepository 創建帳號
            return await authRepository.CreateAccountAsync(account);
        }

        /// <summary>
        /// 驗證帳號憑證
        /// </summary>
        /// <param name="username">User 名稱</param>
        /// <param name="password">User 密碼</param>
        /// <returns></returns>
        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            // 根據用戶名獲取帳號
            var account = await authRepository.GetAccountByUsernameAsync(username);
            if (account == null)
            {
                return false; // 帳號不存在
            }

            // 驗證密碼
            return PasswordHelper.VerifyPassword(password, account.PasswordHash, account.Salt);
        }

        /// <summary>
        /// 啟用帳號
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> ActivateAccountAsync(int accountId)
        {
            var account = await authRepository.GetAccountByUsernameAsync(accountId);
            if (account == null)
            {
                return false; // 帳號不存在
            }

            account.IsActive = true;
            return await authRepository.UpdateAccountAsync(account);
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            return await authRepository.DeleteAccountAsync(accountId);
        }
    }
}
