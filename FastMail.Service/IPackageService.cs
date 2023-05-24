namespace FastMail.Service
    {
        public interface IPackageService
        {
            /// <summary>
                    /// Gets the packages by tracking no
                    /// </summary>
                    /// <param name="trackingNo"></param>
                    /// <returns>A task that represents the asynchronous operation</returns>
            List<string> SearchPackage(string trackingNo);
        }
    }

