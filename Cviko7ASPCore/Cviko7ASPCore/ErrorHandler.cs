namespace Cviko7ASPCore
{
    public class ErrorHandler
    {
        private readonly IMyLogger logger;

        public ErrorHandler(IMyLogger logger)
        {
            this.logger = logger;
        }
        public async Task Handler(Exception ex)
        {
            await logger.Log(ex.Message + "\n" + ex.StackTrace);
        }
    }
}
