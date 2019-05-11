using System.Threading.Tasks;

namespace MoviesCatalog.Services.Providers
{
    public static class HostingService
    {
        public static async Task<bool> KeepServerAlive()
        {
            byte hook = 0;
            bool statement = false;

            while (hook != 3)
            {
                if (hook == 1)
                {
                    hook--;
                    statement = false;
                }
                else
                {
                    hook++;
                    statement = false;
                }
            }

            return statement;
        }
    }
}
